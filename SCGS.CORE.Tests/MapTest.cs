using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCGS.CORE.Entity;
using SCGS.CORE.Business;
using NHibernate;
using SCGS.CORE.Entity.SQL;
using System.Collections;

namespace SCGS.CORE.Tests
{
    [TestClass]
    public class MapTest
    {





        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }



        [TestMethod]
        public void MapTest1()
        {

            //var resultado = ConsultaBusiness.ConsultaSQL(

            //      " select p.nome, COUNT(*) AS total, c.DataConsulta AS dtinicial, DATE_ADD(c.DataConsulta, INTERVAL MAX(DAYOFWEEK(c.DataConsulta)) DAY) AS dtfinal"

            //      + " FROM consulta c"

            //      + " INNER JOIN pessoa p ON p.Id = c.medico"

            //      + " WHERE DataConsulta BETWEEN '" + DateTime.Parse("01/11/2015").ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse("30/11/2015").ToString("yyyy-MM-dd") + "'"

            //      + " GROUP BY p.nome, WEEK(c.DataConsulta)"

            //      + " ORDER BY c.DataConsulta ASC"

            //      ).AddScalar("nome", NHibernateUtil.String)
            //      .AddScalar("total", NHibernateUtil.Int32)
            //      .AddScalar("dtinicial", NHibernateUtil.DateTime)
            //      .AddScalar("dtfinal", NHibernateUtil.DateTime)
            //      .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean(typeof(RelConsultas)))
            //      .List<RelConsultas>();

            //foreach (RelConsultas r in resultado)
            //{
            //    Console.WriteLine("Medico:" + r.nome + " " + r.periodo + " Total:" + r.total);
            //}


            /*
            Usuario Usuario = new Usuario()
            {
                Nome = "Fulano",
                DataNascimento = DateTime.Now.AddYears(-19),
                RG = "123456789",
                CPF = "12123333358",
                DataCadastro = DateTime.Now,
                Senha = "ASDFWASDFASDFASDFAS"
            };

            Usuario = UsuarioBusiness.Save(Usuario);

            var user = UsuarioBusiness.Obter(Usuario.Id);

            Assert.IsNotNull(user);




            Funcionario f = new Funcionario()
            {

                Nome = "Fulano Funcionario",
                DataNascimento = DateTime.Now.AddYears(-45),
                RG = "9999999999",
                CPF = "0000000000",
                DataCadastro = DateTime.Now,
                Senha = "Senha",
                TipoFuncionario = TipoFuncionario.Enfermeiro
            };

            f = FuncionarioBusiness.Save(f);
            var func = FuncionarioBusiness.Obter(f.Id);

            Assert.IsNotNull(f);
            */

        }



        [TestMethod]
        public void listaRelTest()
        {
            IList<RelConsultas> rel = ConsultaBusiness.GerarRelatorio(1, null, null);
            Assert.IsNotNull(rel);
        }

        [TestMethod]
        public void listaFuncionarioTest()
        {
            IList<Funcionario> fuc = FuncionarioBusiness.ObterPorEquipe(4);
            Assert.IsNotNull(fuc);
        }

        [TestMethod]
        public void listaRelPatogenoTest()
        {
            IList<RelConsultas> rel = PatogenoProntuarioBusiness.GerarRelatorio(1, null, null);
            Assert.IsNotNull(rel);
        }
    }
}
