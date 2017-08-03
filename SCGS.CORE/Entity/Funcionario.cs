using SCGS.CORE.Util;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class Funcionario : Pessoa, IEndereco
    {
        public Funcionario() {
            DataCadastro = DateTime.Now;
        }

        public virtual TipoFuncionario TipoFuncionario { get; set; }

        public virtual Equipe Equipe { get; set; }

        public virtual CR CR { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual int CEP { get; set; }

        [Required]
        public virtual TipoLogradrouro TipoLogradouro { get; set; }

        public virtual int Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Logradouro { get; set; }
        
        public override string Matricula { get { return string.Concat(DataCadastro.ToString("yyyy"), (int) TipoFuncionario, Id); } }

        public virtual string Funcao()
        {
            return StringValue.Get(TipoFuncionario);
        }

        public virtual string NomeComTratamento() {
            return string.Concat(TipoFuncionario == TipoFuncionario.Medico ? "Dr (a) " : "Sr (a) ", Nome);
        }

    }
}
