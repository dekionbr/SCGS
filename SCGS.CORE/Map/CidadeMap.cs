using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class CidadeMap : ClassMap<Cidade>
    {
        public CidadeMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);

            References(x => x.Estado)
                .Cascade.None();
            Table("cidade");
        }
    }
}
