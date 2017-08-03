using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity.SQL
{
    public class RelMapa
    {
        public virtual string Bairro { get; set; }
        public virtual string Patogeno { get; set; }
        public virtual int Quantidade { get; set; }
    }
}
