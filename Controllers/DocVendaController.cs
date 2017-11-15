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
    public class DocVendaController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocVendas()
        {
            ViewBag.listaEncomendas = Lib_Primavera.PriIntegration.Encomendas_List();
            return View();
        }

        public ActionResult DocVenda(string id)
        {
            ViewBag.docVenda = Lib_Primavera.PriIntegration.Encomenda_Get(id);
            return View();
        }

        public ActionResult ClienteEncomendas(string id)
        {
            ViewBag.encomendasCliente = Lib_Primavera.PriIntegration.getEncomendasCliente(id);
            return View();
        }

        public string EstadoEncomenda(string id)
        {
            return Lib_Primavera.PriIntegration.getEstadoEncomenda(id);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.Encomendas_New(dv);

            if (erro.Erro == 0)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }

            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }

        }
    }
}
