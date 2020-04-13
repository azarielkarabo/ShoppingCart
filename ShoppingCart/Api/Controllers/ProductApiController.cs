﻿using AutoMapper;
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
    [RoutePrefix("api/v1/Product")]
    public class ProductApiController : BaseApiWithModelController<Product, ProductViewModel>
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
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
