using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class PatogenoProntuario : EntidadeBase
    {

        public virtual Patogeno patogeno { get; set; }

        public virtual Prontuario prontuario { get; set; }



    }
}
