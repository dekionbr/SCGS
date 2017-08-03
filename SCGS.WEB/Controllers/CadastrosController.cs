using SCGS.CORE.Business;
using SCGS.CORE.Entity;
using SCGS.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SCGS.WEB.Controllers
{
    [Authorize(Roles = SCGS.CORE.Security.RoleManager.FUNCIONARIO)]
    public class CadastrosController : Controller
    {
        //
        // GET: /Cadastros/

        public ActionResult Index()
        {
            return View();
        }

        #region Controle de Funcionário

        //
        // GET: /Funcionarios/
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult Funcionarios()
        {
            List<Funcionario> model = FuncionarioBusiness.ObterTodos();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult SalvarFuncionario(FuncionarioModel model)
        {

            FuncionarioBusiness.Save(model.funcionario);

            return RedirectToAction("Funcionarios");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult SalvarContato(string idfuncionario, string tipocontato, string contato)
        {
            var funcionario = FuncionarioBusiness.Obter(Convert.ToInt32(idfuncionario));

            if (funcionario != null)
            {

                Contato c = new Contato();
                c.Pessoa = funcionario;

                foreach (TipoContato t in Enum.GetValues(typeof(TipoContato)))
                {
                    if (tipocontato.Equals(t.ToString()))
                        c.Tipo = t;
                }
                c.Valor = contato;

                ContatoBusiness.Save(c);
                var contatos = ContatoBusiness.ObterByPessoa(funcionario).Select(
                    a => new
                    {
                        Contato = a.Valor,
                        TipoContato = a.Tipo.ToString(),
                        Id = a.Id
                    }
                    );

                if (contatos != null)
                {

                    return Json(contatos);
                }

            }
            var error = new
            {
                Error = "Error",
                msg = "Selecione um Usuario"
                };
                return Json(error);

      
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult DeletarFuncionario(int Id)
        {
            var funcionario = FuncionarioBusiness.Obter(Id);
            FuncionarioBusiness.Deletar(funcionario);
            return RedirectToAction("Funcionarios");
        }

        //
        // GET: /Funcionario/
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult Funcionario()
        {
            return View();
        }

        //
        // GET: /EditarFuncionario/1
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult EditarFuncionario(int Id)
        {
            Funcionario funcionario = FuncionarioBusiness.Obter(Id);
            TempData["funcionariomodel"] = funcionario;
            return RedirectToAction("FuncionarioForm");
        }

        //
        //POST: /AddAndUpdateFuncionario/

        public ActionResult DeletarContato(string idcontato)
        {
            var contato = ContatoBusiness.Obter(Convert.ToInt32(idcontato));
            ContatoBusiness.Deletar(contato);
            var contatos = ContatoBusiness.ObterByPessoa(contato.Pessoa).Select(
                a => new
                {
                    Contato = a.Valor,
                    TipoContato = a.Tipo.ToString(),
                    Id = a.Id
                }
                );

            if (contatos != null)
            {

                return Json(contatos);
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

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult AddOrUpdateFuncionario(FuncionarioModel model)
        {

            var funcionario = model.funcionario;
            funcionario.Cidade = CidadeBusiness.Obter(Convert.ToInt32(model.Cidade));
            var equipe = model.Equipe;
            funcionario.Equipe = EquipeBusiness.Obter(Convert.ToInt32(model.Equipe));
            if (FuncionarioBusiness.Save(funcionario) != null)
            {
                TempData["msg"] = "Funcionario Salvo com sucesso!";

            }
            else
            {
                TempData["msg"] = "Error!";

            }

            return RedirectToAction("Funcionarios");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult FuncionarioForm()
        {
            var estados = obterEstados();
            ViewBag.Estados = estados;
            var cidades = new List<SelectListItem>();
            cidades.Add(new SelectListItem());
            ViewBag.Cidades = cidades;
            ViewBag.Equipes = obterEquipes();
            FuncionarioModel model = new FuncionarioModel();
             if (TempData["funcionariomodel"] != null){
                 Funcionario funcionario  = TempData["funcionariomodel"] as Funcionario;
                 model.funcionario = funcionario;
                model.contatos = ContatoBusiness.ObterByPessoa(model.funcionario).Count > 0 ? ContatoBusiness.ObterByPessoa(model.funcionario) : new List<Contato>();
            }
            else
            {
                model.contatos = new List<Contato>();
            }
            return View(model);
      
        }

        #endregion




        #region Controller Usuario

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult Usuario()
        {

            return View();
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult UsuarioForm()
        {
            ViewBag.Estados = obterEstados();
            var cidades = new List<SelectListItem>();
            cidades.Add(new SelectListItem());
            ViewBag.Cidades = cidades;
            UsuarioModel model = new UsuarioModel();
            if(TempData["usuariomodel"] != null){
                Usuario usuario = TempData["usuariomodel"] as Usuario;
               // model = iniciarUsuario();
                model.usuario = usuario;
                model.contatoList = ContatoBusiness.ObterByPessoa(usuario) != null ? ContatoBusiness.ObterByPessoa(usuario) : new List<Contato>();
            }
            else{
              model = iniciarUsuario();  
            }
            return View(model);
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        private UsuarioModel iniciarUsuario()
        {
            UsuarioModel model = new UsuarioModel();
            Contato contato = new Contato();
            Endereco endereco = new Endereco();
            Usuario usuario = new Usuario();
            usuario.DataCadastro = DateTime.Now;
            List<Contato> contatos = new List<Contato>();
            List<UsuarioFiliacao> filiacao = new List<UsuarioFiliacao>();
            usuario.Filiacao = filiacao;
            List<Prontuario> prontuarios = new List<Prontuario>();
            usuario.Prontuarios = prontuarios;
            model.contatoList = contatos;
            model.contato = contato;
            model.usuario = usuario;
            model.usuario.Endereco = endereco;

            return model;
        }


        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult DeletarUsuario(int Id)
        {
            var usuario = UsuarioBusiness.Obter(Id);
            UsuarioBusiness.Deletar(usuario);

            return RedirectToAction("Usuarios");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var usuario = model.usuario;
            //  model.contato.Usuario = usuario;
            //  Contato contato = model.contato;
            usuario.Endereco.Cidade = null;
            usuario.Endereco = EnderecoBusiness.Save(usuario.Endereco);
            usuario.DataCadastro = DateTime.Now;
            UsuarioBusiness.Save(usuario);
            return RedirectToAction("Usuarios");
        }

        

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult Usuarios()
        {
            List<Usuario> model = UsuarioBusiness.ObterTodos();
            return View(model);
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.AGENTE)]
        public ActionResult EditarUsuario(int Id)
        {
            TempData["usuariomodel"] = UsuarioBusiness.Obter(Id);
            return RedirectToAction("UsuarioForm");
        }

        #endregion



        public ActionResult SalvarContatoUsuario(int idusuario, string tipo, string valor)
        {
            var usuariotemp = UsuarioBusiness.Obter(Convert.ToInt32(idusuario));


            if (usuariotemp != null)
            {

                Contato c = new Contato();
                c.Pessoa = usuariotemp;

                foreach (TipoContato t in Enum.GetValues(typeof(TipoContato)))
                {
                    if (tipo.Equals(t.ToString()))
                        c.Tipo = t;
                }
                c.Valor = valor;

                ContatoBusiness.Save(c);
                var contatos = ContatoBusiness.ObterByPessoa(usuariotemp).Select(
                    a => new
                    {
                        Contato = a.Valor,
                        TipoContato = a.Tipo.ToString(),
                        Id = a.Id
                    }
                    );

                if (contatos != null)
                {

                    return Json(contatos);
                }


            }

            var error = new
                {
                    Error = "Error",
                    msg = "Selecione um Usuário primeiro"
                };
                return Json(error);

        }




        public ActionResult DeletarContatoUsuario(int idcontato)
        {
            var contato = ContatoBusiness.Obter(Convert.ToInt32(idcontato));
            ContatoBusiness.Deletar(contato);
            var contatos = ContatoBusiness.ObterByPessoa(contato.Pessoa).Select(
                a => new
                {
                    Contato = a.Valor,
                    TipoContato = a.Tipo.ToString(),
                    Id = a.Id
                }
                );

            if (contatos != null)
            {

                return Json(contatos);
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







        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult EquipeForm()
        {
            if (TempData["equipemodel"] != null)
            {
                EquipeModel model = new EquipeModel();
                var equipe = TempData["equipemodel"] as Equipe;
                model.equipe = equipe;
                model.enfermeirochefe = equipe.Enfermeiro.Id.ToString();
                model.medico = equipe.Medico.Id.ToString();
                model.enfermeirotecnico = equipe.EnfermeiroTecnico.Id.ToString();
                ViewBag.Enfermeiros = obterFuncionario(TipoFuncionario.Enfermeiro);
                ViewBag.Medicos = obterFuncionario(TipoFuncionario.Medico);
                ViewBag.Tecnicos = obterFuncionario(TipoFuncionario.EnfermeiroTecnico);
                var itensSelecionaveis = new List<SelectListItem>();
                ViewBag.MicroAreas = obterMicroAreas(itensSelecionaveis);
                List<MicroArea> micros = MicroAreaBusiness.ObterByEquipe(model.equipe);
                ViewBag.MicroAreaIn = micros;
                return View(model);
            }
            else
            {
                ViewBag.Enfermeiros = obterFuncionario(TipoFuncionario.Enfermeiro);
                ViewBag.Medicos = obterFuncionario(TipoFuncionario.Medico);
                ViewBag.Tecnicos = obterFuncionario(TipoFuncionario.EnfermeiroTecnico);
                ViewBag.MicroAreas = new List<MicroArea>();
                var itensSelecionaveis = new List<SelectListItem>();
                ViewBag.MicroAreas = obterMicroAreas(itensSelecionaveis);
                ViewBag.MicroAreaIn = new List<MicroArea>();
                return View();
            }
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult SalvarEquipe(EquipeModel model)
        {
            var equipe = model.equipe;
                equipe.Enfermeiro = FuncionarioBusiness.Obter(Convert.ToInt32(model.enfermeirochefe));
                equipe.EnfermeiroTecnico = FuncionarioBusiness.Obter(Convert.ToInt32(model.enfermeirotecnico));
                equipe.Medico = FuncionarioBusiness.Obter(Convert.ToInt32(model.medico));
                EquipeBusiness.Save(equipe);
            return RedirectToAction("Equipes");
        }


        private List<SelectListItem> obterMicroAreas(List<SelectListItem> itensSelecionaveis)
        {
            foreach (MicroArea m in MicroAreaBusiness.ObterTodos())
            {
                itensSelecionaveis.Add(

                    new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Descricao
                    }
                    );

            }
            return itensSelecionaveis;
        }



        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult EditarEquipe(int Id)
        {
            TempData["equipemodel"] = EquipeBusiness.Obter(Id);
            return RedirectToAction("EquipeForm");
        }


        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult DeletarEquipe(int Id)
        {
            Equipe model = EquipeBusiness.Obter(Id);
            EquipeBusiness.Deletar(model);
            return RedirectToAction("Equipes");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult Equipe()
        {


            return View();
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult Equipes()
        {
            List<Equipe> model = EquipeBusiness.ObterTodos();

            return View(model);
        }


        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult MicroArea()
        {
            List<MicroArea> model = MicroAreaBusiness.ObterTodos();
            return View(model);
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult MicroAreaDelete(int Id)
        {
            TempData["microareamodel"] = MicroAreaBusiness.Obter(Id);
            return RedirectToAction("MicroArea");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult MicroAreaEdit(int Id)
        {
            TempData["microareamodel"] = MicroAreaBusiness.Obter(Id);
            return RedirectToAction("MicroAreaForm");
        }


        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult MicroAreaForm()
        {
            var cidades = new List<SelectListItem>();

            if (TempData["microareamodel"] != null)
            {
                ViewBag.Equipes = obterEquipes();
                ViewBag.Estados = obterEstados();
                cidades.Add(new SelectListItem());
                ViewBag.Cidades = cidades;
                var micro = TempData["microareamodel"] as MicroArea;
                MicroAreaModel model = new MicroAreaModel();
                model.endereco = EnderecoBusiness.ObterByMicroArea(micro).FirstOrDefault();
                model.microarea = micro;
                model.equipe = micro.Equipe.Id.ToString();
                return View(model); 
            }
            ViewBag.Equipes = obterEquipes();
            ViewBag.Estados= obterEstados();
            cidades.Add(new SelectListItem());
            ViewBag.Cidades = cidades;
            return View();

        }


        [HttpPost]
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult SalvarMicroArea(MicroAreaModel model)
        {
            MicroArea microarea = model.microarea;
            microarea.Equipe = EquipeBusiness.Obter(Convert.ToInt32(model.equipe));
            microarea = MicroAreaBusiness.Save(microarea);
            model.endereco.MicroArea = microarea;
            model.endereco.Cidade = CidadeBusiness.Obter(Convert.ToInt32(model.cidade));
            EnderecoBusiness.Save(model.endereco);
            return RedirectToAction("MicroArea");
        }


        private List<SelectListItem> obterFuncionario(TipoFuncionario tipofuncionario)
        {
            List<Funcionario> agentes = FuncionarioBusiness.ObterTodos().Where(a => a.TipoFuncionario == tipofuncionario).ToList<Funcionario>();
            var itensSelecionaveis = new List<SelectListItem>();
            foreach (Funcionario f in agentes)
            {

                itensSelecionaveis.Add(
                        new SelectListItem
                        {
                            Value = f.Id.ToString(),
                            Text = f.Nome,
                        }

                    );
            }

            return itensSelecionaveis;
        }
        

        private List<SelectListItem> obterEstados()
        {
            List<Estado> estados = EstadoBusiness.ObterTodos();
            var itensSelecionaveis = new List<SelectListItem>();
            foreach (Estado e in estados)
            {

                itensSelecionaveis.Add(
                        new SelectListItem
                        {
                            Value = e.Id.ToString(),
                            Text = e.Nome
                        }

                    );
            }
            return itensSelecionaveis;
        }


        private List<SelectListItem> obterEquipes()
        {
            List<Equipe> equipes = EquipeBusiness.ObterTodos();
            var itensSelecionaveis = new List<SelectListItem>();
            foreach (Equipe e in equipes)
            {

                itensSelecionaveis.Add(
                        new SelectListItem
                        {
                            Value = e.Id.ToString(),
                            Text = e.Nome
                        }

                    );
            }
            return itensSelecionaveis;
        }



        public ActionResult PreencherCidades(int id)
        {
            return Json(obterCidades(id).ToList(), JsonRequestBehavior.AllowGet);
        }


        private List<Cidade> obterCidades( int id)
        {
            var estado = EstadoBusiness.Obter(id);
            return CidadeBusiness.ObterCidadeEstado(estado); ;
        }


        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE)]
        public ActionResult Unidades()
        {
            List<Unidade> model = UnidadeBusiness.ObterTodos();
            return View(model);
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE_GERAL)]
        public ActionResult SalvarUnidade(UnidadeModel model)
        {
            Unidade unidade = model.unidade;
            unidade.Gerente = FuncionarioBusiness.Obter(Convert.ToInt32(model.gerente));
            unidade.Cidade = CidadeBusiness.Obter(Convert.ToInt32(model.cidade));
            UnidadeBusiness.Save(unidade);
            return RedirectToAction("Unidades");
        }

        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE_GERAL)]
        public ActionResult UnidadeForm()
        {
            ViewBag.Gerentes = obterFuncionario(TipoFuncionario.Gerente);
            ViewBag.Estados = obterEstados();
            ViewBag.Cidades = new List<SelectListItem>();
            return View();
        }




        public ActionResult PatogenoForm()
        {
            Patogeno patogeno = TempData["patogeno"] != null ? TempData["patogeno"] as Patogeno : new CORE.Entity.Patogeno();
            return View(patogeno);
        }


        public ActionResult Patogeno()
        {
            List<Patogeno> patogenos = PatogenoBusiness.ObterTodos();
            return View(patogenos);
        }



        public ActionResult PesquisaPatogeno(string campo, string valor)
        {
            var patogenos = PatogenoBusiness.ObterByParametro(campo, valor).Select(
               a => new
               {
                    Id = a.Id,
                   Nome = a.nome,
                   Codigo = a.codigo,
                   Descricao = a.descricao

               });

            if (patogenos != null)
            {

                return Json(patogenos);
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


        public ActionResult SalvarPatogeno(Patogeno patogeno)
        {

            PatogenoBusiness.Save(patogeno);
            return RedirectToAction("Patogeno");
        }

        public ActionResult EditarPatogeno(int Id)
        {
            Patogeno patogeno = PatogenoBusiness.Obter(Id);
            TempData["patogeno"] = patogeno;
            return RedirectToAction("PatogenoForm");
        }

        public ActionResult DeletarPatogeno(int Id)
        {
            Patogeno patogeno = PatogenoBusiness.Obter(Id);
            PatogenoBusiness.Deletar(patogeno);
            return RedirectToAction("Patogeno");
        }


    }
}
