using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funcoes;
using PessoaAniversario;

namespace _Repositorio
{
    public class Repositorio
    {
        private List<Pessoa> _listaPessoa = new List<Pessoa>();
        private int _pessoaId;

        public void AdicionarPessoa(Pessoa pessoa)
        {
            pessoa.pessoaId = _pessoaId;
            
            _listaPessoa.Add(pessoa);
            _pessoaId += 1;
        }
        public List<Pessoa> ListaPessoas()
        {
            return _listaPessoa;
        }
        public Pessoa PesquisaPessoa(Pessoa pessoa)
        {
            return _listaPessoa[_listaPessoa.IndexOf(pessoa)];
        }
        public void ImprimirPessoa(Pessoa pessoa)
        {
            Console.WriteLine(pessoa.pessoaId + " - " + pessoa.nomeCompleto);
        }
    }
}
