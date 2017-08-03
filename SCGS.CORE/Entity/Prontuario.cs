using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Prontuario : EntidadeBase
    {

        public virtual IList<Pedido> Medicamentos { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual string Pescricao { get; set; }

        public virtual DateTime DATAPrescricao { get; set; }

        public virtual Funcionario Funcionario { get; set; }
    }
}
