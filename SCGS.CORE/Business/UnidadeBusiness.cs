using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class UnidadeBusiness
    {


        public static Unidade Obter(int Id)
        {
            var unidade = (
                from u in Session.Current.QueryOver<Unidade>()
                where u.Id == Id
                select u).SingleOrDefault();

            return unidade;
        }

        public static IList<Unidade> ObterTodas()
        {
            return Session.Current.QueryOver<Unidade>().List();
        }

        public static Unidade Save(Unidade unidade)
        {
            using (var scope = new TransactionScope())
            {
                unidade = Session.Current.Merge<Unidade>(unidade);
                Session.Current.SaveOrUpdate(unidade);
                scope.Complete();
            }
            return unidade;
        }



        public static List<Unidade> ObterTodos()
        {
            var unidade = (
                from u in Session.Current.QueryOver<Unidade>().List<Unidade>()
                select u).ToList();

            return unidade;
        }


    }
}
