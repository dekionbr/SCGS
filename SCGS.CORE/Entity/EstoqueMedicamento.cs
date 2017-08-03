using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class EstoqueMedicamento:EntidadeBase
    {
        public virtual int qtd { get; set; }
        public virtual Medicamento medicamento { get; set; }
        public virtual Funcionario funcionarioresponsavel { get; set; }
        public virtual DateTime dtCadastro { get; set; }
        public virtual DateTime dtUltimaAlteracao { get; set; }
        public virtual Usuario usuariodestino { get; set; }
        public virtual Funcionario acs { get; set; }


    }
}
