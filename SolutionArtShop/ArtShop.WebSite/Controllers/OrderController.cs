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
        readonly BaseDataService<OrderDetail> dbDetail;
        readonly BaseDataService<Product> dbProduct;

        public OrderController()
        {
            db = new BaseDataService<Order>();
            dbItem = new BaseDataService<CartItem>();
            dbDetail = new BaseDataService<OrderDetail>();
            dbProduct = new BaseDataService<Product>();
        }
            public ActionResult Index()
        {
            var model = db.Get().OrderBy(x=>x.Id).Reverse();
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveOrder(int itemsCount, string totalPrice)
        {
            double precioTotal = Convert.ToDouble(totalPrice.Replace("$", ""));
            if (precioTotal <= 0)
            {
                ViewBag.MessageDanger = "Debe agregar items al carrito";
                return RedirectToAction("Index","home",null);
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
                    return RedirectToAction("Index", "home", null);
                try
                {
                    Order oOrderSave= db.Create(oOrder);

                    HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");

                    List<CartItem> listaItems = dbItem.Get().Where(x => x.CartId == Convert.ToInt32(cookie.Value)).ToList();

                    foreach(var item in listaItems)
                    {

                        //Actualizacion cantidad vendida de producto
                        Product oProducto = dbProduct.GetById(item.ProductId);
                        oProducto.QuantitySold += 1;
                        this.CheckAuditPattern(oProducto, true);
                        var list3 = dbProduct.ValidateModel(oProducto);

                        if (ModelIsValid(list3))
                            return RedirectToAction("Index", "home", null);

                        dbProduct.Update(oProducto);
                        //Alta Detalles de orden
                        OrderDetail oDetail = new OrderDetail()
                        {
                            OrderId = oOrderSave.Id,
                            ProductId=item.ProductId,
                            Price = item.Price,
                            Quantity = item.Quantity
                        };
                        this.CheckAuditPattern(oDetail, true);
                        var list2 = dbDetail.ValidateModel(oDetail);

                        if (ModelIsValid(list2))
                            return RedirectToAction("Index", "home", null);

                        dbDetail.Create(oDetail);
                    }



                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex, User.Identity.GetUserId());

                }

            }
            return RedirectToAction("deleteCartItemsAfterSave", "Cart");

        }

        public ActionResult Pay(int itemsCount, string totalPrice)
        {
            ViewBag.itemsCount = itemsCount;
            ViewBag.totalPrice = totalPrice;
            return View();
        }
        

    }
}