using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class UnidadeModel
    {
        public virtual Unidade unidade { get; set; }
        public virtual string gerente { get; set; }
        public virtual string estado { get; set; }
        public virtual string cidade { get; set; }
        public virtual Endereco endereco { get; set; }
        public virtual List<Funcionario> gerenteSelect { get; set; }
        public virtual List<Cidade> cidadeSelect { get; set; }
      
    }
}