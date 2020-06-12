using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class PaintsController : Controller
    {
        // GET: Paints
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult itemPaint()
        {
            return View();
        }
    }
}