using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Data
{
    public class AuthRepo : IDisposable
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();
        private UserManager<IdentityUser> userManager;
        public AuthRepo()
        {
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(dbContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Register(UserModel model)
        {
            var user = new IdentityUser()
            {
                UserName = model.Username
            };

            var result = userManager.Create(user, model.Password);

            return result;
        }

        public async Task<IdentityUser> Login(UserModel model)
        {
            var user = userManager.Find(model.Username, model.Password);
            return user;
        }

        public void Dispose()
        {
            dbContext.Dispose();
            userManager.Dispose();
        }
    }
}