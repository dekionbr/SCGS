using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class Patogeno: EntidadeBase
    {
        public virtual string codigo { get; set; }
        public virtual string nome { get; set; }
        public virtual string descricao { get; set; }

    }
}
