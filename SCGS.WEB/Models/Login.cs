using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGS.WEB.Models
{
    public class Login
    {
        public bool IsConectado { get; set; }
        public string Redirect { get; set; }
        public string Mensagem { get; set; }
    }
}