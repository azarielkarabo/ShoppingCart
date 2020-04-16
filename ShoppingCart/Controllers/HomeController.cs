using PagedList;
using ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public HomeController()
        {
            dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult CartItems()
        {
            return View();
        }
    }
}
