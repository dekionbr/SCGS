using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.UF);
            Table("estado");
        }
    }
}
