using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ShoppingCart.Api
{
    public abstract class BaseApiController : ApiController
    {
        protected HttpResponseMessage Execute<T>(Func<T, HttpResponseMessage> action)
        {
            try
            {
                var service = DependencyResolver.Current.GetService<T>();
                return action.Invoke(service);
            }
            catch (ValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


        protected HttpResponseMessage Success(object data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        protected HttpResponseMessage Success()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }
        protected HttpResponseMessage DeleteSuccess()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
        protected HttpResponseMessage CreateSuccess(object data)
        {
            return Request.CreateResponse(HttpStatusCode.Created, data);
        }
        protected HttpResponseMessage Failure(HttpStatusCode code, string message)
        {
            return Request.CreateErrorResponse(code, message);
        }
        protected HttpResponseMessage Error(string message)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        }
    }
}
