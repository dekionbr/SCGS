using SCGS.CORE.Business;
using SCGS.CORE.Entity;
using SCGS.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGS.WEB.Controllers
{
    [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
    public class ConsultaController : Controller
    {
        //
        // GET: /Consulta/
        public ActionResult AgendamentoConsulta()
        {
            AgendamentoConsultaModel model = new AgendamentoConsultaModel();
            model.usuarios = new List<CORE.Entity.Usuario>();
            return View(model);
        }
        
        public ActionResult Consulta()
        {
            List<Consulta> model;
            if (TempData["consultas"] == null)
            {
                model = ConsultaBusiness.ObterTodosSemCanceladas();

            }
            else
            {
                model = TempData["consultas"] as List<Consulta>;
            }

            return View(model);
        }


        public ActionResult FiltroConsulta(string dtde, string dtate)
        {
            List<Consulta> model = ConsultaBusiness.ObterByPeriodo(DateTime.Parse(dtde), DateTime.Parse(dtate));
            TempData["consultas"] = model;
            return RedirectToAction("Consulta", model);
        }




        public ActionResult ConfirmarConsulta(int Id)
        {
            Consulta consulta = ConsultaBusiness.Obter(Id);
            consulta.Confirmado = true;
            ConsultaBusiness.Save(consulta);
            return RedirectToAction("Consulta");
        }
        
        public ActionResult CancelarConsulta(int Id)
        {
            Consulta consulta = ConsultaBusiness.Obter(Id);
            consulta.cancelada = true;
            ConsultaBusiness.Save(consulta);
            return RedirectToAction("Consulta");
        }
        
        public ActionResult SalvarConsulta(AgendamentoConsultaModel model)
        {
            Consulta consulta = model.consulta;
            consulta.Usuario = UsuarioBusiness.Obter(model.usuario.Id);
            consulta.medico = FuncionarioBusiness.Obter(Convert.ToInt32(model.medico));
            ConsultaBusiness.Save(consulta);
            return RedirectToAction("Consulta");
        }
        
        public ActionResult Agendar(int Id)
        {
            var usuario = UsuarioBusiness.Obter(Id);
            AgendamentoConsultaModel model = new AgendamentoConsultaModel();
            model.usuario = usuario;
            model.consulta = new CORE.Entity.Consulta();
            
            TempData["AgModel"] = model;
            return RedirectToAction("AgendamentoConsultaForm");
        }
        
        private static List<SelectListItem> obterMedicos()
        {
            var medicos = FuncionarioBusiness.ObterTodos().Where(a => a.TipoFuncionario == CORE.Entity.TipoFuncionario.Medico).ToList<Funcionario>();
            List<SelectListItem> itens = new List<SelectListItem>();
            foreach (Funcionario f in medicos)
            {
                itens.Add(
                    new SelectListItem { Text = f.Nome, Value = f.Id.ToString() }
                    );
            }

            return itens;
        }

        public ActionResult AgendamentoConsultaForm()
        {
            ViewBag.Medicos = obterMedicos();
            return View(TempData["AgModel"] as AgendamentoConsultaModel);
        }

        public ActionResult Cancelar(int Id)
        {
            return View();
        }

        public ActionResult PesquisarUsuario(string campo, string valor)
        {
            var usuarios = UsuarioBusiness.ObterByParametro(campo, valor).Select(
                a => new
                {
                    Nome = a.Nome,
                    CPF = a.CPF,
                    RG = a.RG,
                    DataNascimento = a.DataNascimento.Day+"/"+a.DataNascimento.Month+"/"+a.DataNascimento.Year,
                    Id = a.Id

                });

            if (usuarios != null)
            {

                return Json(usuarios);
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


        public ActionResult ConsultaForm()
        {
            return View();
        }

    }
}
