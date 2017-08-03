using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Estado : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual string UF { get; set; }
    }
}
