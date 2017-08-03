using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Unidade : EntidadeBase, IEndereco
    {

        public virtual Funcionario Gerente { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual int CNPJ { get; set; }

        public virtual int CEP { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Bairro { get; set; }

        public virtual TipoLogradrouro TipoLogradouro { get; set; }

        public virtual int Numero { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual string Endereco
        {
            get;
            set;
        }

        public virtual List<Equipe> Equipes { get; set; }

        public virtual string Nome {
            get {
                return string.Concat("Unidade ", Logradouro);
            }
        }
    }
}
