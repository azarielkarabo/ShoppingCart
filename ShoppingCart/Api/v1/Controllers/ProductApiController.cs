using ShoppingCart.Api.v1.Model;
using ShoppingCart.Models;
using System;
using System.Web.Http;

namespace ShoppingCart.Api.v1.Controllers
{
    /// <summary>
    /// All the product cruds are done in a generic BaseApiWithModelController
    /// </summary>
    [RoutePrefix("api/v1/Product")]
    public class ProductApiController : BaseApiWithModelController<Product, ProductViewModel>
    {

    }
}
