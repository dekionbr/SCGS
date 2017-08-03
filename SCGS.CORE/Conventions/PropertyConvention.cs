using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SCGS.CORE.Conventions
{
    class PropertyConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType == typeof(TimeSpan) ||
                instance.Property.PropertyType == typeof(TimeSpan?))
                instance.CustomType("TimeAsTimeSpan");
        }
    }
}
