using FluentNHibernate.Mapping;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Map
{
    public class CRMap : ClassMap<CR>
    {

        public CRMap()
        {
            Id(x => x.Id);
            
            Map(x => x.TipoCR).CustomType<TipoCR>();

            References(x => x.Estado)
                .Cascade.None();
            Table("cr");
        }
    }
}
