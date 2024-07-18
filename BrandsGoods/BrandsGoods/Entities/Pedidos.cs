using System;
using System.Data;
using System.Collections.Generic;
using BrandsGoods.Data;
using BrandsGoods.Menus;
using System.Text;

namespace BrandsGoods.Entities
{
    /// <summary>
    /// Classe de Pedidos
    /// </summary>
    public class Pedidos //: IEntities
    {
        private readonly static string TableName = "Pedidos";
        private readonly static string[] TableColumns = { "pedidoID", "clienteID", "pedidoData", "NumItem", "articleID", "articleAmount", "articlePrice" };
        private readonly static string ViewName = "vwPedidos";
        private readonly static string[] ViewColumns = { "pedID", "cliID", "Cliente", "Data", "Qtd", "Valor" };

        private bool IsNew;
        public int PedidoID { get; set; }
        public DateTime PedidoData { get; set; }
        public Clientes Cliente { get; set; }
        public List<Articles> ListArticles { get; set; }

        /// <summary>
        /// Construtor Vazio
        /// </summary>
        public Pedidos()
        {
            IsNew = true;
            PedidoData = DateTime.Now;
            Cliente = new Clientes();
            ListArticles = new List<Articles>();
        }

        /// <summary>
        /// Construtor por ID
        /// </summary>
        /// <param name="pedidoID"></param>
        public Pedidos(int pedidoID)
        {
            GetById(pedidoID);
        }

        /// <summary>
        /// Construtor Completo
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="clienteID"></param>
        /// <param name="pedidoData"></param>
        /// <param name="listArticle"></param>
        private Pedidos(int pedidoID, int clienteID, DateTime pedidoData, List<Articles> listArticle)
        {
            IsNew = false;
            PedidoID = pedidoID;
            PedidoData = pedidoData;
            Cliente.GetById(clienteID);
            ListArticles = listArticle;
        }

        /// <summary>
        /// Obter item pelo ID
        /// </summary>
        /// <param name="pedidoID"></param>
        public void GetById(int? pedidoID = 0)
        {
            if (pedidoID == 0) pedidoID = PedidoID;

            try
            {
                DbBrandsGoods db = new DbBrandsGoods();

                // Tratando a falta da Tabela de Itens do Pedido separada
                string sql = @"
                SELECT 
	                P.pedidoID pedidoID
	                ,C.clienteID clienteID
	                ,C.clienteNome Cliente
	                ,P.NumItem Num
	                ,A.articleID articleID
	                ,A.articleName articleName
	                ,P.articleAmount
	                ,P.articlePrice

                FROM Pedidos P (NOLOCK)
	                LEFT JOIN Clientes C (NOLOCK) ON P.clienteID=C.clienteID
	                LEFT JOIN Articles A (NOLOCK) ON P.articleID=A.articleID

                WHERE 1=1
	                AND P.pedidoID = " + pedidoID;

                DataTable dt = db.SqlToDataTable(sql);

                IsNew = false;
                PedidoID = (int)dt.Rows[0]["PedidoID"];

                Cliente.GetById((int)dt.Rows[0]["clienteID"]);

                foreach (DataRow row in dt.Rows)
                {
                    Articles item = new Articles();
                    item.ArticleID = (int)row["articleID"];
                    item.ArticleName = (string)row["articleName"];
                    item.ArticlePrice = (decimal)row["articlePrice"];
                    item.ArticleAmount = (int)row["articleAmount"];
                    ListArticles.Add(item);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
            }
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
                int pedidoID;
                pedidoID = Convert.ToInt32(db.RetMaxId("pedidoID", TableName)) + 1;

                // Insert New Record
                string sql = $"INSERT INTO {TableName} ";

                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (Articles item in ListArticles)
                {
                    sb.Append($"SELECT {pedidoID},{Cliente.ClienteID},'{PedidoData}'");
                    sb.Append($",{++i} ");
                    sb.Append($",{item.ArticleID}");
                    sb.Append($",{item.ArticleAmount}");
                    sb.Append($",{item.ArticlePrice.ToString().Replace(',', '.')}");
                    if (i == ListArticles.Count) break;
                    sb.Append(" UNION ALL ");
                }

                sql = sql + sb.ToString();
                db.ExecSqlQuery(sql);

                foreach (Articles item in ListArticles)
                {
                    Articles it = new Articles(item.ArticleID);
                    it.SetAmount(item.ArticleAmount * -1);
                    it.Update();

                    Console.ReadKey();
                }

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
        private void Update()
        {
            // Uma vez efetivado, o pedido não pode ser alterado.
            return;
        }

        /// <summary>
        /// Exclui o registro
        /// </summary>
        private void Delete()
        {   // Uma vez efetivado, o pedido não pode ser alterado.
            return;
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
    }
}
