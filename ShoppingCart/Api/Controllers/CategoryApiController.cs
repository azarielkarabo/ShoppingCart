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
    [RoutePrefix("api/CategoryApi")]
    public class CategoryApiController : BaseApiWithModelController<Category, CategoryViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// This action is used for a dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Pull")]
        public HttpResponseMessage Pull()
        {
            var categories = dbContext.Categories.
                ToList().
                Select(c => new
                {
                    c.Name,
                    c.Id
                });

            return Success(categories);
        }
    }
}
