using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Tool.hbm2ddl;
using SCGS.CORE.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE
{
    public static class Session
    {
        private static ISessionFactory currentFactory;
        private static ISessionFactory CurrentFactory
        {
            get
            {
                if (currentFactory == null || currentFactory.IsClosed)
                {
                    CreateFactoryAndMap();
                }
                return currentFactory;
            }
        }

        public static ISession OpenSession()
        {
            return CurrentFactory.OpenSession();
        }

        private static void CreateFactoryAndMap()
        {
            var connStrKey = "cnn";

            currentFactory = Fluently.Configure()
                .Database(
                    //MsSqlConfiguration.MsSql2008.ConnectionString(
                    //    c => c.FromConnectionStringWithKey(connStrKey)
                    //    )
                    
                    MySQLConfiguration.Standard.Dialect<MySQL5Dialect>().ConnectionString(
                                    c => c.FromConnectionStringWithKey(connStrKey)
                                        )
                 )
                .Mappings(m =>
                {
                    m.FluentMappings
                        .AddFromAssemblyOf<UsuarioMap>()
                        .Conventions.AddFromAssemblyOf<UsuarioMap>();
                        

                })
                .ExposeConfiguration(c =>
                {
                    c.SetProperty("hbm2ddl.keywords", "none");
                    c.SetProperty("current_session_context_class", "thread_static");
                    c.SetProperty("show_sql", "true");
                    c.SetProperty("format_sql", "true");
                    c.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, 
                                  TimeSpan.FromMinutes(2).TotalSeconds.ToString());
                    c.AddSqlFunction("week", new StandardSafeSQLFunction("week", NHibernateUtil.Int32, 1));
                    c.AddSqlFunction("week", new StandardSafeSQLFunction("week", NHibernateUtil.Int32, 1));
                    c.AddSqlFunction("quarter", new StandardSafeSQLFunction("quarter", NHibernateUtil.Int32, 1));
                    c.AddSqlFunction("dateadd", new StandardSafeSQLFunction("dateadd", NHibernateUtil.DateTime, 3));
                    c.AddSqlFunction("datepart", new StandardSafeSQLFunction("datepart", NHibernateUtil.Int32, 2));
                }).ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true)).BuildSessionFactory();
        }

        internal static ISession Current
        {
            get
            {
                if (!CurrentSessionContext.HasBind(CurrentFactory) )
                {
                    CurrentSessionContext.Bind(CurrentFactory.OpenSession());
                }
               
                return CurrentFactory.GetCurrentSession();

            }
        }

        public static void DisposeSession()
        {
            var session = CurrentSessionContext.Unbind(CurrentFactory);
            if (session != null)
            {
                if (session.IsConnected && session.Connection != null)
                    session.Connection.Close();
                if (session.IsOpen)
                    session.Close();
                session.Dispose();
            }
        }
    }
}
