using System;
using System.Globalization;

namespace PessoaAniversario
{
    public class Pessoa
    {
        #region atributos
        private int _pessoaId;
        private string _nome;
        private string _sobrenome;
        private DateTime _dataNascimento;
        #endregion

        #region propriedades
        public int PessoaId
        {
            get { return _pessoaId; }
            set { _pessoaId = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Sobrenome
        {
            get { return _sobrenome; }
            set { _sobrenome = value; }
        }

        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set
            {
                string data = value.ToString();
                if (!DateTime.TryParse(data, out DateTime dataOk))
                {
                    throw new Exception("Data inválida.");
                }
                _dataNascimento = dataOk;
            }
        }
        public string NomeCompleto
        {
            get { return _nome + " " + _sobrenome; }
        }

        public List<Pessoa> pessoas = new();
        #endregion

        #region métodos
        public Pessoa() { }
        public Pessoa(int pessoaId, string nome, string sobrenome, DateTime dataNascimento)
        {
            this._pessoaId = pessoaId;
            this._nome = nome;
            this._sobrenome = sobrenome;
            this._dataNascimento = dataNascimento;
        }

        public DateTime ProximoAniversario()
        {

            DateTime dataProximoAniversario = new DateTime(DateTime.Now.Year, _dataNascimento.Month, _dataNascimento.Day, 0, 0, 0);
            if (DateTime.Compare(dataProximoAniversario, DateTime.Today) < 0)
            {
                dataProximoAniversario = dataProximoAniversario.AddYears(1);
            }
            return dataProximoAniversario;
        }
        public int CalculaDiasFaltantes()
        {
            DateTime dataAtual = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime dataAniversario = ProximoAniversario();
//            DateTime dataProximoAniversario = dataAniversario.ToDateTime(TimeOnly.Parse("00:00"));

            if (dataAtual.Month == dataAniversario.Month &&
                dataAtual.Day == dataAniversario.Day)
            {
                return 0;
            }
            int difDatas = (int)dataAtual.Subtract(dataAniversario).TotalDays;
            if (difDatas < 0) { difDatas *= -1; }

            return difDatas;
        }
        #endregion

    }
}