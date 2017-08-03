using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Contato : EntidadeBase
    {
        public virtual TipoContato Tipo { get; set; }

        public virtual string Valor { get; set; }
            
        public virtual Pessoa Pessoa{ get; set; }

    }
}
