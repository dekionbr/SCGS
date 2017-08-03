using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.RG);
            Map(x => x.CPF);
            Map(x => x.Senha);
            Map(x => x.DataNascimento);
            Map(x => x.DataCadastro);

            HasMany(x => x.Contatos)
                .Cascade.All()
                .OrderBy("Tipo");

            References(x => x.Unidade)
                .Cascade.None();

            Table("pessoa");
        }
    }
}
