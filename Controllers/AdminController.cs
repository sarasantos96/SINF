using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArtigosStock()
        {
            ViewBag.listaArtigos = Lib_Primavera.PriIntegration.ListaArtigos();
            return View();
        }

    }
}
