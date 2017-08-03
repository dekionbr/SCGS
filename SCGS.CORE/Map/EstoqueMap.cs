using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class EstoqueMap : ClassMap<Estoque>
    {

        public EstoqueMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.Descricao);
            Map(x => x.Quantidade);
            Map(x => x.TipoEstoque).CustomType<TipoEstoque>();
            Map(x => x.CodigoLote);
            Map(x => x.DataEntrega);
            Map(x => x.DataValidade);
            Map(x => x.DataCadastro);
            Map(x => x.Fabricante);
            Map(x => x.Fornecedor);
            Map(x => x.EstoqueMinimo);

            References(x => x.Farmaceutico)
                .Cascade.None();
            Table("estoque");
        }
    }
}
