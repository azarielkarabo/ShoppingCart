using ShoppingCart.Models;
using ShoppingCart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    [RoutePrefix("api/ContactApi")]
    public class ContactApiController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public HttpResponseMessage SendContact([FromBody]Contact model)
        {
            return Execute<SendContactService>(x =>
            {
                var data = x.SendMailAsync(model);
                return Success(data);
            });
        }

    }
}