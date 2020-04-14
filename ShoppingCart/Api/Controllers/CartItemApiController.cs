using AutoMapper;
using ShoppingCart.Api.v1.Model;
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
            var product = dbContext.Products.Find(model.ProductId);

            var entity = Mapper.Map<CartItem>(model);
            entity.Product = product;
            entity.Name = product?.Name;
            entity.Total = product?.Price ?? 0.00 * entity.Quantity;
            entity.SetId();

            dbContext.SaveChanges();

            model = Mapper.Map<CartItemViewModel>(entity);
            return Success(model);
        }
    }
}
