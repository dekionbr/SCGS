using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class PedidoModel
    {

        public Pedido pedido { get; set; }
        public Estoque medicamento { get; set; }
        public List<Estoque> estoque { get; set; }
        public Usuario usuario { get; set; }
        public string funcionario { get; set; }

    }
}