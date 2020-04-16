using AutoMapper;
using ShoppingCart.Api.v1.Model;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
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
        [Route("")]
        public override HttpResponseMessage Create([FromBody] ProductViewModel model)
        {
            string path = null;

            var httpFileCollection = HttpContext.Current.Request.Files;

            if (httpFileCollection.Count != 0)
            {
                var file = httpFileCollection[0];
                string fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

                path = HttpContext.Current.Server.MapPath("~/Content/Images/Products/" + fileName);
                file.SaveAs(path);

                path = "/Content/Images/Products/" + fileName;
            }

            var stringPrice = HttpContext.Current.Request.Form["price"].Trim();

            if (!string.IsNullOrWhiteSpace(stringPrice))
            {
                model.Price = double.Parse(stringPrice, CultureInfo.InvariantCulture);
            }
            var category = dbContext.Categories.Find(model.CategoryId);

            var entity = Mapper.Map<Product>(model);
            entity.Category = category;
            entity.ImagePath = path;
            entity.SetId();

            dbContext.Products.Add(entity);
            dbContext.SaveChanges();

            model = Mapper.Map<ProductViewModel>(entity);

            return Success(model);
        }
    }
}
