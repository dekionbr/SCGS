using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class SistemaExternoMap : ClassMap<SistemaExterno>
    {

        public SistemaExternoMap()
        {
            Id(x => x.Id);

            Map(x => x.CNPJ);
            Map(x => x.Login);
            Map(x => x.Senha);
            Map(x => x.TipoLogradouro).CustomType<TipoLogradrouro>();
            Map(x => x.Logradouro);
            Map(x => x.Numero);
            Map(x => x.CEP);
            Map(x => x.Complemento);

            References(x => x.Cidade)
                .Cascade.None();

            Table("sistemaexterno");
        }
    }
}
