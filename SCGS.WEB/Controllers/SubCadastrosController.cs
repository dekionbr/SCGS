using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGS.WEB.Controllers
{
    public class SubCadastrosController : Controller
    {
        // GET: SubCadastros
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult ContatoPessoa()
        {

            return View();
        }


        public ActionResult EditContatoPessoa(int Id)
        {

            return RedirectToAction("ContatoPessoa");
        }


        public ActionResult DeleteContatoPessoa()
        {

            return View();
        }

    }
}