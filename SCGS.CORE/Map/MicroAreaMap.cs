using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class MicroAreaMap : ClassMap<MicroArea>
    {
        public MicroAreaMap()
        {
            Id(x => x.Id);

            Map(x => x.Descricao);
            
            References(x => x.Equipe)
                .Cascade.None();

            HasMany(x => x.Enderecos)
                .Cascade.None();

            Table("itempedido");
        }
    }
}
