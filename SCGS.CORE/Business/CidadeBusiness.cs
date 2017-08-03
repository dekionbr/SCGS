using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Business
{
    public class CidadeBusiness
    {

        public static List<Cidade> ObterTodos()
        {
            var cidades = (
                from r in Session.Current.CreateCriteria<Cidade>().List<Cidade>()
                select r).ToList();

            return cidades;
        }



        public static Cidade Obter(int Id)
        {
            var cidade = (
                from r in Session.Current.QueryOver<Cidade>()
                where r.Id == Id
                select r).SingleOrDefault();

            return cidade;
        }



        public static List<Cidade> ObterCidadeEstado(Estado estado)
        {
            var cidades = (
                from c in Session.Current.CreateCriteria<Cidade>().List<Cidade>()
                where c.Estado == estado
                select c).ToList();
            return cidades;
        }
    }
}
