﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Artigo
    {
        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }

        public double STKAtual
        {
            get;
            set;
        }

        public string Categoria
        {
            get;
            set;
        }

        public double Preco
        {
            get;
            set;
        }

        public int Quantidade
        {
            get;
            set;
        }

    }
}