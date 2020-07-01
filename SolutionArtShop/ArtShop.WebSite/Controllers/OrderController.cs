using ArtShop.Data.Model;
using ArtShop.Data.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OdeToFood.WebSite.Controllers;
using OdeToFood.WebSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Order

        readonly BaseDataService<Order> db;

        public OrderController()
        {
            db = new BaseDataService<Order>();
        }
            public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveOrder(int itemsCount, string totalPrice)
        {
            double precioTotal = Convert.ToDouble(totalPrice.Replace("$", ""));
            if (precioTotal <= 0)
            {
                ViewBag.MessageDanger = "Debe agregar items al carrito";
                return Redirect(Request.UrlReferrer.AbsoluteUri.ToString());
            }
            else
            {
                Order oOrder = new Order(){
                    UserId=User.Identity.GetUserId(),
                    OrderDate = DateTime.Now,
                    OrderNumber = 1,//DESHARCODEAR
                    ItemCount = itemsCount,
                    TotalPrice = precioTotal

                };
                this.CheckAuditPattern(oOrder, true);
                var list = db.ValidateModel(oOrder);

                if (ModelIsValid(list))
                    return Redirect(Request.UrlReferrer.AbsoluteUri.ToString());
                try
                {
                    db.Create(oOrder);
                    return RedirectToAction("deleteCartItems", "Cart");
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);

                }

            }
            return Redirect(Request.UrlReferrer.AbsoluteUri.ToString());

        }


    }
}