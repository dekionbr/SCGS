using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class UsuarioFiliacaoMap : ClassMap<UsuarioFiliacao>
    {

        public UsuarioFiliacaoMap()
        {
            Id(x => x.Id);

            Map(x => x.TipoFiliado).CustomType<TipoFiliado>();

            References(x => x.UsuarioFiliado)
                .Cascade.None();

            Table("usuariofiliacao");
        }
    }
}
