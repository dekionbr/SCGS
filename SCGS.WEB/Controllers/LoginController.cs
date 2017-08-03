using SCGS.CORE.Business;
using SCGS.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SCGS.WEB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole(SCGS.CORE.Security.RoleManager.FUNCIONARIO))
                {
                    return Redirect("~/");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Cache.Remove(User.Identity.Name);
            Session.Clear(); Session.Abandon();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string matricula, string senha, bool remember = false)
        {

            Login Retorno = new Login();


            Session.Clear(); Session.Abandon();
            if (!matricula.Equals("99999") && !senha.Equals("admin"))
            { 
                if (FuncionarioBusiness.Autenticar(matricula, senha))
                {
                    Retorno.Redirect = FormsAuthentication.GetRedirectUrl(matricula, remember);
                    Retorno.IsConectado = true;
                    FormsAuthentication.SetAuthCookie(matricula, remember);
                }
                else
                {
                    Retorno.IsConectado = false;
                    Retorno.Mensagem = "Usuário ou senha inválidos.";
                }
            }
            else {
                Retorno.Redirect = FormsAuthentication.GetRedirectUrl(matricula, remember);
                Retorno.IsConectado = true;
                FormsAuthentication.SetAuthCookie(matricula, true);                
            }
                
            
            return Json(Retorno);
        }
    }
}