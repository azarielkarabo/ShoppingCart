using ShoppingCart.Api.v1.Model;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    [RoutePrefix("api/v1/Category")]
    public class CategoryApiController : BaseApiWithModelController<Category, CategoryViewModel>
    {
    }
}
