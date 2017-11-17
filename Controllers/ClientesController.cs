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
    public class ClientesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            ViewBag.listaClientes = Lib_Primavera.PriIntegration.ListaClientes();
            return View();
        }

        public ActionResult Cliente(string id)
        {
            ViewBag.cliente = Lib_Primavera.PriIntegration.GetCliente(id);
            ViewBag.encomendasCliente= Lib_Primavera.PriIntegration.getEncomendasCliente(id);

            return View();
        }

        public ActionResult NovoCliente()
        {
            return View();
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Cliente cliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdCliente(cliente);
                if (erro.Erro == 0)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }

            catch (Exception exc)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Lib_Primavera.Model.Cliente cliente)
        {
            String cod = cliente.CodCliente;
            String name = cliente.NomeCliente;
            String numContribuite = cliente.NumContribuinte;
            String morada = cliente.Morada;
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereClienteObj(cliente);

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
        
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete(string id)
        {


            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {

                erro = Lib_Primavera.PriIntegration.DelCliente(id);

                if (erro.Erro == 0)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }

            }

            catch (Exception exc)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;

            }

        }
        
    }
}
