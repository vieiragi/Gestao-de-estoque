using System;
using System.Linq;
using BrandsGoods.Menus;
using BrandsGoods.Entities;

namespace BrandsGoods.Forms
{
    class FrmPedidos
    {
        public Pedidos Pedido { get; set; }
        public bool Confirmado { get; set; }
        public bool Sair { get; set; }
        public FrmPedidos()
        {
            Confirmado = false;
            Sair = false;
            Pedido = new Pedidos();
        }
        public void Novo()
        {
            Console.WriteLine("Cadastro de pedidos:");
            Console.WriteLine("--------------------");

            try
            {
                // Selecionando o cliente
                SelecionarCliente();
                if (Sair) return;

                // Selecionando os itens do pedido
                SelecionarProdutos();
                if (Sair) return;

                if (Confirmado)
                {
                    Pedido.Create();
                }
            }
            catch { return; }
        }

        private void SelecionarCliente()
        {

            Mensagem.Listagem("Clientes", Clientes.GetTable());
            Console.WriteLine("\n-------------------");
            Console.WriteLine("Selecione o ID do Cliente para o novo pedido:");

            try
            {
                Pedido.Cliente.GetById(int.Parse(Console.ReadLine()));
            }

            catch
            {                
                Sair = true;
            }
            return;
        }

        private void SelecionarProdutos()
        {
            try
            {
                while (true)
                {
                    PrintResumo();

                    string opt;
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine("Menu de Opções:");
                    Console.WriteLine("\tI) Incluir novo item no pedido");
                    Console.WriteLine("\tX) Excluir item do pedido");
                    Console.WriteLine("\tF) Finalizar");
                    Console.WriteLine("Digite a opção desejada:");

                    opt = Console.ReadLine();

                    switch (opt.ToUpper())
                    {

                        case "I":
                            Mensagem.Listagem("Produto", Articles.GetTable());
                            Console.WriteLine("\n----------------------");
                            Console.WriteLine("Qual id do produto?");

                            Articles item = new Articles();
                            item.GetById(int.Parse(Console.ReadLine()));

                            Console.WriteLine("\nProduto selecionado:");
                            Console.WriteLine($"\tID: {item.ArticleID}");
                            Console.WriteLine($"\tNome: {item.ArticleName}");
                            Console.WriteLine();

                            Console.WriteLine("\n----------------------");
                            Console.WriteLine("Insira a Quantidade:");
                            item.ArticleAmount = int.Parse(Console.ReadLine());
                            Console.WriteLine();


                            //if (Mensagem.Confirmacao()) 
                            Pedido.ListArticles.Add(item);
                            break;

                        case "X":
                            Console.WriteLine("\n----------------------");
                            Console.WriteLine("Qual id do produto?");

                            string itemDelete = Console.ReadLine();
                            Articles itemx = Pedido.ListArticles.Single(item => item.ArticleID == int.Parse(itemDelete));

                            Console.WriteLine("\nRemover o seguinte item:");
                            Console.WriteLine($"\tID: {itemx.ArticleID}");
                            Console.WriteLine($"\tNome: {itemx.ArticleName}");

                            if (Mensagem.Confirmacao()) Pedido.ListArticles.Remove(itemx);
                            break;

                        case "F":
                            Console.WriteLine("\n\n----------------------");
                            Console.WriteLine("Deseja concluir o pedido?");
                            if (Mensagem.Confirmacao())
                            {
                                Confirmado = true;
                                PrintResumo();
                                return;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"\nErro:\n{ex}\n");
                Console.ReadKey();
                return;
            }
        }

        private void PrintResumo()
        {
            Console.Clear();
            SubMenu.Pedido();
            Console.WriteLine("----------------------");
            Console.WriteLine("Dados do Cliente:");
            Console.WriteLine($"ID: {Pedido.Cliente.ClienteID}");
            Console.WriteLine($"Nome: {Pedido.Cliente.ClienteNome}");
            Articles.PrintList("dados do pedido", Pedido.ListArticles);

            Console.WriteLine("\n----------------------");
            Console.WriteLine($"Total do Itens: {Pedido.ListArticles.Count()}");
            Console.WriteLine($"Total do Pedido: {Pedido.ListArticles.Sum(item => (item.ArticleAmount * item.ArticlePrice)).ToString("c")}");
        }

        public void GetDetalhes(int id)
        {
            Pedido.GetById(id);
            PrintResumo();
        }
    }
}
