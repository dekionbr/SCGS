using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class EntidadeBase
    {
        public virtual int Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var outro = obj as EntidadeBase;
            if (outro == null)
                return false;
            else
                return outro.Id.Equals(Id);
        }
    }
}
