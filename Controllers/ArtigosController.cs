using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;


namespace FirstREST.Controllers
{
    public class ArtigosController : Controller
    {

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Artigos()
        {
            ViewBag.listaArtigos = Lib_Primavera.PriIntegration.ListaArtigos(); 
            return View();
        }

        public ActionResult Artigo(string id)
        {
            ViewBag.artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
            return View();
        }

        public ActionResult ArtigosPorCategoria(string id)
        {
            ViewBag.artigosCategoria = Lib_Primavera.PriIntegration.ListaArtigosCategoria(id);
            return View();
        }

        [System.Web.Http.HttpPost]
        public JsonResult AdicionaArtigoCarrinho([FromBody] Lib_Primavera.Model.Artigo artigo)
        {
            //try
            //{
                int clienteId = Int32.Parse(Request.Cookies["UserId"].Value);
                String productId = artigo.CodArtigo;
                String productName = artigo.DescArtigo;
                float productPrice = (float) artigo.Preco;              

                var db = new FirstREST.Models.StoreEntities();
                var art = new FirstREST.Models.Cart {ClientId = clienteId, ProductId = productId, ProductName = productName, ProductPrice = productPrice };
                db.Carts.Add(art);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
               // var msg = e.StackTrace;
                //return Json(new { success = false, msg = e.StackTrace }, JsonRequestBehavior.AllowGet);
            //}
        }
        //
        // GET: /Artigos/

       /* public IEnumerable<Lib_Primavera.Model.Artigo> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }


        // GET api/artigo/5    
        public Artigo Get(string id)
        {
            Lib_Primavera.Model.Artigo artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
            if (artigo == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }*/

    }
}

