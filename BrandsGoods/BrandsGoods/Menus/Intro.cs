using System;

namespace BrandsGoods.Menus
{
    static class Intro
    {
        public static void Show()
        {
            Console.WriteLine("\n\n");

            // Gera um numero randomico
            Random rnd = new Random();
            int numRandom = rnd.Next(1, 4);

            Console.ForegroundColor = ConsoleColor.Gray;
            // Gera a intro aleatória
            switch (numRandom)
            {
                case 1:
                    Intro01();
                    break;
                case 2:
                    Intro02();
                    break;
                case 3:
                    Intro03();
                    break;
                case 4:
                    Intro04();
                    break;
                default:
                    Console.WriteLine("Erro ao Gerar a Introdução");
                    break;
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("=====================================");
            Console.WriteLine("Bem Vindo ao Sistema da Brands Goods!");
            Console.WriteLine("Dev. por Giovanni");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
        static void Intro01()
        {
            Console.WriteLine("                  '########::'########:::::'###::::'##::: ##:'########:::'######::");
            Console.WriteLine("                   ##.... ##: ##.... ##:::'## ##::: ###:: ##: ##.... ##:'##... ##:");
            Console.WriteLine("                   ##:::: ##: ##:::: ##::'##:. ##:: ####: ##: ##:::: ##: ##:::..::");
            Console.WriteLine("                   ########:: ########::'##:::. ##: ## ## ##: ##:::: ##:. ######::");
            Console.WriteLine("                   ##.... ##: ##.. ##::: #########: ##. ####: ##:::: ##::..... ##:");
            Console.WriteLine("                   ##:::: ##: ##::. ##:: ##.... ##: ##:. ###: ##:::: ##:'##::: ##:");
            Console.WriteLine("                   ########:: ##:::. ##: ##:::: ##: ##::. ##: ########::. ######::");
            Console.WriteLine("                  ........:::..:::::..::..:::::..::..::::..::........::::......:::");
            Console.WriteLine("           :'######::::'#######:::'#######::'########:::'######::::::'######:::::'###::::");
            Console.WriteLine("           '##... ##::'##.... ##:'##.... ##: ##.... ##:'##... ##::::'##... ##:::'## ##:::");
            Console.WriteLine("            ##:::..::: ##:::: ##: ##:::: ##: ##:::: ##: ##:::..::::: ##:::..:::'##:. ##::");
            Console.WriteLine("            ##::'####: ##:::: ##: ##:::: ##: ##:::: ##:. ######:::::. ######::'##:::. ##:");
            Console.WriteLine("            ##::: ##:: ##:::: ##: ##:::: ##: ##:::: ##::..... ##:::::..... ##: #########:");
            Console.WriteLine("            ##::: ##:: ##:::: ##: ##:::: ##: ##:::: ##:'##::: ##::::'##::: ##: ##.... ##:");
            Console.WriteLine("           . ######:::. #######::. #######:: ########::. ######:::::. ######:: ##:::: ##:");
            Console.WriteLine("           :......:::::.......::::.......:::........::::......:::::::......:::..:::::..::");
        }
        static void Intro02()
        {
            Console.WriteLine("            ____                      _        ____                 _       ____    _    ");
            Console.WriteLine("           | __ ) _ __ __ _ _ __   __| |___   / ___| ___   ___   __| |___  / ___|  / \\   ");
            Console.WriteLine("           |  _ \\| '__/ _` | '_ \\ / _` / __| | |  _ / _ \\ / _ \\ / _` / __| \\___ \\ / _ \\  ");
            Console.WriteLine("           | |_) | | | (_| | | | | (_| \\__ \\ | |_| | (_) | (_) | (_| \\__ \\  ___) / ___ \\ ");
            Console.WriteLine("           |____/|_|  \\__,_|_| |_|\\__,_|___/  \\____|\\___/ \\___/ \\__,_|___/ |____/_/   \\_\\");
        }
        static void Intro03()
        {
            Console.WriteLine("                 _____               _        _____           _        _____ _____ ");
            Console.WriteLine("                | __  |___ ___ ___ _| |___   |   __|___ ___ _| |___   |   __|  _  |");
            Console.WriteLine("                | __ -|  _| .'|   | . |_ -|  |  |  | . | . | . |_ -|  |__   |     |");
            Console.WriteLine("                |_____|_| |__,|_|_|___|___|  |_____|___|___|___|___|  |_____|__|__|");
        }
        static void Intro04()
        {
            Console.WriteLine("          ____                      _        _____                 _        _____         ");
            Console.WriteLine("         |  _ \\                    | |      / ____|               | |      / ____|  /\\    ");
            Console.WriteLine("         | |_) |_ __ __ _ _ __   __| |___  | |  __  ___   ___   __| |___  | (___   /  \\   ");
            Console.WriteLine("         |  _ <| '__/ _` | '_ \\ / _` / __| | | |_ |/ _ \\ / _ \\ / _` / __|  \\___ \\ / /\\ \\  ");
            Console.WriteLine("         | |_) | | | (_| | | | | (_| \\__ \\ | |__| | (_) | (_) | (_| \\__ \\  ____) / ____ \\ ");
            Console.WriteLine("         |____/|_|  \\__,_|_| |_|\\__,_|___/  \\_____|\\___/ \\___/ \\__,_|___/ |_____/_/    \\_\\");
        }
    }
}
