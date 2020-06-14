using ArtShop.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolutionPlatformLPPA5.Models
{
    public class HomeController : Controller
    {

        readonly IProductData db;

        public HomeController()
        {
            db = new InMemoryProductData();

        }
        public ActionResult Index()
        {
            var model = db.GetAll().Reverse().Take(6).Reverse();
            return View(model);
        }
    }
}