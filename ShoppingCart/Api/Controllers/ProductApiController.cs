using AutoMapper;
using ShoppingCart.Api.v1.Model;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    /// <summary>
    /// All the product cruds are done in a generic BaseApiWithModelController
    /// </summary>
    [RoutePrefix("api/ProductApi")]
    public class ProductApiController : BaseApiWithModelController<Product, ProductViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Added here since  the baseapi needs authorization
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public override HttpResponseMessage GetAll()
        {
            return base.GetAll();
        }

        [HttpPost]
        public override HttpResponseMessage Create([FromBody] ProductViewModel model)
        {

            var category = dbContext.Categories.Find(model.CategoryId);

            var entity = Mapper.Map<Product>(model);
            entity.Category = category;
            entity.SetId();

            dbContext.Products.Add(entity);
            dbContext.SaveChanges();

            model = Mapper.Map<ProductViewModel>(entity);

            return Success(model);
        }
    }
}
