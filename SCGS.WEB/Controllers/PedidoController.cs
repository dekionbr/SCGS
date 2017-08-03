using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGS.WEB.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Pedido()
        {
            return View();
        }



        public ActionResult PedidoForm()
        {
            return View();
        }
    }

}