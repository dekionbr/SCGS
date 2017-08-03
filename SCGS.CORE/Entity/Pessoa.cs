using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace SCGS.CORE.Entity
{
    public class Pessoa : EntidadeBase
    {
        public Pessoa() {
            DataCadastro = DateTime.Now;
        }

        public virtual string Nome { get; set; }

        public virtual string RG { get; set; }

        public virtual string CPF { get; set; }

        public virtual DateTime DataNascimento { get; set; }
        
        
        public virtual DateTime DataCadastro { get; set; }

        [Required]        
        public virtual Unidade Unidade { get; set; }

        public virtual string Senha { get; set; }

        public virtual IList<Contato> Contatos { get; set; }

        public virtual string Matricula { get { return string.Concat(DataCadastro.ToString("yyyy"), Id); } }

    }
}
