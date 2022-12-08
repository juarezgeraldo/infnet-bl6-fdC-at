using System;
using System.Globalization;

namespace PessoaAniversario
{
    public class Pessoa
    {
        #region atributos
        private int? _pessoaId;
        private string? _nome;
        private string? _sobrenome;
        private DateOnly _dataNascimento;
        #endregion

        #region propriedades
        public int pessoaId
        {
            get { return _pessoaId.Value; }
            set { _pessoaId = value; }
        }
        public string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string sobrenome
        {
            get { return _sobrenome; }
            set { _sobrenome = value; }
        }

        public DateOnly dataNascimento
        {
            get { return _dataNascimento; }
            set
            {
                string data = value.ToString();
                if (!DateOnly.TryParse(data, out DateOnly dataOk))
                {
                    throw new Exception("Data inválida.");
                }
                _dataNascimento = dataOk;
            }
        }
        public string nomeCompleto
        {
            get { return _nome + " " + _sobrenome; }
        }

        public List<Pessoa> pessoas = new List<Pessoa>();
        #endregion

        #region métodos
        public Pessoa() { }
        public Pessoa(int pessoaId, string nome, string sobrenome, DateOnly dataNascimento)
        {
            this._pessoaId = pessoaId;
            this._nome = nome;
            this._sobrenome = sobrenome;
            this._dataNascimento = dataNascimento;
        }

        public DateOnly proximoAniversario()
        {

            DateTime dataProximoAniversario = new DateTime(DateTime.Now.Year, _dataNascimento.Month, _dataNascimento.Day, 0, 0, 0);
            if (DateTime.Compare(dataProximoAniversario, DateTime.Now) < 0)
            {
                dataProximoAniversario = dataProximoAniversario.AddYears(1);
            }
            return DateOnly.FromDateTime(dataProximoAniversario);
        }
        public int calculaDiasFaltantes()
        {
            DateTime dataAtual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateOnly dataAniversario = proximoAniversario();
            DateTime dataProximoAniversario = dataAniversario.ToDateTime(TimeOnly.Parse("00:00"));

            if (dataAtual.Month == dataAniversario.Month &&
                dataAtual.Day == dataAniversario.Day)
            {
                return 0;
            }
            int difDatas = (int)dataAtual.Subtract(dataProximoAniversario).TotalDays;
            if (difDatas < 0) { difDatas = difDatas * -1; }

            return difDatas;
        }
        #endregion

    }
}