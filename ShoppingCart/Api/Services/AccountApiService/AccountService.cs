using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShoppingCart.Api.Services.AccountApiService
{

    public class AccountService : IAccountService
    {

        private readonly Database dbContext = new Database();
        private UserManager<IdentityUser> userManager;
        public AccountService()
        {
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(dbContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IdentityResult Register(UserModel model)
        {
            var user = new IdentityUser()
            {
                UserName = model.Username
            };

            var result = userManager.Create(user, model.Password);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IdentityUser Login(UserModel model)
        {
            var user = userManager.Find(model.Username, model.Password);
            return user;
        }

    }
}