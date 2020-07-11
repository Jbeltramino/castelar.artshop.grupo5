using ArtShop.Data.Model;
using ArtShop.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class ErrorController : Controller
    {
        readonly BaseDataService<Error> db;

        public ErrorController()
        {
            db = new BaseDataService<Error>();
        }
        // GET: Error
        public ActionResult Index()
        {
            var model = db.Get();
            return View(model);
        }
    }
}