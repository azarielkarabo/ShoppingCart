using AutoMapper;
using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/OrderApi")]
    public class OrderApiController : BaseApiWithModelController<Order, OrderViewModel>
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();
        public override HttpResponseMessage Create([FromBody] OrderViewModel model)
        {
            var entity = Mapper.Map<Order>(model);
            entity.SetId();
            dbContext.Orders.Add(entity);
            dbContext.SaveChanges();

            //used to get the cart items that belongs to the user
            var userId = User.Identity.GetUserId();

            var cartItems = dbContext.ShoppingCartItems.Where(c => c.UserId == userId).ToList();
            foreach (var item in cartItems)
            {
                var orderDetails = Mapper.Map<OrderDetail>(item);
                orderDetails.Order = entity;

                dbContext.OrderDetails.Add(orderDetails);
                dbContext.SaveChanges();
            }

            //Now since we have inserted the order and its order details 
            //we delete items from the cart

            foreach (var item in cartItems)
            {
                dbContext.ShoppingCartItems.Remove(item);
                dbContext.SaveChanges();
            }

            return Success();
        }
    }
}
