using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Equipe : EntidadeBase
    {
        public virtual string Nome { get; set; }

        public virtual IList<MicroArea> MicroAreas { get; set; }

        public virtual string Descricao { get; set; }

        public virtual Funcionario EnfermeiroTecnico { get; set; }

        public virtual Funcionario Medico { get; set; }

        public virtual Funcionario Enfermeiro { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
