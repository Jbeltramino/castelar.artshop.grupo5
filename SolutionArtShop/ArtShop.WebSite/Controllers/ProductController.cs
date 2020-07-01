using ArtShop.Data.Model;
using ArtShop.Data.Services;
using OdeToFood.WebSite.Controllers;
using OdeToFood.WebSite.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class ProductController : BaseController
    {

        readonly BaseDataService<Product> db;
        readonly BaseDataService<Artist> dbArtist ;

        public ProductController()
        {
            db = new BaseDataService<Product>();
            dbArtist = new BaseDataService<Artist>();
        }
        public ActionResult Index()
        {
            var model = db.Get().OrderBy(x=>x.Id);
            return View(model);
        }
        public ActionResult itemProduct(int? id)
        {
            if (id == null)
            {
                Logger.Instance.LogException(new Exception("Id Product null "));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.GetById(id.Value);
            if (product == null)
            {
                Logger.Instance.LogException(new Exception("Product HttpNotFound"));
                return HttpNotFound();
            }
            return View(product);
            
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ABMView()
        {
            var list = db.Get();
            foreach(var item in list)
            {
                item.Artista = dbArtist.GetById(Convert.ToInt32(item.ArtistID)); 
            }
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            


            //List<SelectListItem> listItems = new List<SelectListItem>();
            //foreach (var item in artistas)
            //{
            //    listItems.Add(new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() });
            //}
            //ViewBag.artistas = listItems;

            Product model = new Product()
            {
                Artistas = dbArtist.Get()
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Product pintura)
        {
            this.CheckAuditPattern(pintura, true);
            var list = db.ValidateModel(pintura);
            if (ModelIsValid(list))
                return View(pintura);
            try
            {
                //HttpPostedFileBase file = Request.Files["Image"];
                //var fileName = String.Empty;
                //fileName = Path.GetFileName(file.FileName);
               
                //fileName = fileName.Substring(0, fileName.IndexOf('.')) + "_" + DateTime.Now.Millisecond + "- " + DateTime.Now.Second + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Hour + "." + fileName.Substring(fileName.IndexOf('.') + 1);
                
                //var uploadDir = "~/Content/Images";
                
                //var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName);
                
                //file.SaveAs(imagePath);
                db.Create(pintura);
                return RedirectToAction("ABMView");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return View(pintura);
            }

        }

        public ActionResult Edit(int? id)
        {
            BaseDataService<Artist> dbArtist = new BaseDataService<Artist>();
            if (id == null)
            {
                Logger.Instance.LogException(new Exception("Id Paint null "));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pintura = db.GetById(id.Value);
            if (pintura == null)
            {
                Logger.Instance.LogException(new Exception("Paint HttpNotFound"));
                return HttpNotFound();
            }
            pintura.Artistas = dbArtist.Get();
            return View(pintura);
        }
        [HttpPost]
        public ActionResult Edit(Product pintura)
        {
            this.CheckAuditPattern(pintura);
            var list = db.ValidateModel(pintura);
            if (ModelIsValid(list))
                return View(pintura);
            try
            {
                db.Update(pintura);
                return RedirectToAction("ABMView");

            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return View(pintura);
            }

        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pintura = db.GetById(id.Value);
            if (pintura == null)
            {
                Logger.Instance.LogException(new Exception("Paint HttpNotFound"));
                return HttpNotFound();
            }
            try
            {
                db.Delete(pintura);
                return RedirectToAction("ABMView");
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ViewBag.MessageDanger = ex.Message;
                return RedirectToAction("ABMView");
            }

        }
    }
}