using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class ProntuarioMap : ClassMap<Prontuario>
    {
        public ProntuarioMap()
        {
            Id(x => x.Id);

            Map(x => x.Pescricao);

            References(x => x.Usuario)
                .Cascade.None();

            Map(x => x.DATAPrescricao);

            HasMany(x => x.Medicamentos)
                .Cascade.None();           

            References(x => x.Funcionario);

            Table("prontuario");
        }
    }
}
