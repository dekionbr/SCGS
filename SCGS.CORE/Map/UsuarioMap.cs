using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class UsuarioMap : SubclassMap<Usuario>
    {
        public UsuarioMap()
        {

            References(x => x.Endereco);

            //HasMany(x => x.Prontuarios)
            //    .Access.None();

            
            //HasMany(x => x.Filiacao)
            //    .Access.None();

            Table("usuario");
        }

    }
}
