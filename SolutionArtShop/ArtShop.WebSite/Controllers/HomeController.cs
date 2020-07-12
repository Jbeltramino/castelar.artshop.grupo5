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
        readonly BaseDataService<Artist> dbArtist;

        public HomeController()
        {
            db = new BaseDataService<Product>();
            dbArtist = new BaseDataService<Artist>();
        }
        public ActionResult Index()
        {
            var model = db.Get().OrderBy(x => x.Id).Take(6).Reverse();
            foreach (var item in model)
            {
                item.Artista = dbArtist.GetById(Convert.ToInt32(item.ArtistID));
            }
            return View(model);
        }
    }
}