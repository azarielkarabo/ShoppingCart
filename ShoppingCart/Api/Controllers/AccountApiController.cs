using Microsoft.AspNet.Identity;
using ShoppingCart.Api.Services.AccountApiService;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountApiController : BaseApiController
    {
        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register([FromBody]UserModel model)
        {
            return Execute<AccountService>(x =>
            {
                var data = x.Register(model);
                return Success(data);
            });
        }
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]UserModel model)
        {
            return Execute<AccountService>(x =>
            {
                var data = x.Login(model);
                return Success(data);
            });
        }
    }
}
