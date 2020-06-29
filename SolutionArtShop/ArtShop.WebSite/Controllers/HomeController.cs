using ArtShop.Data.Model;
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

        readonly BaseDataService<Product> db;

        public HomeController()
        {
            db = new BaseDataService<Product>();

        }
        public ActionResult Index()
        {
            var model = db.Get().Take(6).Reverse();
            return View(model);
        }
    }
}