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
    public class ArmazensController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Armazens()
        {
            ViewBag.listaArmazens = Lib_Primavera.PriIntegration.ListaArmazens();
            return View();
        }

        public ActionResult Armazem(string id)
        {
            ViewBag.armazem = Lib_Primavera.PriIntegration.GetArmazem(id);
            return View();
        }
        /*
        //
        // GET: /Armazens/
        public IEnumerable<Lib_Primavera.Model.Armazem> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArmazens();
        }


        // GET api/Armazens/5    
        public Armazem Get(string id)
        {
            Lib_Primavera.Model.Armazem armazem = Lib_Primavera.PriIntegration.GetArmazem(id);
            if (armazem == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return armazem;
            }
        }*/

    }
}
