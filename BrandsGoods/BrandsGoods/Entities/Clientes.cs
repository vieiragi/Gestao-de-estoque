using System;
using System.Globalization;
using System.Data;
using System.Collections.Generic;
using BrandsGoods.Utils;
using BrandsGoods.Data;
using BrandsGoods.Menus;
using System.Linq;

namespace BrandsGoods.Entities
{
    /// <summary>
    /// Classe de Clientes
    /// </summary>
    public class Clientes : IEntities
    {
        private readonly static string TableName = "Clientes";
        private readonly static string[] TableColumns = { "clienteID", "clienteNome" };
        private readonly static string ViewName = "vwClientes";
        private readonly static string[] ViewColumns = { "ID", "Nome" };
        private bool IsNew;
        public int ClienteID { get; set; }
        public string ClienteNome { get; set; }

        /// <summary>
        /// Construtor Vazio
        /// </summary>
        public Clientes()
        {
            IsNew = true;
            ClienteNome = "Novo Cliente";
        }

        /// <summary>
        /// Construtor por ID
        /// </summary>
        /// <param name="clienteID"></param>
        public Clientes(int clienteID)
        {
            GetById(clienteID);
        }

        /// <summary>
        /// Construtor completo
        /// </summary>
        /// <param name="clienteID"></param>
        /// <param name="clienteNome"></param>
        private Clientes(int clienteID, string clienteNome)
        {
            ClienteID = clienteID;
            ClienteNome = clienteNome;
        }

        /// <summary>
        /// Obter item pela ID
        /// </summary>
        /// <param name="clienteID"></param>
        public void GetById(int? clienteID = 0)
        {
            if (clienteID == 0) clienteID = ClienteID;

            DbBrandsGoods db = new DbBrandsGoods();

            string sql = db.SqlSelect(TableColumns, TableName, "", $" AND clienteID={clienteID}");
            DataTable dt = db.SqlToDataTable(sql);

            IsNew = false;
            ClienteID = (int)dt.Rows[0]["clienteID"];
            ClienteNome = (string)dt.Rows[0]["clienteNome"];
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
                int clienteID;
                clienteID = Convert.ToInt32(db.RetMaxId("clienteID", TableName)) + 1;

                // Insert New Record
                string sql = $"INSERT INTO {TableName} VALUES ({clienteID}, '{ClienteNome}')";
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
                string sql = $"UPDATE T SET clienteNome = '{ClienteNome}' FROM {TableName} T WHERE clienteID = {ClienteID}";
                db.ExecSqlQuery(sql);

                Mensagem.Operacao("U");
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
                string sql = $"DELETE FROM {TableName} WHERE clienteID = {ClienteID}";
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
        public static List<Clientes> GetList(string where = "")
        {
            DataTable dt = GetTable(where);

            List<Clientes> ls = new List<Clientes>();

            foreach (DataRow r in dt.Rows)
            {
                Clientes item = new Clientes(
                    Convert.ToInt32(r["ID"]),
                    Convert.ToString(r["Nome"])
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
        public static List<Clientes> FilterList(List<Clientes> ls)
        {
            List<Clientes> filterList = new List<Clientes>();
            string opt;
            string criterio = "";
            bool loop = true;

            while (loop)
            {
                Console.WriteLine("\nSelecione o filtro desejado :");
                Console.WriteLine("1- Por ID:");
                Console.WriteLine("2- Por Nome:");
                Console.WriteLine();

                opt = Console.ReadLine();
                try
                {
                    switch (opt)
                    {
                        case "1":
                            Console.WriteLine("\nDigite ID desejado:");
                            criterio = Console.ReadLine();
                            filterList = ls.Where(c => c.ClienteID.Equals(Convert.ToInt32(criterio))).ToList();
                            loop = false;
                            break;

                        case "2":
                            Console.WriteLine("\nDigite nome desejado:");
                            criterio = Console.ReadLine();
                            filterList = ls.Where(c => c.ClienteNome.ToLower().Contains(criterio.ToLower())).ToList();
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
            } // while

            return filterList;
        } // method

        /// <summary>
        /// Imprime a lista na console
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ls"></param>
        public static void PrintList(string title ,List<Clientes> ls)
        {
            Console.WriteLine($"\n-----");
            Console.WriteLine($"Lista {title}:\n");
            Console.WriteLine($"{UText.Left("ID",5)}{UText.Left("Nome", 25)}.");

            foreach (Clientes c in ls)
            {
                Console.WriteLine($"{UText.Left(c.ClienteID.ToString(),5)}{UText.Left(c.ClienteNome, 25)}.");
            }
        }
    }
}
