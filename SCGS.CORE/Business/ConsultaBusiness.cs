using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using SCGS.CORE.Entity;
using SCGS.CORE.Entity.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class ConsultaBusiness
    {


        public static Consulta Obter(int Id)
        {
            var funcioanrio = (
                from r in Session.Current.CreateCriteria<Consulta>().List<Consulta>()
                where r.Id == Id
                select r).SingleOrDefault();

            return funcioanrio;
        }




        public static Consulta Deletar(Consulta Consulta)
        {
            using (var scope = new TransactionScope())
            {
                Consulta = Session.Current.Merge<Consulta>(Consulta);
                if (Consulta.Id != 0)
                    Session.Current.Delete(Consulta);
                scope.Complete();
            }
            return Consulta;
        }


        public static Consulta Save(Consulta Consulta)
        {
            using (var scope = new TransactionScope())
            {
                Consulta = Session.Current.Merge<Consulta>(Consulta);

                if (Consulta.Id != 0)
                    Session.Current.Update(Consulta);
                else
                    Session.Current.Save(Consulta);

                scope.Complete();
            }
            return Consulta;
        }

        public static List<Consulta> ObterTodos()
        {
            var consultas = (
                from r in Session.Current.CreateCriteria<Consulta>().List<Consulta>()
                select r).ToList();
            return consultas;
        }



        public static List<Consulta> ObterTodosSemCanceladas()
        {
            var consultas = (
                from r in Session.Current.CreateCriteria<Consulta>().List<Consulta>()
                where r.cancelada == false
                select r).OrderByDescending(a => a.DataConsulta).ToList();
            return consultas;
        }


        public static List<Consulta> ObterByPeriodo(DateTime de, DateTime ate)
        {
            var consultas = (
                from r in Session.Current.CreateCriteria<Consulta>().List<Consulta>()
                where r.DataConsulta.CompareTo(de) >= 0 && r.DataConsulta.CompareTo(ate) <= 0
                select r).OrderByDescending(a => a.DataConsulta).ToList();
            return consultas;
        }



        private static ISQLQuery ConsultaSQL(string query)
        {
            return Session.Current.CreateSQLQuery(query);

        }

        /// <summary>
        /// Metodo que gera relatório de todos as consultas do sistema separando por funcionário e margem de corte.
        /// </summary>
        /// <param name="corte"></param>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <param name="IdFuncionario"></param>
        /// <returns></returns>
        public static IList<RelConsultas> GerarRelatorio(int corte, string inicio, string fim, int? IdFuncionario = null)
        {
            ICriteria ct = Session.Current.CreateCriteria<Consulta>("c")
                                          .CreateCriteria("c.medico", "p", NHibernate.SqlCommand.JoinType.InnerJoin);

            ProjectionList prod = Projections.ProjectionList()
                 .Add(Projections.RowCount(), "value")
                 .Add(Projections.GroupProperty(Projections.Property("p.Nome")), "label");


            //DATEADD(dd, -(DATEPART(dw, WeddingDate)-1), WeddingDate)

            if (!String.IsNullOrEmpty(inicio) &&
                !String.IsNullOrEmpty(fim))
            {
                DateTime _inicio = DateTime.Parse(inicio);
                DateTime _fim = DateTime.Parse(fim);
                ct.Add(Restrictions.Between("c.DataConsulta", _inicio, _fim));
            }

            if (IdFuncionario != null)
            {
                ct.Add(Restrictions.Eq(Projections.Property("p.Id"), IdFuncionario.Value));
            }

            //ct.Add(Restrictions.Eq(Projections.Property("c.Confirmado"), true));

            return ct.SetProjection(ObterCorteConsulta(prod, corte))
                     .AddOrder(Order.Asc(Projections.Property("c.DataConsulta")))
                     .SetResultTransformer(
                        NHibernate.Transform.Transformers
                                            .AliasToBean(typeof(RelConsultas))
                                           ).List<RelConsultas>();
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
                                NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                               )), "ano");


                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (1 - DAYOFWEEK(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (7 - DAYOFWEEK(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datafinal");
                    break;
                case 1:

                    // Função do date 
                    //DATE_ADD(?1, interval  (1 - dayofmonth(?1)) day)

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction("month",
                                NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                               )), "ano");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_ADD(?1, interval  (1 - dayofmonth(?1)) day)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datainicial");


                    prod.Add(Projections.SqlFunction(
                                   new SQLFunctionTemplate(NHibernateUtil.Date,
                                                           "LAST_DAY(?1)"),
                                   NHibernateUtil.Date,
                                   Projections.Property("c.DataConsulta")), "datafinal");

                    break;
                case 2:

                    // Função do date 
                    //MAKEDATE(YEAR(?1), 1) + INTERVAL QUARTER(?1) QUARTER - INTERVAL 1 QUARTER

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction(
                                new SQLFunctionTemplate(NHibernateUtil.Int32,
                                                     "CEILING(MONTH(?1) / 6)"),
                                NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                                )), "corte");

                    prod.Add(Projections.GroupProperty(
                           Projections.SqlFunction("year",
                               NHibernateUtil.Int32,
                               Projections.Property("c.DataConsulta")
                               )), "ano");


                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1), 1) + INTERVAL ((CEILING(MONTH(?1) / 6) - 1) * 6) MONTH"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1), 1) + INTERVAL (CEILING(MONTH(?1) / 6) * 6) MONTH - INTERVAL 1 day"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datafinal");

                    break;
                case 3:

                    // Função do date 
                    //DATE_ADD(?1, interval  (1 - dayofyear(?1)) day))

                    prod.Add(Projections.GroupProperty(
                            Projections.SqlFunction("year",
                                NHibernateUtil.Int32, Projections.Property("c.DataConsulta")
                                )), "corte");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "MAKEDATE(YEAR(?1),1)"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datainicial");

                    prod.Add(Projections.SqlFunction(
                                    new SQLFunctionTemplate(NHibernateUtil.Date,
                                                            "DATE_FORMAT(this_.DataConsulta ,'%Y-12-31')"),
                                    NHibernateUtil.Date,
                                    Projections.Property("c.DataConsulta")), "datafinal");
                    break;
            }

            return prod;

        }


        public static IList<RelConsultas> queryConsulta(string dtini, string dtfim)
        {
            return ConsultaBusiness.ConsultaSQL(

                              " SELECT count(*) as value, c.DataConsulta as data"
                            + " FROM consulta c INNER JOIN pessoa p ON p.Id = c.medico"
                            + " WHERE c.DataConsulta BETWEEN '" + DateTime.Parse(dtini).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(dtfim).ToString("yyyy-MM-dd") + "'"
                            + " GROUP BY c.DataConsulta"
                            + " ORDER BY c.DataConsulta ASC"
                                  ).AddScalar("value", NHibernateUtil.Int32)
                              .AddScalar("data", NHibernateUtil.DateTime)
                              .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean(typeof(RelConsultas)))
                              .List<RelConsultas>();
        }


    }
}
