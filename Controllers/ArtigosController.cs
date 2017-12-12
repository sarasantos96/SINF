using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.IO;

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
            Lib_Primavera.Model.Artigo artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
            List<Lib_Primavera.Model.Artigo> recomendados = Lib_Primavera.PriIntegration.ListaTop4ArtigosCategoria(artigo.Categoria,artigo.CodArtigo);
            List<Lib_Primavera.Model.Armazem> armazens = Lib_Primavera.PriIntegration.ListaArmazensProduto(id);
            ViewBag.artigo = artigo;
            ViewBag.recomendados = recomendados;
            ViewBag.armazens = armazens;
            ViewBag.categoria = Lib_Primavera.PriIntegration.GetCategoria(artigo.Categoria);
            return View();
        }

        public ActionResult ArtigosPorCategoria(string id)
        {
            ViewBag.categoria = Lib_Primavera.PriIntegration.GetCategoria(id);
            ViewBag.listaCategorias = Lib_Primavera.PriIntegration.ListaCategorias();
            ViewBag.artigosCategoria = Lib_Primavera.PriIntegration.ListaArtigosCategoria(id);
            return View();
        }

        [System.Web.Http.HttpPost]
        public JsonResult AdicionaArtigoCarrinho([FromBody] Lib_Primavera.Model.Artigo artigo)
        {
            try
            {
                int clienteId = Int32.Parse(Request.Cookies["UserId"].Value);
                String productId = artigo.CodArtigo;
                String productName = artigo.DescArtigo;
                float productPrice = (float) artigo.Preco;
                int quantity = artigo.Quantidade;

                var db = new FirstREST.Models.StoreEntities();
                var art = new FirstREST.Models.Cart {ClientId = clienteId, ProductId = productId, ProductName = productName, ProductPrice = productPrice, Quantity = quantity };
                db.Carts.Add(art);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var msg = e.StackTrace;
                return Json(new { success = false, msg = e.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult SearchResults(string query)
        {
            List<Lib_Primavera.Model.Artigo> results = Lib_Primavera.PriIntegration.SearchArtigo(query);
            ViewBag.results = results;
            ViewBag.query = query;
            return View();
        }

    }
}

