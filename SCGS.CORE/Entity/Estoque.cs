using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class Estoque : EntidadeBase
    {
        public virtual string Nome { get; set; }

        public virtual int EstoqueMinimo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual int CodigoLote { get; set; }

        public virtual DateTime DataValidade { get; set; }

        public virtual DateTime DataCadastro{ get; set; }

        public virtual DateTime DataEntrega { get; set; }

        public virtual string Fabricante { get; set; }

        public virtual string Fornecedor { get; set; }

        public virtual int Quantidade { get; set; }

        public virtual TipoEstoque TipoEstoque { get; set; }

        public virtual Funcionario Farmaceutico { get; set; }
    }
}
