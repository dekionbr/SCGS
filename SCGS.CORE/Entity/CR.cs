using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class CR : EntidadeBase
    {
        public virtual TipoCR TipoCR { get; set; }

        public virtual Estado Estado { get; set; }

    }
}
