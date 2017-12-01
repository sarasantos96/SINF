using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Utilizador
    {
        //Parameters for local database
        public string Username
        {
            get;
            set;
        }

        public string Pass
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Fullname
        {
            get;
            set;
        }
        public string CodCliente
        {
            get;
            set;
        }
    }
}