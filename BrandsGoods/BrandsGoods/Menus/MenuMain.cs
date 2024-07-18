using System;
using System.Data;
using System.Collections.Generic;
using BrandsGoods.Entities;
using BrandsGoods.Utils;
using BrandsGoods.Forms;

namespace BrandsGoods.Menus
{
    class MenuMain
    {
        public static bool Show()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("\t╔════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║              *** Menu Principal ***                ║");
            Console.WriteLine("\t╚════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("\t\t1. Cadastro de Cliente");
            Console.WriteLine("\t\t2. Cadastro de Produtos");
            Console.WriteLine("\t\t3. Estoque");
            Console.WriteLine("\t\t4. Pedidos");
            Console.WriteLine("\t\t----------");
            if (Program.groupId == 0)
            {
                Console.WriteLine("\t\t9. Adm.");
            }
            Console.WriteLine("\t\t0. Sair");
            Console.WriteLine("\t");
            Console.WriteLine("\t══════════════════════════════════════════════════════");
            Console.WriteLine("");
            Console.WriteLine("Digite a opção desejada:");

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    MenuCadastroCliente();
                    return true;
                case "2":
                    MenuCadastroProduto();
                    return true;
                case "3":
                    MenuMovEstoque();
                    return true;
                case "4":
                    MenuMovPedido();
                    return true;
                case "9":
                    if (Program.groupId == 0)
                    {
                        MenuAdmin();
                    }
                    return true;
                default:
                    Console.WriteLine("Erro ao Gerar a Introdução");
                    return true;
            }
        }

        static void MenuCadastroCliente()
        {
            string tela = "Clientes";
            Clientes item = new Clientes();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\t╔════════════════════════════╗");
                Console.WriteLine("\t║       MENU DE CLIENTES     ║");
                Console.WriteLine("\t║════════════════════════════║");
                Console.WriteLine("\t║ L. >> LISTAR               ║");
                Console.WriteLine("\t║ N. >> NOVO                 ║");
                Console.WriteLine("\t║ E. >> EDITAR               ║");
                Console.WriteLine("\t║ X. >> EXCLUIR              ║");
                Console.WriteLine("\t║--------------------------- ║");
                Console.WriteLine("\t║ 0. >> VOLTAR               ║");
                Console.WriteLine("\t╚════════════════════════════╝");

                Console.WriteLine("\nDigite a opção desejada:");
                string opt = Console.ReadLine();
                opt = opt.ToUpper();

                SubMenu.Opcoes(opt);

                switch (opt)
                {
                    case "0":
                        return;

                    case "L":
                        //Mensagem.Listagem(tela, Clientes.GetTable());
                        List<Clientes> ls = Clientes.GetList();

                        // Imprimindo a lista completa
                        Clientes.PrintList(
                            "Completa"
                            , ls)
                            ;

                        Console.ReadKey();

                        // Imprimindo a lista filtrada
                        Clientes.PrintList(
                            "Filtrada"
                            , Clientes.FilterList(ls)
                            );

                        Console.ReadKey();

                        break;

                    case "N":
                        Mensagem.Listagem(tela, Clientes.GetTable());

                        // Criando registro
                        Console.WriteLine("\nDigite o nome do cliente:");
                        item.ClienteNome = Console.ReadLine();

                        // Execução
                        if (Mensagem.Confirmacao()) item.Create();

                        break;

                    case "E":
                        Mensagem.Listagem(tela, Clientes.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o ID do cliente:");
                        item.ClienteID = Convert.ToInt32(Console.ReadLine());

                        item.GetById();

                        // Editando registro
                        Console.WriteLine("\nDigite o Nome do cliente:");
                        item.ClienteNome = Console.ReadLine();

                        // Execução
                        if (Mensagem.Confirmacao()) item.Update();

                        break;

                    case "X":
                        Mensagem.Listagem(tela, Clientes.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o ID do cliente:");
                        item.ClienteID = Convert.ToInt32(Console.ReadLine());

                        item.GetById();

                        // Execução
                        if (Mensagem.Confirmacao()) item.Delete();

                        break;

                    default:
                        break;
                }
            }
        } // Final Menu Cliente

        static void MenuCadastroProduto()
        {
            string tela = "Produtos";
            Articles item = new Articles();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\t╔════════════════════════════╗");
                Console.WriteLine("\t║       MENU DE PRODUTOS     ║");
                Console.WriteLine("\t║════════════════════════════║");
                Console.WriteLine("\t║ L. >> LISTAR               ║");
                Console.WriteLine("\t║ N. >> NOVO                 ║");
                Console.WriteLine("\t║ E. >> EDITAR               ║");
                Console.WriteLine("\t║ X. >> EXCLUIR              ║");
                Console.WriteLine("\t║--------------------------- ║");
                Console.WriteLine("\t║ 0. >> VOLTAR               ║");
                Console.WriteLine("\t╚════════════════════════════╝");

                Console.WriteLine("\nDigite a opção desejada:");
                string opt = Console.ReadLine();
                opt = opt.ToUpper();

                SubMenu.Opcoes(opt);

                switch (opt)
                {
                    case "0":
                        return;

                    case "L":
                        //Mensagem.Listagem(tela, Articles.GetTable());
                        List<Articles> ls = Articles.GetList();

                        // Imprimindo a lista completa
                        Articles.PrintList(
                            "Completa"
                            , ls)
                            ;

                        Console.ReadKey();

                        // Imprimindo a lista filtrada
                        Articles.PrintList(
                            "Filtrada"
                            , Articles.FilterList(ls)
                            );

                        Console.ReadKey();

                        break;

                    case "N":
                        Mensagem.Listagem(tela, Articles.GetTable());

                        // Criando registro
                        Console.WriteLine("\nDigite o nome do produto:");
                        item.ArticleName = Console.ReadLine();


                        Console.WriteLine("\nDigite o preço do produto:");
                        item.ArticlePrice = Convert.ToDecimal(Console.ReadLine());

                        // Execução
                        if (Mensagem.Confirmacao()) item.Create();

                        break;

                    case "E":
                        Mensagem.Listagem(tela, Articles.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o ID do produto:");
                        item.ArticleID = Convert.ToInt32(Console.ReadLine());

                        item.GetById();

                        // Editando registro
                        Console.WriteLine("\nDigite o Nome do produto:");
                        item.ArticleName = Console.ReadLine();

                        Console.WriteLine("\nDigite o preço do produto:");
                        item.ArticlePrice = Convert.ToDecimal(Console.ReadLine());

                        // Execução
                        if (Mensagem.Confirmacao()) item.Update();
                        Mensagem.Operacao("U");

                        break;

                    case "X":
                        Mensagem.Listagem(tela, Articles.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o ID do produto:");
                        item.ArticleID = Convert.ToInt32(Console.ReadLine());

                        item.GetById();

                        // Execução
                        if (Mensagem.Confirmacao()) item.Delete();

                        break;

                    default:
                        break;
                }
            }
        }

        static void MenuMovEstoque()
        {
            string tela = "Estoque";
            Articles item = new Articles();
            int[] level = new int[] { 0 };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n");
                Console.WriteLine("\t╔════════════════════════════╗");
                Console.WriteLine("\t║           ESTOQUE          ║");
                Console.WriteLine("\t║════════════════════════════║");
                Console.WriteLine("\t║ Q. >> AT. QUANTIDADE       ║");
                Console.WriteLine("\t║ S. >> AT. SITUACAO         ║");
                if (Program.groupId == 0)
                {
                    Console.WriteLine("\t║--------------------------- ║");
                    Console.WriteLine("\t║ 1. >> IMPORTAR             ║");
                    Console.WriteLine("\t║ 2. >> EXPORTAR             ║");
                }
                Console.WriteLine("\t║--------------------------- ║");
                Console.WriteLine("\t║ 0. >> VOLTAR               ║");
                Console.WriteLine("\t╚════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("\nDigite a opção desejada:");
                string opt = Console.ReadLine();
                opt = opt.ToUpper();

                SubMenu.Opcoes(opt);

                switch (opt)
                {
                    case "0":
                        return;

                    case "1":

                        if (!Permissao.Check(level))
                        {
                            Mensagem.SemPermissao();
                            break;
                        }

                        Articles.ImportJson();
                        break;

                    case "2":
                        Articles.ExportJson();
                        break;

                    case "Q":
                        Mensagem.Listagem(tela, Articles.GetTable());

                        while (true)
                        {
                            try
                            {
                                // Obtendo o registro
                                Console.WriteLine("\nDigite o ID do produto:");
                                item.ArticleID = Convert.ToInt32(Console.ReadLine());

                                item.GetById();
                                int qtd;
                                // Editando registro
                                Console.WriteLine("\nDigite a nova quantidade:");
                                qtd = int.Parse(Console.ReadLine());
                                item.SetAmount(qtd - item.ArticleAmount);
                                break;
                            }
                            catch { Console.WriteLine("\nTente novamente...\n"); }
                        }

                        // Execução
                        if (Mensagem.Confirmacao())
                        {
                            item.Update();
                            Mensagem.Operacao("U");
                        }
                        break;

                    case "S":
                        Mensagem.Listagem(tela, Articles.GetTable());
                        while (true)
                        {
                            try
                            {
                                // Obtendo o registro
                                Console.WriteLine("\nDigite o ID do produto:");
                                item.ArticleID = Convert.ToInt32(Console.ReadLine());

                                item.GetById();

                                // Editando registro
                                Console.WriteLine("\nDescreva a nova situacao (Em Estoque / Sem Estoque):");
                                string situacao = Console.ReadLine();
                                item.SetSituacao(situacao);
                                break;
                            }
                            catch { Console.WriteLine("\nTente novamente...\n"); }
                        }

                        // Execução
                        if (Mensagem.Confirmacao())
                        {
                            item.Update();
                            Mensagem.Operacao("U");
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        static void MenuMovPedido()
        {
            while (true)
            {
                DataTable dt;

                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("\t╔════════════════════════════╗");
                Console.WriteLine("\t║           PEDIDOS          ║");
                Console.WriteLine("\t║════════════════════════════║");
                Console.WriteLine("\t║ L. >> LISTAR               ║");
                Console.WriteLine("\t║ N. >> NOVO                 ║");
                Console.WriteLine("\t║ D. >> DETALHES             ║");
                Console.WriteLine("\t║--------------------------- ║");
                Console.WriteLine("\t║ 0. >> VOLTAR               ║");
                Console.WriteLine("\t╚════════════════════════════╝");
                Console.WriteLine();

                Console.WriteLine("\nDigite a opção desejada:");
                string opt = Console.ReadLine();
                opt = opt.ToUpper();

                SubMenu.Opcoes(opt);

                switch (opt)
                {
                    case "0":
                        return;

                    case "L":
                        dt = Pedidos.GetTable();
                        UPrint.DataTableToConsole(dt);

                        Console.ReadKey();
                        break;

                    case "N":
                        FrmPedidos frm = new FrmPedidos();
                        frm.Novo();
                        break;

                    case "D":
                        dt = Pedidos.GetTable();
                        UPrint.DataTableToConsole(dt);

                        FrmPedidos frm1 = new FrmPedidos();
                        // Obtendo o registro

                        Console.WriteLine("\nDigite o ID do pedido:");
                        frm1.GetDetalhes(Convert.ToInt32(Console.ReadLine()));

                        Console.ReadKey();
                        break;

                    default:
                        break;
                }
            }
        }

        static void MenuAdmin()
        {
            if (Program.groupId != 0) return;

            string tela = "Usuários";
            Utilizador item = new Utilizador();
            string password;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\t╔════════════════════════════╗");
                Console.WriteLine("\t║           ADMIN.           ║");
                Console.WriteLine("\t║════════════════════════════║");
                Console.WriteLine("\t║ L. >> LISTAR               ║");
                Console.WriteLine("\t║ N. >> NOVO                 ║");
                Console.WriteLine("\t║ E. >> EDITAR               ║");
                Console.WriteLine("\t║ X. >> EXCLUIR              ║");
                Console.WriteLine("\t║--------------------------- ║");
                Console.WriteLine("\t║ 0. >> VOLTAR               ║");
                Console.WriteLine("\t╚════════════════════════════╝");

                Console.WriteLine("\nDigite a opção desejada:");
                string opt = Console.ReadLine();
                opt = opt.ToUpper();

                SubMenu.Opcoes(opt);

                switch (opt)
                {
                    case "0":
                        return;

                    case "L":
                        Mensagem.Listagem(tela, Utilizador.GetTable());
                        Console.ReadKey();
                        break;

                    case "N":
                        Mensagem.Listagem(tela, Utilizador.GetTable());

                        // Criando registro
                        Console.WriteLine("\nDigite o codigo do usuario:");
                        item.Num = Console.ReadLine();

                        Console.WriteLine("\nDigite o nome do usuario:");
                        item.Nome = Console.ReadLine();

                        Console.WriteLine("\nDigite a senha:");
                        UText.Mask(out password);
                        item.Senha = password;

                        Console.WriteLine("\nDigite o ID do Grupo (0- Admin, 1- Usuario):");
                        item.GrupoID = Convert.ToInt32(Console.ReadLine());

                        // Execução
                        if (Mensagem.Confirmacao()) item.Create();

                        break;

                    case "E":
                        Mensagem.Listagem(tela, Utilizador.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o codigo do usuario:");
                        item.Num = Console.ReadLine();

                        item.GetById();

                        // Editando registro
                        Console.WriteLine("\nDigite o nome do usuario:");
                        item.Nome = Console.ReadLine();

                        Console.WriteLine("\nDigite a senha:");
                        UText.Mask(out password);
                        item.Senha = password;

                        Console.WriteLine("\nDigite o ID do Grupo (0- Admin, 1- Usuario):");
                        item.GrupoID = Convert.ToInt32(Console.ReadLine());

                        // Execução
                        if (Mensagem.Confirmacao()) item.Update();

                        break;

                    case "X":
                        Mensagem.Listagem(tela, Utilizador.GetTable());

                        // Obtendo o registro
                        Console.WriteLine("\nDigite o codigo do usuario:");
                        item.Num = Console.ReadLine();

                        item.GetById();

                        // Execução
                        if (Mensagem.Confirmacao()) item.Delete();

                        break;

                    default:
                        break;
                }
            }
        }

    }
}
