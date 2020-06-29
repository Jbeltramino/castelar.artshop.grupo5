using ArtShop.Data.Model;
using ArtShop.Data.Services;
using OdeToFood.WebSite.Controllers;
using OdeToFood.WebSite.Services;
using System;
using System.ComponentModel;
using System.Net;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class ArtistController : BaseController
    {
        // GET: Artist

        private BaseDataService<Artist> db;

        public ArtistController()
        {
            db = new BaseDataService<Artist>();
        }
        public ActionResult Index()
        {
            var list = db.Get();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ABMView()
        {
            var list = db.Get();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Artist artist)
        {
            this.CheckAuditPattern(artist, true);
            var list = db.ValidateModel(artist);
            if (ModelIsValid(list))
                return View(artist);
            try
            {
                db.Create(artist);
                return RedirectToAction("ABMView");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return View(artist);
            }
            
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Logger.Instance.LogException(new Exception("Id Artist null "));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artist = db.GetById(id.Value);
            if (artist == null)
            {
                Logger.Instance.LogException(new Exception("Artist HttpNotFound"));
                return HttpNotFound();
            }
            return View(artist);
        }
        [HttpPost]
        public ActionResult Edit(Artist artist)
        {
            this.CheckAuditPattern(artist);
            var list = db.ValidateModel(artist);
            if (ModelIsValid(list))
                return View(artist);
            try
            {
                db.Update(artist);
                return RedirectToAction("ABMView");

            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return View(artist);
            }

        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artist = db.GetById(id.Value);
            if (artist == null)
            {
                Logger.Instance.LogException(new Exception("Artist HttpNotFound"));
                return HttpNotFound();
            }
            try
            {
                db.Delete(artist);
                return RedirectToAction("ABMView");
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return View(artist);
            }

        }
    }
}