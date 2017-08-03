using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class FuncionarioModel
    {
        public Funcionario funcionario { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Equipe { get; set; }
        public Contato contato { get; set; }
        public List<Contato> contatos { get; set; }


    }
}