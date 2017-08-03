using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class ConsultaMap : ClassMap<Consulta>
    {
        public ConsultaMap() {



            Id(x => x.Id);

            Map(x => x.DataConsulta);

            Map(x => x.Turno).CustomType<int>();

            Map(x => x.Confirmado);

            Map(x => x.cancelada);

            Map(x => x.obs);

           Map(x => x.Prioridade).CustomType<int>();

            References(x => x.Usuario)
                .Cascade.None();

            References(x => x.medico)
              .Cascade.None();

            References(x => x.agente)
              .Cascade.None();

            Table("consulta");

        }
    }
}
