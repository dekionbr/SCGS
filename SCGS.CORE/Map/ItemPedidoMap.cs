using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class ItemPedidoMap : ClassMap<ItemPedido>
    {
        public ItemPedidoMap()
        {
            Id(x => x.Id);

            Map(x => x.Quantidade);

            References(x => x.Pedido)
                .Cascade.None();

            References(x => x.Estoque)
                .Cascade.None();

            Table("itempedido");
        }
    }
}
