using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity.SQL
{
    public class RelConsultas
    {
        public virtual int value { get; set; }
        public virtual DateTime datainicial { get; set; }
        public virtual DateTime datafinal { get; set; }
        public virtual string label { get; set; }
        public virtual int corte { get; set; }
        public virtual int ano { get; set; }
    }
    
}
