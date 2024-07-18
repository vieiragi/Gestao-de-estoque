using System;
using System.Globalization;
using System.Data;
using System.Collections.Generic;
using BrandsGoods.Data;
using BrandsGoods.Menus;
using System.Linq;
using BrandsGoods.Utils;
using System.IO;
using Newtonsoft.Json;

namespace BrandsGoods.Entities
{
    /// <summary>
    /// Classe de Produtos (Articles)
    /// </summary>
    public class Articles : IEntities
    {
        private readonly static string TableName = "Articles";
        private readonly static string[] TableColumns = { "articleID", "articleName", "articlePrice", "articleAmount", "Situacao" };
        private readonly static string ViewName = "vwArticles";
        private readonly static string[] ViewColumns = { "ID", "Nome", "Preco", "Qtd", "Situacao" };
        private bool IsNew;
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public decimal ArticlePrice { get; set; }
        public int ArticleAmount { get; set; }
        public string Situacao { get; set; }

        /// <summary>
        /// Construtor Vazio
        /// </summary>
        public Articles()
        {
            IsNew = true;
            ArticleName = "Novo Produto";
            ArticlePrice = 0;
            ArticleAmount = 0;
        }
        /// <summary>
        /// Construtor por ID
        /// </summary>
        /// <param name="articleID"></param>
        public Articles(int articleID)
        {
            GetById(articleID);
        }
        /// <summary>
        /// Construtor completo
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="articleName"></param>
        /// <param name="articlePrice"></param>
        /// <param name="articleAmount"></param>
        /// <param name="situacao"></param>
        private Articles(int articleID, string articleName, decimal articlePrice, int articleAmount, string situacao = "")
        {
            IsNew = false;
            ArticleID = articleID;
            ArticleName = articleName;
            ArticlePrice = articlePrice;
            ArticleAmount = articleAmount;
            if (string.IsNullOrEmpty(situacao)) SetSituacao();
            else Situacao = situacao;

        }

        /// <summary>
        /// Obter item pela ID
        /// </summary>
        /// <param name="articleID"></param>
        public void GetById(int? articleID = 0)
        {
            if (articleID == 0) articleID = ArticleID;

            DbBrandsGoods db = new DbBrandsGoods();

            string sql = db.SqlSelect(TableColumns, TableName, "", $" AND articleID={articleID}");
            DataTable dt = db.SqlToDataTable(sql);

            IsNew = false;
            ArticleID = (int)dt.Rows[0]["articleID"];
            ArticleName = (string)dt.Rows[0]["articleName"];
            ArticlePrice = (decimal)dt.Rows[0]["articlePrice"];
            ArticleAmount = (int)dt.Rows[0]["articleAmount"];
        }

        /// <summary>
        /// Altera a situação do estoque
        /// </summary>
        /// <param name="situacao"></param>
        public void SetSituacao(string? situacao = "")
        {
            if (!string.IsNullOrEmpty(situacao))
            {
                if (situacao != "Sem Estoque")
                {
                    Situacao = "Em Estoque";
                    return;
                }
                Situacao = situacao;
                return;
            }
            if (ArticleAmount <= 0) Situacao = "Sem Estoque";
            if (ArticleAmount > 0) Situacao = "Em Estoque";
        }

        /// <summary>
        /// Altera a quantidade em estoque
        /// </summary>
        /// <param name="amount"></param>
        public void SetAmount(int amount)
        {
            if (IsNew) return;

            // Checando permissões
            int[] level = { 0, 1 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            ArticleAmount = ArticleAmount + amount;
            SetSituacao();
        }

        /// <summary>
        /// Criar um novo registro
        /// </summary>
        public void Create()
        {
            if (!IsNew) return;
            // Checando permissões
            int[] level = { 0, 1 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            // Executando cadastro
            try
            {
                DbBrandsGoods db = new DbBrandsGoods();

                // Get Last ID
                int articleID;
                articleID = Convert.ToInt32(db.RetMaxId("articleID", TableName)) + 1;

                SetSituacao();

                // Insert New Record
                string sql = $"INSERT INTO {TableName} VALUES ({articleID}, '{ArticleName}', {ArticlePrice.ToString("G", CultureInfo.InvariantCulture)}, {ArticleAmount}, '{Situacao}')";
                db.ExecSqlQuery(sql);

                Mensagem.Operacao("C");
            }

            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
            }
        }

        /// <summary>
        /// Atualizar o registro
        /// </summary>
        public void Update()
        {
            if (IsNew) return;

            // Checando permissões
            int[] level = { 0, 1 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            // Executando atualização
            try
            {
                DbBrandsGoods db = new DbBrandsGoods();

                // Update Record
                if (string.IsNullOrEmpty(Situacao)) SetSituacao();

                string sql = $"UPDATE T SET articleName = '{ArticleName}', articlePrice = {ArticlePrice.ToString("G", CultureInfo.InvariantCulture)}, articleAmount = {ArticleAmount}, Situacao = '{Situacao}' FROM {TableName} T WHERE articleID = {ArticleID}";
                db.ExecSqlQuery(sql);

            }

            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
            }
        }

        /// <summary>
        /// Exclui o registro
        /// </summary>
        public void Delete()
        {
            if (IsNew) return;
            // Checando permissões
            int[] level = { 0 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            // Executando exclusão
            try
            {
                DbBrandsGoods db = new DbBrandsGoods();

                // Delete Record
                string sql = $"DELETE FROM {TableName} WHERE articleID = {ArticleID}";
                db.ExecSqlQuery(sql);

                Mensagem.Operacao("D");
            }

            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
            }
        }

        /// <summary>
        /// Obtem a tabela com dados da classe
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetTable(string where = "")
        {
            DbBrandsGoods db = new DbBrandsGoods();

            string sql = db.SqlSelect(ViewColumns, ViewName, "", where);
            return db.SqlToDataTable(sql);
        }

        /// <summary>
        /// Obtem a lista com dados da classe
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<Articles> GetList(string where = "")
        {
            DataTable dt = GetTable(where);

            List<Articles> ls = new List<Articles>();

            foreach (DataRow r in dt.Rows)
            {
                Articles item = new Articles(
                    Convert.ToInt32(r["ID"]),
                    Convert.ToString(r["Nome"]),
                    Convert.ToInt32(r["Preco"]),
                    Convert.ToInt32(r["Qtd"]),
                    Convert.ToString(r["Situacao"])
                    );

                ls.Add(item);
            }

            return ls;
        }

        /// <summary>
        /// Filtrar a lista - criado para exemplificar o LINQ
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public static List<Articles> FilterList(List<Articles> ls)
        {
            List<Articles> filterList = new List<Articles>();
            string opt;
            string criterio = "";
            decimal valorInicial;
            decimal valorFinal;
            bool loop = true;

            while (loop)
            {
                Console.WriteLine("\nSelecione o filtro desejado :");
                Console.WriteLine("1- Por ID:");
                Console.WriteLine("2- Por Nome:");
                Console.WriteLine("3- Por Valor:");
                Console.WriteLine("4- Por Estoque:");
                Console.WriteLine();

                opt = Console.ReadLine();
                try
                {
                    switch (opt)
                    {
                        case "1":
                            Console.WriteLine("\nDigite ID desejado:");
                            criterio = Console.ReadLine();
                            filterList = ls.Where(item => item.ArticleID.Equals(Convert.ToInt32(criterio))).ToList();
                            loop = false;
                            break;

                        case "2":
                            Console.WriteLine("\nDigite nome desejado:");
                            criterio = Console.ReadLine();
                            filterList = ls.Where(item => item.ArticleName.ToLower().Contains(criterio.ToLower())).ToList();
                            loop = false;
                            break;

                        case "3":
                            Console.WriteLine("\nDigite o valor inicial:");
                            valorInicial = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("\nDigite o valor final:");
                            valorFinal = Convert.ToDecimal(Console.ReadLine());

                            filterList = ls.Where(item =>
                                item.ArticlePrice >= valorInicial && item.ArticlePrice <= valorFinal
                                ).ToList();
                            loop = false;
                            break;

                        case "4":
                            Console.WriteLine("\nDigite o valor inicial:");
                            valorInicial = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("\nDigite o valor final:");
                            valorFinal = Convert.ToDecimal(Console.ReadLine());

                            filterList = ls.Where(item =>
                                item.ArticleAmount >= valorInicial && item.ArticleAmount <= valorFinal
                                ).ToList();
                            loop = false;
                            break;


                        default:
                            Console.WriteLine("\nTente novamente.");
                            Console.WriteLine("----------------");
                            break;
                    }
                }

                catch
                {
                    Console.WriteLine("\nErro: tente novamente.");
                    Console.WriteLine("----------------------");
                }
            } 

            return filterList;
        } 

        /// <summary>
        /// Imprime a lista na console
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ls"></param>
        public static void PrintList(string title, List<Articles> ls)
        {
            Console.WriteLine($"\n-----");
            Console.WriteLine($"Lista {title}:\n");
            Console.WriteLine($"{UText.Left("ID", 5)}{UText.Left("Nome", 30)}{UText.Left("Preço", 15)}{UText.Left("Qtd.", 10)}{UText.Left("Total", 15)}{UText.Left("Situacao", 15)}.");

            foreach (Articles item in ls)
            {
                Console.WriteLine($"{UText.Left(item.ArticleID.ToString(), 5)}{UText.Left(item.ArticleName, 30)}{UText.Left(item.ArticlePrice.ToString("c"), 15)}{UText.Left(item.ArticleAmount.ToString(), 10)}{UText.Left((item.ArticlePrice * Convert.ToDecimal(item.ArticleAmount)).ToString("c"), 15)}{UText.Left(item.Situacao, 15)}.");
            }
        }


        /// <summary>
        /// Importar arquivo Json
        /// </summary>
        public static void ImportJson()
        {
            // Checando permissões
            int[] level = { 0 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            // Definindo variáveis
            string fileName;
            string fileCurrentPath = Path.GetFullPath(@"..\..\..\");
            DataTable dt;

            List<Articles> listFull = GetList();
            List<Articles> listImport = new List<Articles>();
            List<Articles> listNew = new List<Articles>();
            List<Articles> listExists = new List<Articles>();

            Console.WriteLine("\nImportação de Json:");

            // Execução
            while (true)
            {
                try
                {
                    Console.WriteLine("\nLocal atual:");
                    Console.WriteLine(fileCurrentPath);

                    Console.WriteLine("\nDigite o Folder \\ Nome do arquivo.json:");
                    fileName = Console.ReadLine();

                    dt = UConverter.JsonToDataTable(fileCurrentPath + fileName);

                    foreach (DataRow r in dt.Rows)
                    {
                        //"ID", "Nome", "Preco", "Qtd", "Situacao"
                        Articles item = new Articles(
                            Convert.ToInt32(r["articleID"]),
                            Convert.ToString(r["articleName"]),
                            Convert.ToInt32(r["articlePrice"]),
                            Convert.ToInt32(r["articleAmount"])
                            );

                        listImport.Add(item);

                        // LINQ
                        // Ao importar o objeto, a comparação foi feita por nome, pois entende-se que o cadastro pode vir com a ID diferente.
                        // Após montar a lista de objetos a serem importados, será buscado o ID do item original, para então atualizar o saldo.
                        if (listFull.Any(elem => elem.ArticleName == item.ArticleName))
                        {
                            listExists.Add(item);
                            listExists.LastOrDefault().ArticleID = listFull.Find(elem => elem.ArticleName == item.ArticleName).ArticleID;
                        }
                        else
                        {
                            listNew.Add(item);
                        }
                    }

                    // Importação:
                    try
                    {
                        Console.WriteLine("\n-------------------------");
                        Console.WriteLine("Item a serem importados:");
                        PrintList(":", listNew);

                        Console.WriteLine("\n-------------------------");
                        Console.WriteLine("Item a serem atualizados:");
                        PrintList(":", listExists);

                        Console.WriteLine("\n--------------------------------------");
                        Console.WriteLine("Opções:");
                        Console.WriteLine("\tI) Importar");
                        Console.WriteLine("\tA) Atualizar");
                        Console.WriteLine("\t0) Sair");
                        Console.WriteLine("Digite a opção desejada:");
                        string opt = Console.ReadLine();

                        switch (opt.ToUpper())
                        {
                            case "0":
                                return;

                            case "I":
                                foreach (Articles item in listNew)
                                {
                                    item.IsNew = true;
                                    item.Create();
                                }

                                Console.WriteLine("\nItens importados com sucesso.");
                                break;

                            case "A":
                                foreach (Articles item in listExists)
                                {
                                    Articles itemUpdate = new Articles(item.ArticleID);
                                    itemUpdate.SetAmount(item.ArticleAmount);

                                    itemUpdate.Update();
                                }

                                Mensagem.Operacao("U");
                                break;

                            default:
                                Console.WriteLine("\nOpção inválida.");
                                Console.WriteLine("---------------");
                                break;
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nErro ao importar.");
                        Console.WriteLine("-----------------");
                        Console.WriteLine(ex.ToString() + "\n");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nErro ao especificar o arquivo.");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(ex.ToString() + "\n");

                    Console.ReadKey();
                    //break;
                }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Exporta todos os dados para Json
        /// </summary>
        public static void ExportJson()
        {
            // Checando permissões
            int[] level = { 0 };

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            string file = Path.GetFullPath(@"..\..\..\") + "_files\\Export_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".json";
            string jsonText;

            Console.WriteLine("\nExportação para o arquivo:");
            Console.WriteLine(file);
            Console.WriteLine();

            jsonText = JsonConvert.SerializeObject(GetTable());

            //Write to a file
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(jsonText);
            }

            Console.WriteLine("\nExportação realizada com sucesso.");
            Console.ReadKey();

        }
    }
}
