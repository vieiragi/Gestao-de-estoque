using System;
using System.Data;
using System.Collections.Generic;
using BrandsGoods.Data;
using BrandsGoods.Menus;

namespace BrandsGoods.Entities
{
    /// <summary>
    /// Classe de Utilizador (Users)
    /// </summary>
    public class Utilizador : IEntities
    {
        private readonly static string TableName = "Utilizador";
        private readonly static string[] TableColumns = { "Num", "Nome", "Senha", "grupoID" };
        private readonly static string ViewName = "vwUtilizador";
        private readonly static string[] ViewColumns = { "CodUser", "Nome", "Senha", "ID", "Grupo" };
        private bool IsNew;
        public string Num { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public int GrupoID { get; set; }

        /// <summary>
        /// Construtor Vazio
        /// </summary>
        public Utilizador()
        {
            IsNew = true;
            Nome = "Novo Usuario";
            Senha = "mudar@123";
            GrupoID = 1;
        }

        /// <summary>
        /// Construtor por ID
        /// </summary>
        /// <param name="num"></param>
        public Utilizador(string num)
        {
            GetById(num);
        }
        /// <summary>
        /// Construtor completo
        /// </summary>
        /// <param name="num"></param>
        /// <param name="nome"></param>
        /// <param name="senha"></param>
        /// <param name="grupoId"></param>
        private Utilizador(string num, string nome, string senha, int grupoId)
        {
            Num = num;
            Nome = nome;
            Senha = senha;
            GrupoID = grupoId;
        }

        /// <summary>
        /// Obter item pela ID
        /// </summary>
        /// <param name="num"></param>
        public void GetById(string num = "")
        {
            if (string.IsNullOrEmpty(num)) num = Num;

            DbBrandsGoods db = new DbBrandsGoods();
            // Executando cadastro
            try
            {
                string sql = db.SqlSelect(TableColumns, TableName, "", $" AND Num='{num}'");
                DataTable dt = db.SqlToDataTable(sql);

                IsNew = false;
                Num = (string)dt.Rows[0]["Num"];
                Nome = (string)dt.Rows[0]["Nome"];
                Senha = (string)dt.Rows[0]["Senha"];
                GrupoID = (int)dt.Rows[0]["grupoID"];
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
            int[] level = { 0 }; // Somente admin.

            if (!Permissao.Check(level))
            {
                Mensagem.SemPermissao();
                return;
            }

            // Executando cadastro
            try
            {
                DbBrandsGoods db = new DbBrandsGoods();

                // Insert New Record
                string sql = $"INSERT INTO {TableName} VALUES ('{Num}', '{Nome}', '{Senha}', {GrupoID})";
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
            int[] level = { 0 }; // Somente admin.

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
                string sql = $"UPDATE T SET Nome = '{Nome}', Senha = '{Senha}', grupoID = {GrupoID} FROM {TableName} T WHERE Num = '{Num}'";
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
            int[] level = { 0 }; // Somente admin.

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
                string sql = $"DELETE FROM {TableName} WHERE Num = '{Num}'";
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
        public static List<Utilizador> GetList(string where = "")
        {
            DataTable dt = GetTable(where);

            List<Utilizador> ls = new List<Utilizador>();

            foreach (DataRow r in dt.Rows)
            {
                Utilizador item = new Utilizador(
                    Convert.ToString(r["Num"]),
                    Convert.ToString(r["Nome"]),
                    Convert.ToString(r["Senha"]),
                    Convert.ToInt32(r["grupoID"])
                    );

                ls.Add(item);
            }

            return ls;
        }
    }
}
