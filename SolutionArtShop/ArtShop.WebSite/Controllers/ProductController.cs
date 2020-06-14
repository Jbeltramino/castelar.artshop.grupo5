using ArtShop.Data.Model;
using ArtShop.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class ProductController : Controller
    {

        readonly IProductData db;
        
        public ProductController()
        {
            db = new InMemoryProductData();
            
        }
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }
        public ActionResult itemProduct(Product data)
        {
            return View(data);
        }
    }
}