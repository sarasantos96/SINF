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
    public class ValuesController : Controller
    {
        // GET api/values
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Values()
        {
            ViewBag.listaValues = new string[] { "value1", "value2" };
            return View();
        }

        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/


        // GET api/values/5
        public ActionResult Value()
        {
            ViewBag.value = "value";
            return View();
        }

        /*public string Get(int id)
        {
            return "value";
        }*/



        //  ----- POST

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}