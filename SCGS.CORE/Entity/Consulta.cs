using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class Consulta : EntidadeBase
    {
        public virtual Usuario Usuario { get; set; }

        public virtual DateTime DataConsulta { get; set; }

        public virtual string obs{ get; set; }

        public virtual Turno Turno { get; set; }

        public virtual bool Confirmado { get; set; }

        public virtual bool cancelada { get; set; }

        public virtual TipoPrioridade Prioridade { get; set; }

        public virtual Funcionario medico { get; set; }

        public virtual Funcionario agente { get; set; }

    }
}
