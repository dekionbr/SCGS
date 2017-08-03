using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGS.WEB.Controllers
{
    [Authorize(Roles =  SCGS.CORE.Security.RoleManager.AGENTE)]
    public class AtendimentoController : Controller
    {
        //
        // GET: /Atendimento/

        public ActionResult Atendimento()
        {
            return View();
        }

    }
}
