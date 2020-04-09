using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api
{
    public class BaseApiWithModelController<TClass, TViewModel> : BaseApiController where TClass : BaseModel
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            return Execute<TService<TClass, TViewModel>>(x =>
              {
                  var data = x.GetAll();
                  return Success(data);
              });
        }

        [HttpGet]
        [Route("id")]
        public HttpResponseMessage Get(Guid id)
        {
            return Execute<TService<TClass, TViewModel>>(x =>
            {
                var data = x.Get(id);
                return Success(data);
            });
        }

        [HttpPut]
        [Route("id")]
        public HttpResponseMessage Update(Guid id, [FromBody]TViewModel model)
        {
            return Execute<TService<TClass, TViewModel>>(x =>
            {
                var data = x.Update(id, model);
                return Success(data);
            });
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create([FromBody]TViewModel model)
        {
            return Execute<TService<TClass, TViewModel>>(x =>
            {
                var data = x.Create(model);
                return Success(data);
            });
        }

        [HttpDelete]
        [Route("id")]
        public HttpResponseMessage Remove(Guid id)
        {
            return Execute<TService<TClass, TViewModel>>(x =>
            {
                x.Remove(id);
                return Success();
            });
        }
    }
}
