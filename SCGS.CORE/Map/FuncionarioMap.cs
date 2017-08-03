using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class FuncionarioMap : SubclassMap<Funcionario>
    {
        public FuncionarioMap()
        {

            Map(x => x.Complemento);
            Map(x => x.CEP);
            Map(x => x.TipoLogradouro).CustomType<int>();
            Map(x => x.Logradouro);            
            Map(x => x.TipoFuncionario);
            Map(x => x.Numero);

            References(x => x.CR)
                .Cascade.All()
                .NotFound.Ignore();

            References(x => x.Equipe)
                .Cascade.None()
                .NotFound.Ignore();

            References(x => x.Cidade)
                .Cascade.None()
                .NotFound.Ignore();

            Table("funcionario");
        }
    }
}
