using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace FirstREST.Controllers
{
    public class ShoppingCartController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaCarrinho()
        {
            int userID = Int32.Parse(Request.Cookies["UserId"].Value);
            var db = new FirstREST.Models.StoreEntities();
            var cart = from m in db.Carts
                         where m.ClientId == userID
                         select m;
            double total = 0;
            foreach(var art in cart.ToList()){
                total += art.ProductPrice * art.Quantity;
            }

            FirstREST.Models.Utilizador myUser = db.Utilizadors
                .FirstOrDefault(u => u.Id == userID);

            ViewBag.cliente = myUser;
            List<Models.Cart> carrinho = new List<Models.Cart>();
            if (cart != null)
                carrinho = cart.ToList();
            ViewBag.carrinho = carrinho;
            ViewBag.total = total;
            return View();
        }

        [System.Web.Http.HttpPost]
        public JsonResult RemoveArtigo([FromBody] string id)
        {
            try
            {
                int userID = Int32.Parse(Request.Cookies["UserId"].Value);
                int rowID = Int32.Parse(id);
                var db = new FirstREST.Models.StoreEntities();
                var productCart = db.Carts
                    .FirstOrDefault(u => u.ClientId == userID && u.Id == rowID);
                db.Carts.Remove(productCart);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.StackTrace }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
