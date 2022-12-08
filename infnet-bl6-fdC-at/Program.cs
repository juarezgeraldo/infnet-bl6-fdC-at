
using System.Collections;
using Funcoes;

namespace AplicacaoPrincipal

{
    public class Programa
    {
        static void Main(string[] args)
        {
            int opcao = 0;
            Funcoes.Funcoes funcao = new Funcoes.Funcoes();

            string arquivo = funcao.verificaArquivo();
            Console.WriteLine(arquivo);

            while (opcao != 9)
            {
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine("Selecione uma das opções abaixo:");
                Console.WriteLine("1 - Adicionar pessoa");
                Console.WriteLine("2 - Alterar pessoas");
                Console.WriteLine("3 - Excluir pessoas");
                Console.WriteLine("4 - Pesquisar pessoas");
                Console.WriteLine("5 - Pesquisar aniversário");
                Console.WriteLine();
                Console.WriteLine("9 - Sair");

                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Valor informado está inválido");
                };

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        funcao.cadastraPessoa();
                        break;
                    case 4:
                        funcao.pesquisaPessoa();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }

        }
    }
}