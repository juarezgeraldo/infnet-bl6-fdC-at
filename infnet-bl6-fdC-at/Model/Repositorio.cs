using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Funcoes;
using PessoaAniversario;

namespace _Repositorio
{
    public class Repositorio
    {
        private readonly List<Pessoa> _listaPessoa = new();
        private int _pessoaId;

        public List<Pessoa> CarregaPessoasLista(string arquivo)
        {
            string[] dadosArquivo = File.ReadAllLines(arquivo);
            if (dadosArquivo.Length > 0)
            {
                foreach (string dados in dadosArquivo)
                {
                    Pessoa pessoa = new();
                    pessoa = JsonSerializer.Deserialize<Pessoa>(dados);
                    _listaPessoa.Add(pessoa);
                    _pessoaId = pessoa.PessoaId >= _pessoaId ? _pessoaId = pessoa.PessoaId : _pessoaId;
                }

                _pessoaId += 1;
            }
            return _listaPessoa;
        }
        public void AdicionarPessoa(Pessoa pessoa, string arquivo)
        {
            pessoa.PessoaId = _pessoaId;
            
            _listaPessoa.Add(pessoa);

            using (StreamWriter sw = File.AppendText(arquivo))
            {
                string jsonString = JsonSerializer.Serialize(pessoa);
                sw.WriteLine(jsonString);
            }

            _pessoaId += 1;
        }
        public void AlterarPessoa(string arquivo)
        {
            File.Delete(arquivo);
            using StreamWriter sw = File.AppendText(arquivo);
            {
                foreach(Pessoa pessoa in _listaPessoa)
                {
                    string jsonString = JsonSerializer.Serialize(pessoa);
                    sw.WriteLine(jsonString);
                }
            }
        }
        public void ExcluirPessoa(string arquivo)
        {
            File.Delete(arquivo);
            using StreamWriter sw = File.AppendText(arquivo);
            {
                foreach (Pessoa pessoa in _listaPessoa)
                {
                    string jsonString = JsonSerializer.Serialize(pessoa);
                    sw.WriteLine(jsonString);
                }
            }
        }
        public List<int> PesquisaPessoas(string nomePesq)
        {
            Boolean achouPessoa = false;
            List<int> pessoaIds = new();

            foreach (Pessoa p in _listaPessoa)
            {
                if (p.NomeCompleto.Contains(nomePesq, StringComparison.OrdinalIgnoreCase))
                {
                    if (!achouPessoa)
                    {
                        Console.Clear();
                        Console.WriteLine("Selecione qual pessoa quer ver");
                        achouPessoa = true;
                    }
                    Repositorio.ImprimirPessoa(p);
                    pessoaIds.Add(p.PessoaId);
                }
            }
            return pessoaIds;
        }
        public List<Pessoa> PesquisaAniversarioHoje()
        {
            List<Pessoa> listaPessoas = new();

            foreach (Pessoa p in _listaPessoa)
            {
                if (p.CalculaDiasFaltantes() == 0)
                {
                    listaPessoas.Add(p);
                }
            }
            return listaPessoas;
        }
        public static void ImprimirPessoa(Pessoa pessoa)
        {
            Console.WriteLine(pessoa.PessoaId + " - " + pessoa.NomeCompleto);
        }
    }
}
