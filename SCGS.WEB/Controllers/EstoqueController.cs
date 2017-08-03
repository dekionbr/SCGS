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
    public class EstoqueController : Controller
    {
        //

        // GET: Pedido
        public ActionResult Pedido()
        {
            return View();
        }

        // GET: Pedidos
        public ActionResult Pedidos()
        {
            return View();
        }


        public ActionResult PedidoForm()
        {

            return View(TempData["pedidoModel"] as PedidoModel);
        }




        public ActionResult PedidoMedicamento(int Id)
        {
            var usuario = UsuarioBusiness.Obter(Id);
            PedidoModel model = new PedidoModel();
            model.estoque = EstoqueBusiness.ObterTodos();
            model.usuario = usuario;
            model.pedido = new CORE.Entity.Pedido();
            TempData["pedidoModel"] = model;
            return RedirectToAction("PedidoForm");
        }




        public ActionResult SelecionarUsuario(int Id)
        {
            TempData["Usuario"] = UsuarioBusiness.Obter(Id);
            Usuario u = TempData["Usuario"] as Usuario;
            TempData["NomeUsuario"] = u.Nome;
            Prontuario model = ViewBag.Prontuario;
            return RedirectToAction("ProntuarioForm", model);
        }


        public ActionResult ProntuarioForm()
        {
            ViewData["usuarios"] = UsuarioBusiness.ObterTodos();
            Prontuario model = new Prontuario();
            return View(model);
        }


        public ActionResult Prontuario()
        {
            List<Prontuario> model = ProntuarioBusiness.ObterTodos();
            return View(model);
        }




        public ActionResult SalvarMedicamento( int  [] medicamentos )
        {
            
            return RedirectToAction("PedidoForm");
        }

        // GET: /Estoque/
        public ActionResult Estoque()
        {
            List<Estoque> model = EstoqueBusiness.ObterTodos();
            return View(model);
        }


        public ActionResult EstoqueForm()
        {
            Estoque model = TempData["model"] !=null ? TempData["model"] as Estoque : new Estoque();
           return View(model);
        }


        public ActionResult SalvarEstoque(Estoque model)
        {
            EstoqueBusiness.Save(model);
            return RedirectToAction("Estoque");
        }

        public ActionResult EditarEstoque(int Id)
        {
            TempData["model"] = EstoqueBusiness.Obter(Id);
            return RedirectToAction("EstoqueForm");
        }


        public ActionResult DeletarEstoque(int Id)
        {
            Estoque model = EstoqueBusiness.Obter(Id);
            EstoqueBusiness.Deletar(model);
            return RedirectToAction("Estoque");
        }
    }
}
