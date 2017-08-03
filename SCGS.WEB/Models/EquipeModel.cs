using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class EquipeModel
    {
        public Equipe equipe { get; set; }
        public string enfermeirochefe { get; set; }
        public string enfermeirotecnico{ get; set; }
        public string medico { get; set; }
        public MicroArea microarea { get; set; }
        public List<Funcionario> enfermeirochefeSelect { get; set; }
        public List<Funcionario> enfermeirotecnicoSelect { get; set; }
        public List<Funcionario> medicoSelect { get; set; }
        public List<MicroArea> microareaSelect { get; set; }
    
    
    
    
    }
}