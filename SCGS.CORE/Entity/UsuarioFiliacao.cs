using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class UsuarioFiliacao : EntidadeBase
    {
        public virtual Usuario UsuarioFiliado { get; set; }

        public virtual TipoFiliado TipoFiliado { get; set; }
    }
}
