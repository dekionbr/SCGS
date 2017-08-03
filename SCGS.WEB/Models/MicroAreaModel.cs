using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class MicroAreaModel
    {
        public MicroArea microarea { get; set; }
        public string equipe{ get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public Endereco endereco { get; set; }
        public List<Endereco> enderecos { get; set; }
        public virtual TipoLogradrouro TipoLogradouro { get; set; }


    }
}