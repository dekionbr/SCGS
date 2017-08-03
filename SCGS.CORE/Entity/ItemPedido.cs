using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class ItemPedido : EntidadeBase
    {
        public virtual int Quantidade { get; set; }       

        public virtual Pedido Pedido { get; set; }

        public virtual Estoque Estoque { get; set; }


    }
}
