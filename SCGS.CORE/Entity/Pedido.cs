using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Pedido : EntidadeBase
    {
        public virtual Funcionario Agente { get; set; }
        public virtual IList<ItemPedido> Itens { get; set; }
        public virtual Usuario usuario{ get; set; }


    }
}
