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

