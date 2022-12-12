using PessoaAniversario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _IRepositorio
{
    internal interface IRepositorio
    {
        public List<Pessoa> CarregaPessoasLista(string arquivo);
        public void AdicionarPessoa(Pessoa pessoa, string arquivo);
        public void AlterarPessoa(string arquivo);
        public void ExcluirPessoa(string arquivo);
        public Dictionary<int, int> PesquisaPessoas(string nomePesq);
        public List<Pessoa> PesquisaAniversarioHoje();
    }
}
