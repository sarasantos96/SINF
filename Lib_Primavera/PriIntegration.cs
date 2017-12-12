using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using ADODB;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {
        
        //--------------------------------CLIENT--------------------------------------------
        # region Cliente

        //GET all clients
        public static List<Model.Cliente> ListaClientes()
        {
            
            
            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome FROM  CLIENTES");

                
                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        //GET client by id
        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {


            StdBELista objCli;


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objCli = PriEngine.Engine.Consulta("SELECT c.Cliente as ID, c.Nome as NomeCliente, c.Fac_Mor as Morada FROM CLIENTES AS c WHERE c.Cliente ='" + codCliente + "'");

                myCli.CodCliente = objCli.Valor("ID");
                myCli.Morada = objCli.Valor("Morada");
                myCli.NomeCliente = objCli.Valor("NomeCliente");
                
                return myCli;                
            }
            else
                return null;
        }

        //UPDATE client information
        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
           

            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == true)
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_Morada(cliente.Morada);
                        
                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);
                    }
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        //DELETE client
        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        //CREATE client
        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            

            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    StdBELista objCli = PriEngine.Engine.Consulta("SELECT * FROM CLIENTES AS c WHERE c.Cliente ='" + cli.CodCliente + "'");

                    if (objCli.Vazia())
                    {
                        myCli.set_Cliente(cli.CodCliente);
                        myCli.set_Nome(cli.NomeCliente);
                        myCli.set_Morada(cli.Morada);
                        myCli.set_Moeda("EUR");
                        myCli.set_ModoPag("DEP");
                        myCli.set_CondPag("2");
                        //myCli.set_CodigoPostal("4420-527");
                        myCli.set_Pais("PT");
                        myCli.set_Localidade("Porto");

                        PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);
                    }
                    
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                String s = ex.Message;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

       

        #endregion Cliente;   


        //------------------------------ PRODUCT ------------------------------------------------
        #region Artigo

        public static List<Lib_Primavera.Model.Artigo> BestSellers()
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP 10 Artigo.Descricao as Artigo, COUNT(a.Artigo) as Quantidade FROM LinhasDoc AS a LEFT JOIN Artigo ON Artigo.Artigo = a.Artigo GROUP BY Artigo.Descricao ORDER BY COUNT(a.Artigo) DESC");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.DescArtigo = objList.Valor("Artigo");
                    art.Quantidade = objList.Valor("Quantidade");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        public static List<Lib_Primavera.Model.Artigo> SearchArtigo(string query)
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT a.Artigo as ID, a.Descricao as DescArtigo, a.STKActual as STKActual, ArtigoMoeda.PVP1 as PVP, Familias.Descricao as Categoria FROM  ARTIGO AS a  LEFT JOIN ArtigoMoeda ON ArtigoMoeda.Artigo = a.Artigo LEFT JOIN Familias ON Familias.Familia = a.Familia WHERE a.Descricao LIKE '"+query+"%'");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("ID");
                    art.DescArtigo = objList.Valor("DescArtigo");
                    art.STKAtual = objList.Valor("STKActual");
                    art.Preco = objList.Valor("PVP");
                    art.Categoria = objList.Valor("Categoria");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        //GET product by id
        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            StdBELista objList;
            Model.Artigo artigo = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT a.Artigo as ID, a.Descricao as DescArtigo,a.Familia as Categoria, a.STKActual as STKActual, ArtigoMoeda.PVP1 as PVP FROM  ARTIGO AS a  LEFT JOIN ArtigoMoeda ON ArtigoMoeda.Artigo = a.Artigo WHERE a.Artigo ='" + codArtigo + "'");

                artigo.CodArtigo = objList.Valor("ID");
                artigo.DescArtigo = objList.Valor("DescArtigo");
                artigo.STKAtual = objList.Valor("STKActual");
                artigo.Preco = objList.Valor("PVP");
                artigo.Categoria = objList.Valor("Categoria");

                return artigo;
            }
            else
                return null;
        }


        //GET all products
        public static List<Model.Artigo> ListaArtigos()
        {
                        
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT a.Artigo as ID, a.Descricao as DescArtigo, a.STKActual as STKActual, ArtigoMoeda.PVP1 as PVP, Familias.Descricao as Categoria FROM  ARTIGO AS a  LEFT JOIN ArtigoMoeda ON ArtigoMoeda.Artigo = a.Artigo LEFT JOIN Familias ON Familias.Familia = a.Familia;");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("ID");
                    art.DescArtigo = objList.Valor("DescArtigo");
                    art.STKAtual = objList.Valor("STKActual");
                    art.Preco = objList.Valor("PVP");
                    art.Categoria = objList.Valor("Categoria");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }



        //GET all products from category
        public static List<Model.Artigo> ListaArtigosCategoria(String codCategoria)
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT a.Artigo as ID, a.Descricao as DescArtigo, a.STKActual as STKActual, ArtigoMoeda.PVP1 as PVP, Familias.Descricao as Categoria FROM  ARTIGO AS a  LEFT JOIN ArtigoMoeda ON ArtigoMoeda.Artigo = a.Artigo LEFT JOIN Familias ON Familias.Familia = a.Familia WHERE Familias.Familia ='"+codCategoria+"'");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("ID");
                    art.DescArtigo = objList.Valor("DescArtigo");
                    art.STKAtual = objList.Valor("STKActual");
                    art.Preco = objList.Valor("PVP");
                    art.Categoria = objList.Valor("Categoria");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        //GET all products from category
        public static List<Model.Artigo> ListaTop4ArtigosCategoria(String codCategoria, String id)
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP 4 a.Artigo as ID, a.Descricao as DescArtigo, a.STKActual as STKActual, ArtigoMoeda.PVP1 as PVP, Familias.Descricao as Categoria FROM  ARTIGO AS a  LEFT JOIN ArtigoMoeda ON ArtigoMoeda.Artigo = a.Artigo LEFT JOIN Familias ON Familias.Familia = a.Familia WHERE Familias.Familia ='" + codCategoria + "'" +" AND a.Artigo !='"+id+"'");

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("ID");
                    art.DescArtigo = objList.Valor("DescArtigo");
                    art.STKAtual = objList.Valor("STKActual");
                    art.Preco = objList.Valor("PVP");
                    art.Categoria = objList.Valor("Categoria");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        #endregion Artigo
        
       
        //--------------------------- CATEGORIES ----------------------------

        #region Categoria

        public static List<Lib_Primavera.Model.Categoria> BestSellersCategories()
        {
            StdBELista objList;

            Model.Categoria art = new Model.Categoria();
            List<Model.Categoria> listArts = new List<Model.Categoria>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP 5 Familias.Descricao as familia, Count(a.Artigo) as quantidade FROM LinhasDoc AS a JOIN Artigo ON Artigo.Artigo = a.Artigo LEFT JOIN Familias ON Artigo.Familia = Familias.Familia GROUP BY Familias.Descricao ORDER BY COUNT(a.Artigo) DESC");

                while (!objList.NoFim())
                {
                    art = new Model.Categoria();
                    art.NomeCategoria = objList.Valor("familia");
                    art.Vendas = objList.Valor("quantidade");
                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        //GET all categories
        public static List<Model.Categoria> ListaCategorias()
        {
            StdBELista objList;

            Model.Categoria arm = new Model.Categoria();
            List<Model.Categoria> listArms = new List<Model.Categoria>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                objList = PriEngine.Engine.Consulta("SELECT Familia as Categoria, Descricao as Nome FROM FAMILIAS");


                while (!objList.NoFim())
                {
                    listArms.Add(new Model.Categoria
                    {
                        CodCategoria = objList.Valor("Categoria"),
                        NomeCategoria = objList.Valor("Nome"),
                    });
                    objList.Seguinte();

                }

                return listArms;

            }
            else
            {
                return null;

            }
        }

        //GET category by id
        public static Model.Categoria GetCategoria(string codCategoria)
        {

            StdBELista objList;

            Model.Categoria cat = new Model.Categoria();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT f.Familia as Categoria, f.Descricao as Nome FROM FAMILIAS AS f WHERE f.Familia ='" + codCategoria + "'");

                cat.CodCategoria = objList.Valor("Categoria");
                cat.NomeCategoria = objList.Valor("Nome");

                return cat;
            }
            else
            {
                return null;

            }

        }


        #endregion Categoria


        //---------------------- WAREHOUSES-----------------------------------
        #region Armazem


        //GET all warehouses
        public static List<Model.Armazem> ListaArmazens()
        {

            StdBELista objList;

            Model.Armazem arm = new Model.Armazem();
            List<Model.Armazem> listArms = new List<Model.Armazem>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                objList = PriEngine.Engine.Consulta("SELECT Armazem, Descricao, Morada, Localidade FROM  ARMAZENS");


                while (!objList.NoFim())
                {
                    listArms.Add(new Model.Armazem
                    {
                       CodArmazem = objList.Valor("Armazem"),
                       Morada = objList.Valor("Morada"),
                       Descricao = objList.Valor("Descricao"),
                       Localidade = objList.Valor("Localidade"),
                    });
                    objList.Seguinte();

                }

                return listArms;

            }
            else
            {
                return null;

            }

        }

        //GET warehouse by id
        public static Model.Armazem GetArmazem(string codArmazem)
        {

            StdBELista objList;

            Model.Armazem arm = new Model.Armazem();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT a.Armazem as CodArmazem, a.Descricao as Descricao, a.Morada as Morada, a.Localidade as Localidade FROM  ARMAZENS AS a WHERE a.Armazem ='"+codArmazem+"'");

                arm.CodArmazem = objList.Valor("CodArmazem");
                arm.Morada = objList.Valor("Morada");
                arm.Descricao = objList.Valor("Descricao");
                arm.Localidade = objList.Valor("Localidade");

                return arm;
            }
            else
            {
                return null;

            }

        }


        //GET all products from warehouse
        public static List<Model.Artigo> ListaProdutosArmazem(string codArmazem)
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Artigo, StkActual FROM ArtigoArmazem  WHERE Armazem ='" + codArmazem + "' ORDER BY Artigo");

                art = new Model.Artigo();
                art.CodArtigo = objList.Valor("Artigo");
                art.STKAtual = objList.Valor("StkActual");
                listArts.Add(art);
                objList.Seguinte();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("Artigo");
                    art.STKAtual = objList.Valor("StkActual");
                    if (listArts[listArts.Count - 1].CodArtigo == art.CodArtigo)
                    {
                        listArts[listArts.Count - 1].STKAtual += art.STKAtual;
                    }
                    else
                    {
                        listArts.Add(art);
                    }


                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }
        }

        public static List<Lib_Primavera.Model.Armazem> ListaArmazensProduto(string codArtigo)
        {
            StdBELista objList;

            Model.Armazem arm = new Model.Armazem();
            List<Model.Armazem> listArms = new List<Model.Armazem>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                objList = PriEngine.Engine.Consulta("SELECT Armazens.Descricao as Descricao , SUM(StkActual) as STKAtual FROM ArtigoArmazem LEFT JOIN Armazens ON Armazens.Armazem = ArtigoArmazem.Armazem WHERE Artigo = '"+codArtigo+"' GROUP BY Armazens.Descricao;");


                while (!objList.NoFim())
                {
                    listArms.Add(new Model.Armazem
                    {
                        Descricao = objList.Valor("Descricao"),
                        STKAtual = objList.Valor("STKAtual"),
                    });
                    objList.Seguinte();

                }

                return listArms;

            }
            else
            {
                return null;

            }
        }

        #endregion Armazem

     
        //-----------------------ORDERS------------------------------------------
        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {
                
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;
                    
                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }

                
        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            

            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra



        //ENCOMENDAS CLIENTES
        #region DocsVenda

        //POST create a new Order
        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    myEnc.set_DataDoc(DateTime.Now);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie("2017");
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                erro.Descricao = ex.Message;
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                return erro;
            }
        }

        //GET all orders
        public static List<Model.DocVenda> Encomendas_List()
        {
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }

        //GET order by numDoc
        public static List<Model.DocVenda> Encomenda_Get(string numDoc)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT a.id, a.Entidade, a.Data, a.NumDoc, a.TotalMerc, a.Serie From CabecDoc AS a where a.TipoDoc='ECL' and a.id='"+numDoc+"'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }

        //GET all client orders
        public static List<Model.DocVenda> getEncomendasCliente(string CodCliente)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Entidade='" + CodCliente + "' ORDER BY Data DESC");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    objListLin = PriEngine.Engine.Consulta("SELECT Estado FROM CabecDocStatus WHERE IdCabecDoc='" + dv.id + "'");
                    dv.Estado = objListLin.Valor("Estado");
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        } 

        //GET order status 
        public static string getEstadoEncomenda(string idCabDoc)
        {
            StdBELista objList;

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT Estado FROM CabecDocStatus WHERE IdCabecDoc='"+idCabDoc+"'");

                return objList.Valor("Estado");
            }
            else
            {
                return null;

            }
        }
        #endregion DocsVenda
    }
}
       