using System;
using BrandsGoods.Data;
using BrandsGoods.Menus;

namespace BrandsGoods
{
    class Program
    {
        public static string connString = DbSettings.ConnectionString;
        public static string userId;
        public static string userName;
        public static int groupId;
        public static int loginTry = 0;

        static void Main(string[] args)
        {
            Intro.Show();

            while (true)
            {
                if (MenuLogin.Show(loginTry) == false)
                {
                    break;
                }
            }

            Console.WriteLine("\n\nEncerrando a aplicação...");
            Console.ReadKey();
        }
    }
}
