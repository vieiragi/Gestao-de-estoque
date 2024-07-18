using System;
using System.Data;
using System.Data.SqlClient;
using BrandsGoods.Menus;

namespace BrandsGoods.Data
{
    class DbBrandsGoods
    {
        private string cnx = Program.connString;

        //Abrir e Fechar Conexão
        private SqlConnection ConOpen()
        {
            SqlConnection cn = new SqlConnection(cnx);
            cn.Open();
            return cn;
        }
        private void ConClose(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        // Builder - SELECT
        public string SqlSelect(string[] tbColunas, string tbTabela, string tbJoin, string tbWhere)
        {
            string colunas = string.Join(",", tbColunas);

            string builder = @"SELECT "
                                + colunas + " FROM "
                                + tbTabela + " (NOLOCK) "
                                + tbJoin + " WHERE 1=1 "
                                + tbWhere
                                + "";
            return builder;
        }

        public string RetMaxId(string tbColuna, string tbTabela)
        {
            string id = "1";
            string builder = @"SELECT TOP 1 "
                                + tbColuna + " FROM "
                                + tbTabela + " (NOLOCK) "
                                + " WHERE 1=1 "
                                + "ORDER BY " + tbColuna + " DESC ";

            DataTable dt = SqlToDataTable(builder);

            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0][0].ToString();
            }

            return id;
        }

        //Classe para execução de comando
        public void ExecSqlQuery(string _sqlQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = ConOpen();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
                throw;
            }
            finally
            {
                ConClose(cn);
            }
        }

        //Classe para retornar um DataTable()
        public DataTable SqlToDataTable(string _sqlQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = ConOpen();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
                throw;
            }
            finally
            {
                ConClose(cn);
            }
        }

        public DataTable SqlToDataTableTask(string _sqlQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = ConOpen();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.ToString());
                throw;
            }
            finally
            {
                ConClose(cn);
            }
        }
    }
}
