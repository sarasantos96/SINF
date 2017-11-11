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
    public class CategoriaController : ApiController
    {
        //
        // GET: /Categoria/
        public IEnumerable<Lib_Primavera.Model.Categoria> Get()
        {
            return Lib_Primavera.PriIntegration.ListaCategorias();
        }

        // GET api/Categoria/5    
        public Categoria Get(string id)
        {
            Lib_Primavera.Model.Categoria categoria = Lib_Primavera.PriIntegration.GetCategoria(id);
            if (categoria == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return categoria;
            }
        }

    }
}
