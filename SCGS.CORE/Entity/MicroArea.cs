using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class MicroArea : EntidadeBase
    {
        public virtual IList<Endereco> Enderecos { get; set; }

        public virtual string Descricao { get; set; }
        
        public virtual Equipe Equipe{ get; set; }

    }
}
