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
            return base.GetAll();
        }

        [HttpPost]
        [Route("")]
        public override HttpResponseMessage Create([FromBody] CartItemViewModel model)
        {
            var user = new AccountController().GetUser();
            if (user == null)
            {
                throw new Exception("User should log in first");
            }

            var product = dbContext.Products.Find(model.ProductId);

            if (product != null)
            {
                var cartItem = dbContext.ShoppingCartItems.FirstOrDefault(c => c.Product.Id == product.Id && c.ApplicationUser == user);
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
                    entity.ApplicationUser = user;

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
            var cartItems = dbContext.ShoppingCartItems.ToList();
            foreach (var item in cartItems)
            {
                dbContext.ShoppingCartItems.Remove(item);
                dbContext.SaveChanges();
            }
            return Success();
        }
    }
}
