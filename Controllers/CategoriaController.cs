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
    public class CategoriaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categorias()
        {
            ViewBag.listaCategorias = Lib_Primavera.PriIntegration.ListaCategorias();
            return View();
        }

        public ActionResult Categoria(string id)
        {
            ViewBag.categoria = Lib_Primavera.PriIntegration.GetCategoria(id);
            return View();
        }


    }
}
