using AutoMapper;
using Microsoft.AspNet.Identity;
using ShoppingCart.Api.v1.Model;
using ShoppingCart.Controllers;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/CartItemApi")]
    public class CartItemApiController : BaseApiWithModelController<CartItem, CartItemViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        [HttpGet]
        [Route("GetAll")]
        public override HttpResponseMessage GetAll()
        {

            var userId = User.Identity.GetUserId();

            var items = dbContext.ShoppingCartItems
                                            .Where(c => c.UserId == userId)
                                        .ToList()
                                   .Select(
                                e =>
                                Mapper.Map<CartItemViewModel>(e))
                          .ToList();

            return Success(items);
        }

        [HttpPost]
        [Route("")]
        public override HttpResponseMessage Create([FromBody] CartItemViewModel model)
        {
            var userId = User.Identity.GetUserId();

            var product = dbContext.Products.Find(model.ProductId);

            if (product != null)
            {
                var cartItem = dbContext.ShoppingCartItems.FirstOrDefault(c => c.Product.Id == product.Id && c.UserId == userId);
                if (cartItem != null)
                {
                    var productPrice = product.Price ?? 0;
                    cartItem.Quantity = cartItem.Quantity + (model.Quantity);
                    cartItem.UnitPrice = productPrice * cartItem.Quantity;

                    model = Mapper.Map<CartItemViewModel>(cartItem);
                }
                else
                {
                    var entity = Mapper.Map<CartItem>(model);
                    entity.Product = product;
                    entity.Description = product?.Description;
                    entity.Name = product?.Name;
                    entity.UnitPrice = product.Price ?? 0.00 * entity.Quantity;
                    entity.UserId = userId;

                    entity.SetId();
                    dbContext.ShoppingCartItems.Add(entity);

                    model = Mapper.Map<CartItemViewModel>(entity);
                }

                dbContext.SaveChanges();
            }

            return Success(model);
        }

        [HttpDelete]
        [Route("ClearCart")]
        public HttpResponseMessage DeleteAll()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = dbContext.ShoppingCartItems.Where(c => c.UserId == userId).ToList();

            foreach (var item in cartItems)
            {
                dbContext.ShoppingCartItems.Remove(item);
                dbContext.SaveChanges();
            }
            return Success();
        }
    }
}
