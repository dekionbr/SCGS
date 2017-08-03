using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class UsuarioModel
    {

        public virtual Usuario usuario { get; set; }
        public virtual Unidade unidade{ get; set; }
        public virtual Contato contato { get; set; }
        public virtual UsuarioFiliacao filiacao { get; set; }
        public virtual List<Unidade> unidadeSelect { get; set; }
        public virtual List<UsuarioFiliacao> filiacaoList { get; set; }
        public virtual List<Contato> contatoList { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        
    
    }
}