using System;

namespace BrandsGoods.Menus
{
    class SubMenu
    {
        public static void Opcoes(string opt)
        {
            Console.Clear();

            switch (opt)
            {
                case "L": 
                    Listar();
                    break;

                case "N":
                    Cadastrar();
                    break;

                case "E":
                    Editar();
                    break;

                case "X":
                    Excluir();
                    break;

                case "D":
                    Excluir();
                    break;

                default:
                    break;
            }
        }
        public static void Listar()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║        LISTAGEM       ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine();
        }
        public static void Cadastrar()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║      NOVO CADASTRO    ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine();
        }
        public static void Editar()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║    EDITAR REGISTRO    ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine();
        }
        public static void Excluir()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║    EXCLUIR REGISTRO   ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine();
        }

        public static void Detalhes()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║  DETALHES DO REGISTRO ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine();
        }
        public static void Pedido()
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║ *** RESUMO DO PEDIDO *** ║");
            Console.WriteLine("╚══════════════════════════╝");
            Console.WriteLine();
        }

    }
}
