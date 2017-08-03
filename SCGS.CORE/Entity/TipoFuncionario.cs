using SCGS.CORE.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public enum TipoFuncionario
    {
        [StringValue("Gerente Geral")]
        GerenteGeral,
        [StringValue("Gerente")]
        Gerente,
        [StringValue("Médico")]
        Medico,
        [StringValue("Enfermeiro")]
        Enfermeiro,
        [StringValue("Enfermeiro Técnico")]
        EnfermeiroTecnico,
        [StringValue("Agente")]
        Agente,
        [StringValue("Farmacêutico")]
        Farmaceutico,
        [StringValue("Funcionário")]
        Funcionario,
        [StringValue("Admin")]
        Admin
    }


}
