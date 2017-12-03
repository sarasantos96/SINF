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
        public JsonResult Post()
        {
            
            int userID = Int32.Parse(Request.Cookies["UserId"].Value);
            var db = new FirstREST.Models.StoreEntities();
            var cart = from m in db.Carts
                       where m.ClientId == userID
                       select m;
            var myUser = db.Utilizadors
                        .FirstOrDefault(u => u.Id == userID);

            DocVenda dv = new DocVenda();
            dv.Entidade = myUser.Username;
            List<LinhaDocVenda> linhas = new List<LinhaDocVenda>();

            foreach (var artigo in cart)
            {
                LinhaDocVenda l = new LinhaDocVenda();
                l.CodArtigo = artigo.ProductId;
                l.Quantidade = 1;
                l.Desconto = 0;
                l.PrecoUnitario = artigo.ProductPrice;
                linhas.Add(l);
            }
            dv.LinhasDoc = linhas;

            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.Encomendas_New(dv);

            if (erro.Erro == 0)
            {
                foreach (var art in cart)
                {
                    db.Carts.Remove(art);
                }
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
