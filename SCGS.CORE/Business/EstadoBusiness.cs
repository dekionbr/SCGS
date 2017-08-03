using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Business
{
    public class EstadoBusiness
    {

        public static List<Estado> ObterTodos()
        {
            var estados = (
                from r in Session.Current.CreateCriteria<Estado>().List<Estado>()
                select r).ToList();

            return estados;
        }


        public static Estado Obter(int Id)
        {
            var estado = (
                from r in Session.Current.QueryOver<Estado>()
                where r.Id == Id
                select r).SingleOrDefault();

            return estado;
        }
    }
}
