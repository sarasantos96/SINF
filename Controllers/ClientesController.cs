using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

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

        public ActionResult Cliente()
        {
            int userID = Int32.Parse(Request.Cookies["UserId"].Value);
            var db = new FirstREST.Models.StoreEntities();
            FirstREST.Models.Utilizador myUser = db.Utilizadors
                .FirstOrDefault(u => u.Id == userID);

            ViewBag.cliente = myUser;
            ViewBag.encomendasCliente= Lib_Primavera.PriIntegration.getEncomendasCliente(myUser.Username);

            return View();
        }

        public ActionResult NovoCliente()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult LogIn()
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
        public JsonResult CreateUtilizador([FromBody] Lib_Primavera.Model.Utilizador cliente)
        {
            try
            {
                String username = cliente.Username;
                String password = cliente.Pass;
                String email = cliente.Email;
                String fullname = cliente.Fullname;
                String codCliente = username;
                String address = cliente.Address;
                int taxPayerNumber = cliente.TaxPayerNumber;

                var db = new FirstREST.Models.StoreEntities();
                var blog = new FirstREST.Models.Utilizador { Email = email, Pass = password, Username = username, Fullname = fullname, CodCliente = codCliente, TaxPayerNumber = taxPayerNumber, Address = address  };
                db.Utilizadors.Add(blog);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.StackTrace }, JsonRequestBehavior.AllowGet);
            }

        }

        public RedirectResult LogOut()
        {
            if (Request.Cookies["UserId"] != null)
            {              
                var c = new HttpCookie("UserId");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return Redirect("~/");
        }

        [System.Web.Http.HttpPost]
        public JsonResult CheckLogIn([FromBody] Lib_Primavera.Model.Utilizador cliente)
        {
            try
            {
                String username = cliente.Username;
                String password = cliente.Pass;

                if (username.Equals("admin") && password.Equals("admin1234"))
                {
                    string idvalue = "admin";
                    Response.Cookies["UserId"].Value = idvalue;
                    return Json(new { success = true, msg="admin"}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //The ".FirstOrDefault()" method will return either the first matched
                    //result or null
                    var db = new FirstREST.Models.StoreEntities();
                    var myUser = db.Utilizadors
                        .FirstOrDefault(u => u.Username == username
                                     && u.Pass == password);
                    if (myUser == null)
                    {
                        throw new InvalidOperationException();
                    }

                    string idvalue = myUser.Id.ToString();
                    Response.Cookies["UserId"].Value = idvalue;
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }              
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpPost]
        public JsonResult Post([FromBody] Lib_Primavera.Model.Cliente cliente)
        {
            String cod = cliente.CodCliente;
            String name = cliente.NomeCliente;
            String numContribuite = cliente.NumContribuinte;
            String morada = cliente.Morada;

            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereClienteObj(cliente);

            if (erro.Erro == 0)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
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
