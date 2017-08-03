using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.WEB.Models
{
    public class AgendamentoConsultaModel
    {
        public Consulta consulta { get; set; }
        public List<Usuario> usuarios { get; set; }
        public Usuario usuario { get; set; }
        public string medico { get; set; }
        public List<Funcionario> medicos { get; set; }

    }
}
