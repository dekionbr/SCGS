using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Meta : EntidadeBase
    {
        public virtual DateTime DateInicio { get; set; }

        public virtual DateTime DateTermino { get; set; }

        public virtual int NumeroAtendimentos { get; set; }

        public virtual Funcionario Agente { get; set; }

    }
}
