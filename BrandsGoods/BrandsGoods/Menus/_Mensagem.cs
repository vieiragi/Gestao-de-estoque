using System;
using System.Data;
using BrandsGoods.Utils;

namespace BrandsGoods.Menus
{
    static class Mensagem
    {
        public static void Listagem(string tela, DataTable dt)
        {
            Console.WriteLine($"\nListagem de {tela}");
            UPrint.DataTableToConsole(dt);
            Console.WriteLine("\nFim da Listagem...\n");

            //Console.ReadKey();
        }
        public static void SemPermissao()
        {
            Console.WriteLine("\nSem Permissão de Acesso:\n");
            Console.ReadKey();
        }

        public static void Operacao(string opt)
        {
            string texto;

            // Opções: C- Confirmado, U- Atualizado, D- Excluído
            switch (opt.ToUpper())
            {
                case "C":
                    texto = "realizado";
                    break;

                case "U":
                    texto = "atualizado";
                    break;

                case "D":
                    texto = "excluído";
                    break;

                default:
                    return;
            }
            Console.WriteLine($"\nCadastro {texto} com sucesso.");
            Console.ReadKey();
        }

        public static bool Confirmacao()
        {
            string confirm;
            Console.WriteLine("\nConfirma a Operação? (S/N)");
            confirm = Console.ReadLine().ToUpper();

            if (confirm == "S") return true;
            else return false;
        }

        public static void Erro(string ex = "")
        {
            if (string.IsNullOrEmpty(ex)) ex = "Sem mais informações.";

            Console.WriteLine("\nErro na operação:\n");
            Console.WriteLine($"{ex}");
            Console.ReadKey();
        }
    }
}
