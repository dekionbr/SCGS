using NHibernate;
using SCGS.CORE.Business;
using SCGS.CORE.Entity;
using SCGS.CORE.Entity.SQL;
using SCGS.CORE.Security;
using SCGS.WEB.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SCGS.WEB.Controllers
{
    public class RelatoriosController : Controller
    {
        //
        // GET: /Relatorios/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Relatorios/Atendimentos
        public ActionResult Atendimentos()
        {
            return View();
        }

        [Authorize(Roles = RoleManager.GERENTE_GERAL)]
        public ActionResult Endemias()
        {
            ViewData["unidades"] = UnidadeBusiness.ObterTodos();
            ViewData["equipes"] = EquipeBusiness.ObterTodos();

            return View();
        }

        [Authorize(Roles = RoleManager.GERENTE + " , " + RoleManager.EMFERMEIRO)]
        public ActionResult Consultas()
        {

            if (User.IsInRole(SCGS.CORE.Security.RoleManager.GERENTE_GERAL))
            {
                ViewData["unidades"] = UnidadeBusiness.ObterTodos();
                ViewData["equipes"] = EquipeBusiness.ObterTodos();
                ViewData["funcionarios"] = FuncionarioBusiness.ObterTodos().Where(a => a.TipoFuncionario == TipoFuncionario.Medico ||
                                                                                       a.TipoFuncionario == TipoFuncionario.Enfermeiro).ToList();
            }
            else if (User.IsInRole(SCGS.CORE.Security.RoleManager.GERENTE))
            {
                ViewData["equipes"] = EquipeBusiness.ObterTodos();
                ViewData["funcionarios"] = FuncionarioBusiness.ObterTodos().Where(a => a.TipoFuncionario == TipoFuncionario.Medico ||
                                                                                       a.TipoFuncionario == TipoFuncionario.Enfermeiro).ToList();
            }
            else
            {
                Funcionario enfermeiro = FuncionarioBusiness.ObterByMatricula(User.Identity.Name);
                ViewData["funcionarios"] = FuncionarioBusiness.ObterTodos().Where(a => (a.TipoFuncionario == TipoFuncionario.Medico ||
                                                                                        a.TipoFuncionario == TipoFuncionario.Enfermeiro) &&
                                                                                        a.Equipe.Equals(enfermeiro.Equipe.Id)).ToList();
            }

            return View();
        }


        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public ActionResult VisualizaPrevisaoEndemias(int corte, List<string> labels, string periodo, int? equipe = null)
        {
            string[] s = periodo.Split('a'); //de 01/06/2015 até 30/06/2015
            Regex rex = new Regex(@"\d{2}/\d{2}/\d{4}");
            Match m1 = rex.Match(s[0]);
            Match m2 = rex.Match(s[1]);
            RelatorioModel modelo = new RelatorioModel();
            List<string> xlabels = new List<string>();

            if (m1.Success && m2.Success)
            {
                List<DataSetModel> dataset = new List<DataSetModel>();
                Random rnd = new Random();

                // Tenta converter nos formatos esperados:
                DateTime initial;
                DateTime final;

                if (DateTime.TryParseExact(m1.Value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out initial) &&
                    DateTime.TryParseExact(m2.Value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out final))
                {
                    List<RelConsultas> relatorio = PatogenoProntuarioBusiness.GerarRelatorioPrevisao(corte, labels, initial, equipe).ToList<RelConsultas>();

                    string label = "de " + initial.ToString("dd/MM/yyyy") + " até " + final.ToString("dd/MM/yyyy");
                    string label2 = "de " + initial.AddYears(1).ToString("dd/MM/yyyy") + " até " + final.AddYears(1).ToString("dd/MM/yyyy");

                    List<string> labelsEncontradas = relatorio.Select(x => x.label).Distinct().ToList();

                    xlabels.Add(label);
                    xlabels.Add(label2);

                    foreach (var item in labelsEncontradas)
                    {
                        List<int> data = new List<int>();

                        RelConsultas relSelect = relatorio.Where(r => r.datainicial == initial && r.datafinal == final && r.label.Equals(item)).FirstOrDefault() ?? new RelConsultas() { value = 0 };
                        data.Add(relSelect.value);

                        int mediaValur = relatorio.Where(r => r.label.Equals(item)).Sum(m => m.value) / relatorio.Where(r => r.label.Equals(item)).Count();
                        data.Add(mediaValur);


                        int red = rnd.Next(1, 256);
                        int greem = rnd.Next(1, 256);
                        int blue = rnd.Next(1, 256);


                        dataset.Add(new DataSetModel()
                        {
                            label = item,
                            fillColor = "rgba(" + red + "," + greem + "," + blue + ",0.2)",
                            strokeColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                            pointColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                            pointStrokeColor = "#fff",
                            pointHighlightFill = "#fff",
                            pointHighlightStroke = "rgba(" + red + "," + greem + "," + blue + ",1)",
                            data = data.ToArray()
                        });
                    }
                }

                modelo.datasets = dataset.ToArray();
                modelo.labels = xlabels.ToArray();
            }
            return Json(modelo);
        }


        public ActionResult VisualizaEndemias(int corte, string dtini = null, string dtfim = null,
                                               int? equipe = null)
        {
            List<RelConsultas> relatorio = PatogenoProntuarioBusiness.GerarRelatorio(corte, dtini, dtfim, equipe).ToList<RelConsultas>();
            List<DataSetModel> dataset = new List<DataSetModel>();
            List<string> labels = new List<string>();
            Random rnd = new Random();

            if (relatorio.Count > 0)
            {
                int inicial = relatorio.ToList().Select(x => x.corte).OrderBy(x => x).First();
                int final = relatorio.ToList().Select(x => x.corte).OrderBy(x => x).Last();

                DateTime datainicial = relatorio.ToList().Select(x => x.datainicial).OrderBy(x => x).First();
                DateTime datafinal = relatorio.ToList().Select(x => x.datafinal).OrderBy(x => x).Last();

                relatorio.GroupBy(x => x.label).ToList().ForEach(x =>
                {
                    DateTime datatempinicial = datainicial;
                    DateTime datatempfinal = obterFinalData(corte, datatempinicial).AddDays(-1);
                    List<int> data = new List<int>();

                    while (datatempinicial < datafinal.AddDays(1))
                    {
                        string label = "de " + datatempinicial.ToString("dd/MM/yyyy") + " até " + datatempfinal.ToString("dd/MM/yyyy");

                        if (!labels.Contains(label))
                            labels.Add(label);

                        int value = x.Where(r => r.datainicial >= datatempinicial && r.datainicial <= datatempfinal).FirstOrDefault() != null ?
                                                        x.Where(r => r.datainicial >= datatempinicial && r.datainicial <= datatempfinal).FirstOrDefault().value : 0;

                        data.Add(value);

                        datatempinicial = obterFinalData(corte, datatempinicial);
                        datatempfinal = obterFinalData(corte, datatempfinal.AddDays(1)).AddDays(-1);
                    }

                    int red = rnd.Next(1, 256);
                    int greem = rnd.Next(1, 256);
                    int blue = rnd.Next(1, 256);

                    dataset.Add(new DataSetModel()
                    {
                        label = x.Key,
                        fillColor = "rgba(" + red + "," + greem + "," + blue + ",0.2)",
                        strokeColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        pointColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        pointStrokeColor = "#fff",
                        pointHighlightFill = "#fff",
                        pointHighlightStroke = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        data = data.ToArray()
                    });
                });
            }

            RelatorioModel modelo = new RelatorioModel()
            {
                datasets = dataset.ToArray(),
                labels = labels.ToArray()
            };

            return Json(modelo);
        }


        public ActionResult VisualizaConsultas(int corte, string dtini = null, string dtfim = null,
                                               int? funcionario = null)
        {
            List<RelConsultas> relatorio = ConsultaBusiness.GerarRelatorio(corte, dtini, dtfim, funcionario).ToList<RelConsultas>();
            List<DataSetModel> dataset = new List<DataSetModel>();
            List<string> labels = new List<string>();
            Random rnd = new Random();

            if (relatorio.Count > 0)
            {
                int inicial = relatorio.ToList().Select(x => x.corte).OrderBy(x => x).First();
                int final = relatorio.ToList().Select(x => x.corte).OrderBy(x => x).Last();

                DateTime datainicial = relatorio.ToList().Select(x => x.datainicial).OrderBy(x => x).First();
                DateTime datafinal = relatorio.ToList().Select(x => x.datafinal).OrderBy(x => x).Last();

                relatorio.GroupBy(x => x.label).ToList().ForEach(x =>
                {
                    DateTime datatempinicial = datainicial;
                    DateTime datatempfinal = obterFinalData(corte, datatempinicial).AddDays(-1);
                    List<int> data = new List<int>();

                    while (datatempinicial < datafinal.AddDays(1))
                    {
                        string label = "de " + datatempinicial.ToString("dd/MM/yyyy") + " até " + datatempfinal.ToString("dd/MM/yyyy");

                        if (!labels.Contains(label))
                            labels.Add(label);

                        int value = x.Where(r => r.datainicial >= datatempinicial && r.datainicial <= datatempfinal).FirstOrDefault() != null ?
                                                        x.Where(r => r.datainicial >= datatempinicial && r.datainicial <= datatempfinal).FirstOrDefault().value : 0;

                        data.Add(value);

                        datatempinicial = obterFinalData(corte, datatempinicial);
                        datatempfinal = obterFinalData(corte, datatempfinal.AddDays(1)).AddDays(-1);
                    }

                    int red = rnd.Next(1, 256);
                    int greem = rnd.Next(1, 256);
                    int blue = rnd.Next(1, 256);

                    dataset.Add(new DataSetModel()
                    {
                        label = x.Key,
                        fillColor = "rgba(" + red + "," + greem + "," + blue + ",0.2)",
                        strokeColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        pointColor = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        pointStrokeColor = "#fff",
                        pointHighlightFill = "#fff",
                        pointHighlightStroke = "rgba(" + red + "," + greem + "," + blue + ",1)",
                        data = data.ToArray()
                    });
                });
            }

            RelatorioModel modelo = new RelatorioModel()
            {
                datasets = dataset.ToArray(),
                labels = labels.ToArray()
            };


            return Json(modelo);
        }

        private DateTime obterFinalData(int corte, DateTime data)
        {
            switch (corte)
            {
                case 0:
                    return AddSemana(data);
                case 1:
                    return AddMensal(data);
                case 2:
                    return AddSemestral(data);
                case 3:
                    return AddAnual(data);
            }

            return DateTime.Now;
        }

        private DateTime AddSemana(DateTime data)
        {
            return data.AddDays(7);
        }

        private DateTime AddMensal(DateTime data)
        {
            int last = DateTime.DaysInMonth(data.Year, data.Month);
            return data.AddDays(last);
        }

        private DateTime AddSemestral(DateTime data)
        {
            int adddays = 0;

            for (int i = 0; i <= 5; i++)
            {
                adddays += DateTime.DaysInMonth(data.Year, data.AddMonths(i).Month);
            }

            return data.AddDays(adddays);
        }

        private DateTime AddAnual(DateTime data)
        {
            int adddays = 0;

            for (int i = 0; i <= 11; i++)
            {
                adddays += DateTime.DaysInMonth(data.Year, data.AddMonths(i).Month);
            }

            return data.AddDays(adddays);
        }

        //private List<object> obterSemanal(string dtini, string dtfim)
        //{
        //    DateTime inicial = DateTime.Parse(dtini);
        //    DateTime final = DateTime.Parse(dtfim);
        //    IList<RelConsultas> model = ConsultaBusiness.queryConsulta(dtini, dtfim);
        //    int contador = 0;
        //    string label = "de " + DateTime.Parse(dtini).ToString("dd/MM/yyyy");
        //    List<object> resultado = new List<object>();
        //    foreach (DateTime day in EachDay(inicial, final))
        //    {
        //        foreach (RelConsultas rel in model)
        //        {
        //            if (day.CompareTo(rel.data) == 0)
        //            {
        //                contador += rel.value;
        //            }
        //        }

        //        if (day.DayOfWeek.CompareTo(DayOfWeek.Monday) == 0)
        //        {
        //            label = " de " + day.ToString("dd/MM/yyyy");
        //        }
        //        if (day.DayOfWeek.CompareTo(DayOfWeek.Sunday) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");

        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );
        //            label = null;
        //            contador = 0;
        //        }
        //        else if (day.CompareTo(final) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");
        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }

        //    }

        //    return resultado;
        //}


        //private List<object> obterMensal(string dtini, string dtfim)
        //{
        //    DateTime inicial = DateTime.Parse(dtini);
        //    DateTime final = DateTime.Parse(dtfim);
        //    IList<RelConsultas> model = ConsultaBusiness.queryConsulta(dtini, dtfim);
        //    int contador = 0;
        //    string label = "de " + DateTime.Parse(dtini).ToString("dd/MM/yyyy");
        //    List<object> resultado = new List<object>();


        //    foreach (DateTime day in EachDay(inicial, final))
        //    {
        //        foreach (RelConsultas rel in model)
        //        {
        //            if (day.CompareTo(rel.data) == 0)
        //            {
        //                contador += rel.value;
        //            }
        //        }

        //        if (day.Day == 1)
        //        {
        //            label = " de " + day.ToString("dd/MM/yyyy");
        //        }
        //        if (day.Day.CompareTo(DateTime.DaysInMonth(day.Year, day.Month)) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");

        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }
        //        else if (day.CompareTo(final) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");
        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }

        //    }

        //    return resultado;
        //}


        //private List<object> obterSemestral(string dtini, string dtfim)
        //{
        //    DateTime inicial = DateTime.Parse(dtini);
        //    DateTime final = DateTime.Parse(dtfim);
        //    IList<RelConsultas> model = ConsultaBusiness.queryConsulta(dtini, dtfim);
        //    int contador = 0;
        //    string label = "de " + DateTime.Parse(dtini).ToString("dd/MM/yyyy");
        //    List<object> resultado = new List<object>();
        //    foreach (DateTime day in EachDay(inicial, final))
        //    {
        //        foreach (RelConsultas rel in model)
        //        {
        //            if (day.CompareTo(rel.data) == 0)
        //            {
        //                contador += rel.value;
        //            }
        //        }

        //        if (day.DayOfYear == (day.DayOfYear / 2) || day.DayOfYear == 1)
        //        {
        //            label = " de " + day.ToString("dd/MM/yyyy");
        //        }
        //        if (day.CompareTo(new DateTime(day.Year, 12, 31)) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");

        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }
        //        else if (day.CompareTo(final) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");
        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }

        //    }

        //    return resultado;
        //}


        //private List<object> obterByAnual(string dtini, string dtfim)
        //{
        //    DateTime inicial = DateTime.Parse(dtini);
        //    DateTime final = DateTime.Parse(dtfim);
        //    IList<RelConsultas> model = ConsultaBusiness.queryConsulta(dtini, dtfim);
        //    int contador = 0;
        //    string label = "de " + DateTime.Parse(dtini).ToString("dd/MM/yyyy");
        //    List<object> resultado = new List<object>();
        //    foreach (DateTime day in EachDay(inicial, final))
        //    {
        //        foreach (RelConsultas rel in model)
        //        {
        //            if (day.CompareTo(rel.data) == 0)
        //            {
        //                contador += rel.value;
        //            }
        //        }

        //        if (day.DayOfYear == 1)
        //        {
        //            label = " de " + day.ToString("dd/MM/yyyy");
        //        }
        //        if (day.CompareTo(new DateTime(day.Year, 12, 31)) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");

        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }
        //        else if (day.CompareTo(final) == 0)
        //        {
        //            label += " até " + day.ToString("dd/MM/yyyy");
        //            resultado.Add(
        //                             new
        //                             {
        //                                 label = label,
        //                                 value = contador
        //                             }
        //                        );

        //            label = null;
        //            contador = 0;
        //        }

        //    }

        //    return resultado;
        //}




        public ActionResult Cadastro()
        {
            return View();
        }

        //
        // GET: /Relatorios/Unidades
        public ActionResult Unidades(int IdUnidade)
        {
            IList<Unidade> Unidades = UnidadeBusiness.ObterTodas();

            var json = new JavaScriptSerializer().Serialize(Unidades);

            return JavaScript(json);
        }

        //
        // GET: /Relatorios/Equipes
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE_GERAL)]
        [HttpPost]
        public ActionResult Equipes(int Id)
        {
            var equipes = EquipeBusiness.ObterPorUnidade(Id).Select(x => new
            {
                Id = x.Id,
                Nome = x.Nome
            });

            var json = new JavaScriptSerializer().Serialize(equipes);

            return JavaScript(json);
        }

        //
        // GET: /Relatorios/Funcionarios
        [Authorize(Roles = SCGS.CORE.Security.RoleManager.GERENTE_GERAL)]
        [HttpPost]
        public ActionResult Funcionarios(int Id)
        {
            var funcionarios = FuncionarioBusiness.ObterPorEquipe(Id).Select(x => new
            {
                Id = x.Id,
                Nome = x.Nome
            });

            var json = new JavaScriptSerializer().Serialize(funcionarios);

            return JavaScript(json);
        }


        //public ActionResult MapaEndemias() {
        //    var prontuarios= ProntuarioBusiness.

        //    var data = 

        //    return View();
        //}
    }
}
