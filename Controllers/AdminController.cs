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

        /*public ActionResult ArtigosStock()
        {
            ViewBag.listaArmazens = Lib_Primavera.PriIntegration.ListaProdutosArmazem(); 
            return View();
        }*/

        public ActionResult ArtigosStock()
        {
            ViewBag.listaArmazens = Lib_Primavera.PriIntegration.ListaArmazens();
            return View();
        }

        public ActionResult ArmazensArtigo(string id)
        {
            ViewBag.armazem = Lib_Primavera.PriIntegration.GetArmazem(id);
            ViewBag.listaArtigos = Lib_Primavera.PriIntegration.ListaProdutosArmazem(id);
            return View();
        }

    }
}
