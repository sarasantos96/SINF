using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class ArtigoArmazem
    {
        public Artigo Artigo
        {
            get;
            set;
        }

        public List<Armazem> Armazens
        {
            get;
            set;
        }

        public List<KeyValuePair<String, int>> ArmazemStock
        {
            get;
            set;
        } 


    }
}
