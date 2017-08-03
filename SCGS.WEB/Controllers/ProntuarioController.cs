using SCGS.CORE.Business;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGS.WEB.Controllers
{
    public class ProntuarioController : Controller
    {
        //
        // GET: /Prontuario/
        private Patogeno patogeno;
        private Usuario usuario;

        public ActionResult Prontuarios()
        {
            List<Prontuario> prontuarios = ProntuarioBusiness.ObterTodos();
            return View(prontuarios);
        }






        public ActionResult SalvarProntuario(Prontuario prontuario)
        {
            PatogenoProntuario pp = new PatogenoProntuario();
            prontuario.DATAPrescricao = DateTime.Now;

            usuario = TempData["usuarioTemp"] as Usuario;
            patogeno = TempData["patogeno"] as Patogeno;

            prontuario.Usuario = prontuario.Usuario == null ? usuario : prontuario.Usuario;

            prontuario.Funcionario = FuncionarioBusiness.ObterByMatricula(User.Identity.Name);
            prontuario = ProntuarioBusiness.Save(prontuario);
            pp.patogeno = patogeno;
            pp.prontuario = prontuario;

            PatogenoProntuarioBusiness.Save(pp);



            return RedirectToAction("Prontuarios");
        }


        public ActionResult Usuario(int Id) {
            usuario = UsuarioBusiness.Obter(Id);
            TempData["usuarioTemp"] = usuario;
            return Json(usuario);
        }




        public ActionResult Patogeno(int Id)
        {
             patogeno = PatogenoBusiness.Obter(Id);
            TempData["patogeno"] = patogeno;
            return Json(patogeno);
        }



      


        public ActionResult CriarProntuario()
        {
            // Depois de implementar a parte de login, os prontuários devem vir do usuário tipo médico ou enfermeiro.
            // Funcionario Medico = User.Identity.ObterFuncionario();
            ViewData["usuarios"] = UsuarioBusiness.ObterTodos();
            ViewData["patogeno"] = PatogenoBusiness.ObterTodos();
            var model = new Prontuario();
            return View("ProntuarioForm", model);
        }



        public ActionResult Edit(int id)
        {
            // Depois de implementar a parte de login, os prontuários devem vir do usuário tipo médico ou enfermeiro.
            // Funcionario Medico = User.Identity.ObterFuncionario();
            ViewData["usuarios"] = UsuarioBusiness.ObterTodos();
            ViewData["patogeno"] = PatogenoBusiness.ObterTodos();
            var model = ProntuarioBusiness.Obter(id);
            return View("ProntuarioForm", model);
        }


        public ActionResult Deletar (int Id)
        {
            var prontuario = ProntuarioBusiness.Obter(Id);

            ProntuarioBusiness.Deletar(prontuario);

            return RedirectToAction("Prontuarios");
        }

        public ActionResult Filtrar(string campo, string valor) {

            // Depois de implementar a parte de login, os prontuários devem vir do usuário tipo médico ou enfermeiro.
            // Funcionario Medico = User.Identity.ObterFuncionario();

            var Prontuarios = ProntuarioBusiness.ObterByParametroUsuario(campo, valor).Select(
                a => new
                {
                    Nome = a.Usuario.Nome,
                    RG = a.Usuario.RG,
                    Pescricao = a.Pescricao,
                    Id = a.Id

                });

            if (Prontuarios != null)
            {

                return Json(Prontuarios);
            }
            else
            {
                var error = new
                {
                    Error = "Error"
                };
                return Json(error);

            }
        }

    }
}
