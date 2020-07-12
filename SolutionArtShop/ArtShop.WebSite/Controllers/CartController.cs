using ArtShop.Data.Model;
using ArtShop.Data.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OdeToFood.WebSite.Controllers;
using OdeToFood.WebSite.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.WebSite.Controllers
{
    public class CartController : BaseController
    {
        readonly BaseDataService<CartItem> dbItem;
        readonly BaseDataService<Cart> db;
        readonly BaseDataService<Product> dbProduct;

        public CartController()
        {
            db = new BaseDataService<Cart>();
            dbItem = new BaseDataService<CartItem>();
            dbProduct = new BaseDataService<Product>();
        }
        // GET: Cart
        [Authorize]
        public ActionResult Index()
        {
            if (Request.Cookies["cookieCart"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");

                List<CartItem> listaItems = dbItem.Get().Where(x=>x.CartId==Convert.ToInt32(cookie.Value)).ToList();

                foreach(var item in listaItems)
                {
                    item._Product = dbProduct.GetById(item.ProductId);
                }

                return View(listaItems);
            }
            return RedirectToAction("index", "Home");

        }
        [HttpPost]
        public ActionResult AddToCart(int? Id)
        {
            Product oPaint = dbProduct.GetById(Convert.ToInt32(Id));
            if (Id == null)
            {
                Logger.Instance.LogException(new Exception("Id Cart null "), User.Identity.GetUserId());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request.Cookies.Get("cookieCart")==null)
            {
                HttpCookie cookie = new HttpCookie("cookieCart");
                Cart oCart = new Cart();

                CartItem oCartItem =new CartItem()
                {
                    ProductId = Convert.ToInt32(Id),
                    Price = oPaint.Price,
                    Quantity = 1,
                    _Product = oPaint
                };
                
                //Seteo datos de cart
                oCart.ItemCount = 1;
                var format = "dd/MM/yyyy HH:mm:ss";
                var hoy = DateTime.Now.ToString(format);
                var dateTime = DateTime.ParseExact(hoy, format, CultureInfo.InvariantCulture);
                oCart.CartDate = dateTime;
                oCart.Cookie = "";
               
                
                    
                this.CheckAuditPattern(oCart, true);
                var list = db.ValidateModel(oCart);

                if (ModelIsValid(list))
                    return RedirectToAction("itemProduct", "Product", new { Id });
                try
                {
                    //Obtengo el id del carritoCreado
                    Cart oCartSave = db.Create(oCart);
                    
                    cookie.Value = oCartSave.Id.ToString();
                    oCartSave.Cookie = cookie.Value;

                    //Actualizo el carrito con la cookie
                    db.Update(oCartSave);

                    //Genero la cookie
                    Response.Cookies.Add(cookie);

                    //Guardo el id del carrito en el item
                    oCartItem.CartId = oCartSave.Id;
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex, User.Identity.GetUserId());

                }

                this.CheckAuditPattern(oCartItem, true);
                var list2 = dbItem.ValidateModel(oCartItem);

               

                


                if (ModelIsValid(list2))
                    return RedirectToAction("itemProduct", "Product", new { Id });
                try
                {
                    //Guardo el item
                    dbItem.Create(oCartItem);
                    
                    
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex, User.Identity.GetUserId());
                }
                    
               
               
                
            }
            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");
                List<CartItem> listaItems =dbItem.Get().Where(x => x.CartId == Convert.ToInt32(cookie.Value)).ToList();
                 
                CartItem oCartItem = new CartItem()
                {
                    ProductId = Convert.ToInt32(Id),
                    Price = oPaint.Price,
                    Quantity = 1,
                    CartId= Convert.ToInt32(cookie.Value),
                    _Product = oPaint
                };
                
                this.CheckAuditPattern(oCartItem, true);
                var list2 = dbItem.ValidateModel(oCartItem);


                //Actualizo cantidad de items del carrito 
                Cart oCart = db.GetById(Convert.ToInt32(cookie.Value));
                oCart.ItemCount += 1;

                this.CheckAuditPattern(oCart, true);
                var list3 = db.ValidateModel(oCart);

                if (ModelIsValid(list2))
                    return RedirectToAction("itemProduct", "Product", new { Id });
                try
                {
                    //Guardo el item
                    dbItem.Create(oCartItem);
                    db.Update(oCart);
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex, User.Identity.GetUserId());
                }

            }

            return RedirectToAction("itemProduct", "Product", new { Id });
        }

        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cartItem = dbItem.GetById(id.Value);
            if (cartItem == null)
            {
                Logger.Instance.LogException(new Exception("CartItem HttpNotFound"), User.Identity.GetUserId());
                return HttpNotFound();
            }
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");
                Cart oCart = db.GetById(Convert.ToInt32(cookie.Value));
                oCart.ItemCount -= 1;

                dbItem.Delete(cartItem);
                db.Update(oCart);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex, User.Identity.GetUserId());
                ViewBag.MessageDanger = ex.Message;
                return RedirectToAction("Index");
            }

        }

        public ActionResult getPrice()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");
            var precio = 0.0;
            if (cookie != null)
            {
                List<CartItem> listaItems = dbItem.Get().Where(x=>x.CartId==Convert.ToInt32(cookie.Value)).ToList();

                foreach (var item in listaItems)
                {
                    precio = precio + item.Price;

                }
            }
            
            return Json(new { response = precio }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deleteCartItems()
        {
            
            if (Request.Cookies["cookieCart"] != null)
            {

                Response.Cookies["cookieCart"].Expires = DateTime.Now.AddDays(-1);

                HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");

                List<CartItem> listaItems = dbItem.Get().Where(x => x.CartId == Convert.ToInt32(cookie.Value)).ToList();

                foreach (var item in listaItems)
                {
                    dbItem.Delete(item);

                }
                Cart oCart = db.GetById(Convert.ToInt32(cookie.Value));
                db.Delete(oCart);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult deleteCartItemsAfterSave()
        {

            if (Request.Cookies["cookieCart"] != null)
            {

                Response.Cookies["cookieCart"].Expires = DateTime.Now.AddDays(-1);

                HttpCookie cookie = HttpContext.Request.Cookies.Get("cookieCart");

                List<CartItem> listaItems = dbItem.Get().Where(x => x.CartId == Convert.ToInt32(cookie.Value)).ToList();

                foreach (var item in listaItems)
                {
                    dbItem.Delete(item);

                }
                Cart oCart = db.GetById(Convert.ToInt32(cookie.Value));
                db.Delete(oCart);
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
        

    }
}