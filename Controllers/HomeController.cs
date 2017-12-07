using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listaCategorias = Lib_Primavera.PriIntegration.ListaCategorias();
            return View();
        }
    }
}
