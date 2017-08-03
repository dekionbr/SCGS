using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using SCGS.CORE.Entity;
using SCGS.CORE.Entity.SQL;
using SCGS.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class PatogenoProntuarioBusiness
    {


        public static PatogenoProntuario Obter(int Id)
        {
            var PatogenoProntuario = (
                from r in Session.Current.QueryOver<PatogenoProntuario>()
                where r.Id == Id
                select r).SingleOrDefault();

            return PatogenoProntuario;
        }

        public static PatogenoProntuario Save(PatogenoProntuario PatogenoProntuario)
        {
            using (var scope = new TransactionScope())
            {
                PatogenoProntuario = Session.Current.Merge<PatogenoProntuario>(PatogenoProntuario);
                if (PatogenoProntuario.Id != 0)
                    Session.Current.Update(PatogenoProntuario);
                else
                    Session.Current.Save(PatogenoProntuario);
                scope.Complete();
            }
            return PatogenoProntuario;
        }

        public static PatogenoProntuario Deletar(PatogenoProntuario PatogenoProntuario)
        {
            using (var scope = new TransactionScope())
            {
                PatogenoProntuario = Session.Current.Merge<PatogenoProntuario>(PatogenoProntuario);
                if (PatogenoProntuario.Id != 0)
                    Session.Current.Delete(PatogenoProntuario);
                scope.Complete();
            }
            return PatogenoProntuario;
        }

        public static List<PatogenoProntuario> ObterByParametroUsuario(string campo, string valor)
        {
            var PatogenoProntuarios = (
                 from p in Session.Current.CreateCriteria<PatogenoProntuario>().Future<PatogenoProntuario>()
                 join u in Session.Current.CreateCriteria<Prontuario>()
                 .Add(Restrictions.Like(campo, valor, MatchMode.Anywhere)).Future<Usuario>()
                    on p.prontuario.Usuario.Id equals u.Id
                 select p).ToList();

            return PatogenoProntuarios;

        }

        public static List<PatogenoProntuario> ObterTodos()
        {
            var PatogenoProntuarios = (
                from r in Session.Current.CreateCriteria<PatogenoProntuario>().List<PatogenoProntuario>()
                select r).ToList();

            return PatogenoProntuarios;
        }

        public static IList<RelConsultas> GerarRelatorioPrevisao(int corte, List<string> labels, DateTime inicio, int? IdEquipe = null)
        {

            ICriteria ct = Session.Current.CreateCriteria<PatogenoProntuario>("pp")
                                      .CreateCriteria("pp.patogeno", "pa", NHibernate.SqlCommand.JoinType.InnerJoin)
                                      .CreateCriteria("pp.prontuario", "pr", NHibernate.SqlCommand.JoinType.InnerJoin)
                                      .CreateCriteria("pr.Funcionario", "u", NHibernate.SqlCommand.JoinType.InnerJoin)
                                      .CreateCriteria("u.Equipe", "e", NHibernate.SqlCommand.JoinType.InnerJoin);

            ProjectionList prod = Projections.ProjectionList()
                 .Add(Projections.RowCount(), "value")
                 .Add(Projections.GroupProperty(Projections.Property("pa.nome")), "label");

            ct.Add(ObterCortePrevisao(corte, inicio));

            if (IdEquipe != null)
            {
                ct.Add(Restrictions.Eq(Projections.Property("e.Id"), IdEquipe.Value));
            }

            if (labels.Count > 0)
            {
                ct.Add(Restrictions.In(Projections.Property("pa.nome"), labels));
            }

            return ct.SetProjection(ObterCorteConsulta(prod, corte))
                 .AddOrder(Order.Asc(Projections.Property("pr.DATAPrescricao")))
                 .SetResultTransformer(
                    NHibernate.Transform.Transformers
                                        .AliasToBean(typeof(RelConsultas))
                                       ).List<RelConsultas>();
        }

        public static IList<RelConsultas> GerarRelatorio(int corte, string inicio, string fim, int? IdEquipe = null)
        {
            ICriteria ct = Session.Current.CreateCriteria<PatogenoProntuario>("pp")
                                          .CreateCriteria("pp.patogeno", "pa", NHibernate.SqlCommand.JoinType.InnerJoin)
                                          .CreateCriteria("pp.prontuario", "pr", NHibernate.SqlCommand.JoinType.InnerJoin)
                                          .CreateCriteria("pr.Funcionario", "u", NHibernate.SqlCommand.JoinType.InnerJoin)
                                          .CreateCriteria("u.Equipe", "e", NHibernate.SqlCommand.JoinType.InnerJoin);

            ProjectionList prod = Projections.ProjectionList()
                 .Add(Projections.RowCount(), "value")
                 .Add(Projections.GroupProperty(Projections.Property("pa.nome")), "label");


            //DATEADD(dd, -(DATEPART(dw, WeddingDate)-1), WeddingDate)

            if (!String.IsNullOrEmpty(inicio) &&
                !String.IsNullOrEmpty(fim))
            {
                DateTime _inicio = DateTime.Parse(inicio);
                DateTime _fim = DateTime.Parse(fim);
                ct.Add(Restrictions.Between("pr.DATAPrescricao", _inicio, _fim));
            }

            if (IdEquipe != null)
            {
                ct.Add(Restrictions.Eq(Projections.Property("e.Id"), IdEquipe.Value));
            }

            //ct.Add(Restrictions.Eq(Projections.Property("c.Confirmado"), true));

            return ct.SetProjection(ObterCorteConsulta(prod, corte))
                     .AddOrder(Order.Asc(Projections.Property("pr.DATAPrescricao")))
                     .SetResultTransformer(
                        NHibernate.Transform.Transformers
                                            .AliasToBean(typeof(RelConsultas))
                                           ).List<RelConsultas>();
        }


        public static ICriterion ObterCortePrevisao(int corte, DateTime inicio)
        {
            ICriterion crid = null;


            switch (corte)
            {
                case 0:

                    crid = Restrictions.Eq(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                           "WEEK(?1)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), inicio.WeekOfYear());

                    break;
                case 1:

                    crid = Restrictions.Eq(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MONTH(?1)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), inicio.Month);

                    break;
                case 2:
                    crid = Restrictions.Eq(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "CEILING(MONTH(?1) / 6)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), inicio.SemesterOfYear());
                    break;
                case 3:
                    crid = Restrictions.Eq(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "YEAR(?1)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), inicio.Year);
                    break;
            }

            return crid;
        }

        public static ProjectionList ObterCorteConsulta(ProjectionList prod, int corte)
        {
            switch (corte)
            {
                case 0:

                    // Função do date 
                    //DATE_ADD(?1, interval  (7 - DAYOFWEEK(?1)) - 6 day))[WeekStart]


                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction("week",
                                NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                               )), "ano");


                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (1 - DAYOFWEEK(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (7 - DAYOFWEEK(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datafinal");
                    break;
                case 1:

                    // Função do date 
                    //DATE_ADD(?1, interval  (1 - dayofmonth(?1)) day)

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction("month",
                                NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                               )), "ano");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (1 - dayofmonth(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datainicial");


                    prod.Add(Projections.SqlFunction(
                                   new SQLFunctionTemplate(NHibernateUtil.Date,
                                                           "LAST_DAY(?1)"),
                                   NHibernateUtil.Date,
                                   Projections.Property("pr.DATAPrescricao")), "datafinal");

                    break;
                case 2:

                    // Função do date 
                    //MAKEDATE(YEAR(?1), 1) + INTERVAL QUARTER(?1) QUARTER - INTERVAL 1 QUARTER

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction(
                                new SQLFunctionTemplate(NHibernateUtil.Int32,
                                                     "CEILING(MONTH(?1) / 6)"),
                                NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32,
                               Projections.Property("pr.DATAPrescricao")
                               )), "ano");


                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1), 1) + INTERVAL ((CEILING(MONTH(?1) / 6) - 1) * 6) MONTH"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1), 1) + INTERVAL (CEILING(MONTH(?1) / 6) * 6) MONTH - INTERVAL 1 day"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datafinal");

                    break;
                case 3:

                    // Função do date 
                    //DATE_ADD(?1, interval  (1 - dayofyear(?1)) day))

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction("year",
                                NHibernateUtil.Int32, Projections.Property("pr.DATAPrescricao")
                                )), "corte");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1),1)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_FORMAT(this_.DataConsulta ,'%Y-12-31')"),
                                    NHibernateUtil.Date,
                                    Projections.Property("pr.DATAPrescricao")), "datafinal");
                    break;
            }

            return prod;

        }
    }
}
