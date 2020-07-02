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
using System.Web.Helpers;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Order

        readonly BaseDataService<Order> db;
        readonly BaseDataService<CartItem> dbItem;

        public OrderController()
        {
            db = new BaseDataService<Order>();
            dbItem = new BaseDataService<CartItem>();
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
                    //return RedirectToAction("deleteCartItems", "Cart");

                    //Eliminamos los items 

                    if (Request.Cookies["cookieCart"] != null)
                    {

                        Response.Cookies["cookieCart"].Expires = DateTime.Now.AddDays(-1);

                        HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");

                        List<CartItem> listaItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);

                        foreach (var item in listaItems)
                        {
                            dbItem.Delete(item);

                        }
                    }
                    return Redirect(Request.UrlReferrer.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);

                }

            }
            return Redirect(Request.UrlReferrer.ToString());

        }


    }
}