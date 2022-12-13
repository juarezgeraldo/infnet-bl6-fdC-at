using PessoaAniversario;
using System.Collections;
using _Repositorio;
using System.IO;

namespace Funcoes
{
    public class Funcoes
    {
        //private Pessoa pessoa = new Pessoa();
        public List<Pessoa> pessoaList = new();
        private readonly Repositorio repositorio = new();

        public void CarregaPoessoaList(string arquivo)
        {
            pessoaList = repositorio.CarregaPessoasLista(arquivo);
        }

        public void CadastraPessoa(string arquivo)
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Cadastrar pessoas");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome da pessoa:");
            string nome = RecebeStringTela().Trim();

            Console.WriteLine("Digite o sobrenome da pessoa:");
            string sobreNome = RecebeStringTela().Trim();

            Console.WriteLine("Digite a data do aniversário no formato dd/mm/yyyy");
            DateTime dataNascimento = RecebeDataTela();

            Console.WriteLine();

            Console.WriteLine("Confirma inclusão?");
            Console.WriteLine("Nome           : " + nome + " " + sobreNome);
            Console.WriteLine("Data nascimento: {0:dd/MM/yyyy}", dataNascimento);
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");

            if (RecebeOpcao(2) == 1)
            {
                Pessoa pessoa = new()
                {
                    Nome = nome,
                    Sobrenome = sobreNome,
                    DataNascimento = dataNascimento
                };
                repositorio.AdicionarPessoa(pessoa, arquivo);
            }
        }

        public void PesquisaPessoa()
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Pesquisar pessoas");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome/sobrenome ou parte do nome/sobrenome da pessoa:");
            string nomePesq = RecebeStringTela().Trim();

            Dictionary<int, int> indices = repositorio.PesquisaPessoas(nomePesq);

            if (indices.Count > 0)
            {
                Pessoa pessoa = pessoaList[RecebeOpcaoIds(indices)];

                Console.Clear();
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine("Pesquisar pessoas");
                Console.WriteLine("-----------------");
                Console.WriteLine();
                Console.WriteLine("Dados da pessoa selecionada:");
                Console.WriteLine("Nome completo      : " + pessoa.NomeCompleto);
                Console.WriteLine("Data nascimento    : {0:dd/MM/yyyy}", pessoa.DataNascimento);
                Console.WriteLine();
                Console.WriteLine("Próximo aniversário: {0:dd/MM/yyyy}", pessoa.ProximoAniversario());
                Console.WriteLine();
                int diasFaltantes = pessoa.CalculaDiasFaltantes();
                Console.WriteLine(diasFaltantes == 0 ? "É HOJE!!!!!" : "Faltam " + diasFaltantes + " dias para o próximo aniversário:");

                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public void AlteraPessoa(string arquivo)
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Alterar pessoas");
            Console.WriteLine("---------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome/sobrenome ou parte do nome/sobrenome da pessoa:");
            string nomePesq = RecebeStringTela().Trim();

            Dictionary<int, int> indices = repositorio.PesquisaPessoas(nomePesq);

            if (indices.Count > 0)
            {
                int indicePessoa = RecebeOpcaoIds(indices);
                Pessoa pessoa = pessoaList[indicePessoa];


                Console.Clear();
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine("Alterar pessoas");
                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("Dados da pessoa selecionada:");
                Console.WriteLine("Nome completo      : " + pessoa.NomeCompleto);
                Console.WriteLine("Data nascimento    : {0:dd/MM/yyyy}", pessoa.DataNascimento);
                Console.WriteLine();

                Console.WriteLine("Digite o novo nome da pessoa:");
                string nome = RecebeStringTela().Trim();

                Console.WriteLine("Digite o sobrenome da pessoa:");
                string sobreNome = RecebeStringTela().Trim();

                Console.WriteLine("Digite a data do aniversário no formato dd/mm/yyyy");
                DateTime dataNascimento = RecebeDataTela();

                Console.WriteLine();

                Console.WriteLine("Confirma alteração?");
                Console.WriteLine("Nome           : " + nome + " " + sobreNome);
                Console.WriteLine("Data nascimento: {0:d}", dataNascimento);
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");

                if (RecebeOpcao(2) == 1)
                {
                    pessoa.Nome = nome;
                    pessoa.Sobrenome = sobreNome;
                    pessoa.DataNascimento = dataNascimento;

                    pessoaList[indicePessoa] = pessoa;

                    repositorio.AlterarPessoa(arquivo);
                }
            }
        }

        public void ExcluiPessoa(string arquivo)
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Excluir pessoas");
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome/sobrenome ou parte do nome/sobrenome da pessoa:");
            string nomePesq = RecebeStringTela().Trim();

            Dictionary<int, int> indices = repositorio.PesquisaPessoas(nomePesq);

            if (indices.Count > 0)
            {
                Pessoa pessoa = pessoaList[RecebeOpcaoIds(indices)];

                Console.Clear();
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine("Excluir pessoas");
                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("Dados da pessoa selecionada:");
                Console.WriteLine("Nome completo      : " + pessoa.NomeCompleto);
                Console.WriteLine("Data nascimento    : {0:dd/MM/yyyy}", pessoa.DataNascimento);
                Console.WriteLine();

                Console.WriteLine("Confirma exclusão?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");

                if (RecebeOpcao(2) == 1)
                {

                    pessoaList.Remove(pessoa);

                    repositorio.ExcluirPessoa(arquivo);
                }
            }
        }
        private static string RecebeStringTela()
        {
            string? recebeValor = Console.ReadLine();
            while (recebeValor != null &&
                    (recebeValor.Length == 0 ||
                    recebeValor.Trim() == ""))
            {
                Console.WriteLine("Informe o nome da pessoa corretamente...");
                recebeValor = Console.ReadLine();
            };

            return recebeValor;
        }
        private static DateTime RecebeDataTela()
        {
            DateTime data;

            while (!DateTime.TryParse(Console.ReadLine(), out data))
            {
                Console.WriteLine("Data inválida. Informe a data correta...");
            };

            return data;
        }

        private static int RecebeOpcao(int opcaoMax)
        {
            Boolean opcaoValida = (int.TryParse(Console.ReadLine(), out int opcao));
            while (!opcaoValida || opcao <= 0 || opcao > opcaoMax)
            {
                Console.WriteLine("Opcao inválida. Informe a opção correta...");
                opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            };

            return opcao;
        }
        // Dictionary 1-indice gravado 2-indice da lista
        private static int RecebeOpcaoIds(Dictionary<int, int> indices)
        {
            Boolean opcaoValida = (int.TryParse(Console.ReadLine(), out int opcao));
            while (!opcaoValida || !indices.ContainsKey(opcao))
            {
                Console.WriteLine("Opcao inválida. Informe a opção correta...");
                opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            };

            return indices[opcao];
        }
        public string? GetNomeArquivo()
        {
            string? arquivo = null;
            if (Directory.Exists(@"c:\temp"))
            {
                if (File.Exists(@"c:\temp\cadastro_pessoa.txt"))
                {
                    arquivo = @"c:\temp\cadastro_pessoa.txt";
                }
                else
                {
                    arquivo = @"c:\temp\cadastro_pessoa.txt";
                    using StreamWriter sw = File.CreateText(arquivo);
                    //File.Create(@"c:\temp\cadastro_pessoa.txt");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine(@"Diretório 'c:\temp' não existe. Deseja criar?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");

                if (RecebeOpcao(2) == 1)
                {
                    Directory.CreateDirectory(@"c:\temp");
                    File.Create(@"c:\temp\cadastro_pessoa.txt");
                    arquivo = @"c:\temp\cadastro_pessoa.txt";
                }
            }

            return arquivo;
        }
        public void PesquisaAniversarioHoje()
        {
            Console.Clear();
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            List<Pessoa> listaPessoas = repositorio.PesquisaAniversarioHoje();
            if (listaPessoas != null && listaPessoas.Count > 0)
            {
                Console.WriteLine(String.Format("Hoje, dia {0:dd/MM/yyyy}, é aniversário das seguintes pessoas:", DateTime.Now));

                foreach (Pessoa p in listaPessoas)
                {
                    Console.WriteLine(p.NomeCompleto);
                }
            }
            else
            {
                Console.WriteLine(String.Format("Não tem ninguém que faz aniversário hoje, dia {0:dd/MM/yyyy}.", DateTime.Now));
            }
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}