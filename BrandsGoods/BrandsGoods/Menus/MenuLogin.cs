using System;
using BrandsGoods.Utils;

namespace BrandsGoods.Menus
{
    class MenuLogin
    {
        public static bool Show(int loginTry)
        {
            if (Login.Finish(loginTry) == true) return false;

            Program.loginTry++;

            string user;
            string pass = string.Empty;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t╔═══════════════════════╗");
            Console.WriteLine("\t║    *** Login ***      ║");
            Console.WriteLine("\t╚═══════════════════════╝");
            Console.WriteLine();
            Console.Write("Usuário >> ");
            user = Convert.ToString(Console.ReadLine());
            Console.Write("Senha >> ");

            string password;

            UText.Mask(out password);

            // Tentativa de Login
            if (Login.Try(user, password) == true)
            {
                bool mainMenu = true;
                while (mainMenu)
                {
                    mainMenu = MenuMain.Show();
                }

                // Retornando a tela de login
                Program.loginTry = 0;
                return true;
            }

            return true;
        }
    }
}
