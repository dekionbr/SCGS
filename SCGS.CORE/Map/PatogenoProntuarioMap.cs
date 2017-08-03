using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class PatogenoProntuarioMap : ClassMap<PatogenoProntuario>
    {
        public PatogenoProntuarioMap()
        {

            Id(x => x.Id);

            References(x => x.patogeno)
                    .Cascade.None();

            References(x => x.prontuario)
                    .Cascade.None();


            Table("patogeno_prontuario");
        }

   }
}
