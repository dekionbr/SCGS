using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class PedidoMap : ClassMap<Pedido>
    {
        public PedidoMap() {

            Id(x => x.Id);

            References(x => x.Agente)
                .Cascade.None();

            References(x => x.usuario)

                .Cascade.None();

            HasMany(x => x.Itens)
                .Cascade.All();

            Table("pedido");
        }

    }
}
