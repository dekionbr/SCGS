using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class EquipeMap : ClassMap<Equipe>
    {
        public EquipeMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.Descricao);

            HasMany(x => x.MicroAreas)
                .Cascade.None();
                        
            References(x => x.Medico)
                .NotFound.Ignore()
                .Cascade.None();

            References(x => x.EnfermeiroTecnico)
                .NotFound.Ignore()
                .Cascade.None();

            References(x => x.Enfermeiro)
                .Fetch.Join();

            References(x => x.Unidade)
                .Cascade.None();

            Table("equipe");
        }
    }
}
