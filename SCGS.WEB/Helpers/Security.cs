using SCGS.CORE.Business;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc.Html;

namespace SCGS.WEB.Helpers
{
    public static class Security
    {
        /// <summary>
        /// Extende a classe IPrincipal para conter o metodo ObterFuncionario apartir da sessão do usuário
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>Retorna Tipo Funcionário</returns>
        public static Funcionario ObterFuncionario(this IPrincipal principal)
        {
            if (!principal.Identity.Name.Equals("99999"))
            {
                return FuncionarioBusiness.ObterByMatricula(principal.Identity.Name) ?? new Funcionario() { Nome = "" };
            }

            return new Funcionario() { Nome = "Admin" };
        }
    }
}