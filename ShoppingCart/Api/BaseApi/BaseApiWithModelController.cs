using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api
{
    [Authorize]
    public abstract class BaseApiWithModelController<TEntity, TViewModel> : BaseApiController where TEntity : BaseModel
    {
        [HttpGet]
        [Route("")]
        public virtual HttpResponseMessage GetAll()
        {
            return Execute<ServiceRepository<TEntity, TViewModel>>(x =>
              {
                  var data = x.GetAll();
                  return Success(data);
              });
        }

        [HttpGet]
        [Route("id")]
        public virtual HttpResponseMessage Get(Guid id)
        {
            return Execute<ServiceRepository<TEntity, TViewModel>>(x =>
            {
                var data = x.Get(id);
                return Success(data);
            });
        }

        [HttpPut]
        [Route("id")]
        public virtual HttpResponseMessage Update(Guid id, [FromBody]TViewModel model)
        {
            return Execute<ServiceRepository<TEntity, TViewModel>>(x =>
            {
                var data = x.Update(id, model);
                return Success(data);
            });
        }

        [HttpPost]
        [Route("")]
        public virtual HttpResponseMessage Create([FromBody]TViewModel model)
        {
            return Execute<ServiceRepository<TEntity, TViewModel>>(x =>
            {
                var data = x.Create(model);
                return CreateSuccess(data);
            });
        }

        [HttpDelete]
        [Route("id")]
        public virtual HttpResponseMessage Remove(Guid id)
        {
            return Execute<ServiceRepository<TEntity, TViewModel>>(x =>
            {
                x.Remove(id);
                return Success();
            });
        }
    }
}
