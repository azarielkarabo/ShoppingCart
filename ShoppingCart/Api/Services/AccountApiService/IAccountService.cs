using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingCart.Api.Services.AccountApiService
{
    interface IAccountService
    {
        IdentityUser Login(UserModel model);
        IdentityResult Register(UserModel model);
    }
}
