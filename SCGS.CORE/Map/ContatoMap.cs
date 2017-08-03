using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class ContatoMap : ClassMap<Contato>    
    {
        public ContatoMap()
        {
            Id(x => x.Id);

            Map(x => x.Tipo).CustomType<TipoContato>();

            Map(x => x.Valor);

            References(x => x.Pessoa)
                .Cascade.None();

            Table("contato");

        }
    }
}
