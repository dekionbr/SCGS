using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Usuario : Pessoa
    {
        public virtual Endereco Endereco { get; set; }

        public virtual IList<UsuarioFiliacao> Filiacao { get; set; }

        public virtual IList<Prontuario> Prontuarios { get; set; }

        
    }
}
