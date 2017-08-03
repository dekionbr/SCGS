using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class PatogenoMap : ClassMap<Patogeno>
    {

        public PatogenoMap()
        {
            Id(x => x.Id);

            Map(x => x.codigo)
                .UniqueKey("codigo");

            Map(x => x.nome);

            Map(x => x.descricao);


            Table("patogeno");

            
        }
    }
}
