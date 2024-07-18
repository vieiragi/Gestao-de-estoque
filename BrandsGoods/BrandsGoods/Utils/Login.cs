using System;
using System.Data;
using BrandsGoods.Data;
using System.Net;

namespace BrandsGoods.Utils
{
    static class Login
    {
        public static bool Try(string num, string senha)
        {
            DbBrandsGoods db = new DbBrandsGoods();

            string sql = $"SELECT COUNT(*) Aut, Num, Nome, grupoID FROM Utilizador (NOLOCK) WHERE 1=1 AND Num ='{num}' AND Senha = '{senha}' GROUP BY Num, Nome, grupoID";

            try
            {
                DataTable dt = new DataTable();
                dt = db.SqlToDataTable(sql);

                if (dt.Rows.Count == 1)
                {
                    Program.userId = Convert.ToString(dt.Rows[0][1]);
                    Program.userName = Convert.ToString(dt.Rows[0][2]);
                    Program.groupId = Convert.ToInt32(dt.Rows[0][3]);

                    return true;
                }
                else
                {
                    Console.WriteLine($"\nUsuário e senha incorretos.\n");
                    Console.ReadKey();

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro de Login. Contate o Administrador.\n");
                Console.WriteLine($"\nErro: {ex}\n");
                Console.ReadKey();
                return false;
            }
        }
        public static bool Finish(int loginTry)
        {
            if (loginTry >= 3)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("╔═══════════════════════╗");
                Console.WriteLine("║     FALHA DE LOGIN    ║");
                Console.WriteLine("╚═══════════════════════╝");
                Console.WriteLine();
                Console.WriteLine($"Muitas tentativas de Login inválidas.\n");
                Console.WriteLine($"Registrando em log as seguintes informações:\n");
                Console.WriteLine($"\tEndereço IP: {Dns.GetHostAddresses(Environment.MachineName)[0]}");
                Console.WriteLine($"\tDomínio: {Environment.UserDomainName}");
                Console.WriteLine($"\tUsuário: {Environment.UserName}");
                Console.WriteLine($"\tTentativas de acesso: {loginTry}");

                return true;
            }
            else return false;
        }
    }
}
