using PessoaAniversario;
using System.Collections;
using _Repositorio;

namespace Funcoes
{
    public class Funcoes
    {
        //private Pessoa pessoa = new Pessoa();
        public List<Pessoa> PessoaList = new List<Pessoa>();
        private Repositorio repositorio = new Repositorio();

        public void cadastraPessoa()
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Cadastrar pessoas");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome da pessoa:");
            string nome = recebeStringTela().Trim();

            Console.WriteLine("Digite o sobrenome da pessoa:");
            string sobreNome = recebeStringTela().Trim();

            Console.WriteLine("Digite a data do aniversário no formato dd/mm/yyyy");
            DateOnly dataNascimento = recebeDataTela();

            Console.WriteLine();

            Console.WriteLine("Confirma inclusão?");
            Console.WriteLine("Nome           : " + nome + " " + sobreNome);
            Console.WriteLine("Data nascimento: {0:d}", dataNascimento);
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");

            if (recebeOpcao(2) == 1)
            {
                Pessoa pessoa = new Pessoa();
                pessoa.nome = nome;
                pessoa.sobrenome = sobreNome;
                pessoa.dataNascimento = dataNascimento;
                repositorio.AdicionarPessoa(pessoa);
            }
        }

        public void pesquisaPessoa()
        {
            Console.WriteLine("Gerenciador de aniversários");
            Console.WriteLine("===========================");
            Console.WriteLine();
            Console.WriteLine("Pesquisar pessoas");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Digite o nome/sobrenome ou parte do nome/sobrenome da pessoa:");
            string nomePesq = recebeStringTela().Trim();

            Boolean achouPessoa = false;

            List<Pessoa> pessoaList = repositorio.ListaPessoas();
            List<int> pessoaIds = new List<int>();

            foreach (Pessoa p in pessoaList)
            {
                if (p.nome.IndexOf(nomePesq, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.sobrenome.IndexOf(nomePesq, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!achouPessoa)
                    {
                        Console.Clear();
                        Console.WriteLine("Selecione qual pessoa quer ver");
                        achouPessoa = true;
                    }
                    repositorio.ImprimirPessoa(p);
                    pessoaIds.Add(p.pessoaId);
                }
            }

            if (achouPessoa)
            {
                Pessoa pessoa = pessoaList[recebeOpcaoIds(pessoaIds)];

                Console.Clear();
                Console.WriteLine("Gerenciador de aniversários");
                Console.WriteLine("===========================");
                Console.WriteLine();
                Console.WriteLine("Pesquisar pessoas");
                Console.WriteLine("-----------------");
                Console.WriteLine();
                Console.WriteLine("Dados da pessoa selecionada:");
                Console.WriteLine("Nome completo      : " + pessoa.nomeCompleto);
                Console.WriteLine("Data nascimento    : " + pessoa.dataNascimento);
                Console.WriteLine();
                Console.WriteLine("Próximo aniversário: " + pessoa.proximoAniversario());
                Console.WriteLine();
                int diasFaltantes = pessoa.calculaDiasFaltantes();
                Console.WriteLine(diasFaltantes == 0 ? "É HOJE!!!!!" : "Faltam " + diasFaltantes + " dias para o próximo aniversário:");

                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private string recebeStringTela()
        {
            string? recebeValor = Console.ReadLine();
            while (recebeValor.Length == 0 ||
                    recebeValor.Trim() == "")
            {
                Console.WriteLine("Informe o nome da pessoa corretamente...");
                recebeValor = Console.ReadLine();
            };

            return recebeValor;
        }
        private DateOnly recebeDataTela()
        {
            DateOnly data;

            while (!DateOnly.TryParse(Console.ReadLine(), out data))
            {
                Console.WriteLine("Data inválida. Informe a data correta...");
            };

            return data;
        }

        private int recebeOpcao(int opcaoMax)
        {
            int opcao = 0;
            Boolean opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            while (!opcaoValida || opcao <= 0 || opcao > opcaoMax)
            {
                Console.WriteLine("Opcao inválida. Informe a opção correta...");
                opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            };

            return opcao;
        }
        private int recebeOpcaoIds(List<int> listaIds)
        {
            int opcao = 0;
            Boolean opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            while (!opcaoValida || listaIds.IndexOf(opcao) < 0)
            {
                Console.WriteLine("Opcao inválida. Informe a opção correta...");
                opcaoValida = (int.TryParse(Console.ReadLine(), out opcao));
            };

            return opcao;
        }
        public string GetNomeArquivo()
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
                    File.Create(@"c:\temp\cadastro_pessoa.txt");
                    arquivo = @"c:\temp\cadastro_pessoa.txt";
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

                if (recebeOpcao(2) == 1)
                {
                    Directory.CreateDirectory(@"c:\temp");
                    File.Create(@"c:\temp\cadastro_pessoa.txt");
                    arquivo = @"c:\temp\cadastro_pessoa.txt";
                }
            }

            return arquivo;
        }
    }
}