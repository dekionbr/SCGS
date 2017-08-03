using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class EstoqueBusiness
    {

        public static Estoque Obter(int Id)
        {
            var Estoque = (
                from r in Session.Current.QueryOver<Estoque>()
                where r.Id == Id
                select r).SingleOrDefault();

            return Estoque;
        }

        public static Estoque Save(Estoque Estoque)
        {
            using (var scope = new TransactionScope())
            {
                Estoque = Session.Current.Merge<Estoque>(Estoque);
                if (Estoque.Id != 0)
                    Session.Current.Update(Estoque);
                else
                    Session.Current.Save(Estoque);
                scope.Complete();
            }
            return Estoque;
        }

        public static Estoque Deletar(Estoque Estoque)
        {
            using (var scope = new TransactionScope())
            {
                Estoque = Session.Current.Merge<Estoque>(Estoque);
                if (Estoque.Id != 0)
                    Session.Current.Delete(Estoque);
                scope.Complete();
            }
            return Estoque;
        }





        public static List<Estoque> ObterTodos()
        {
            var Estoques = (
                from r in Session.Current.CreateCriteria<Estoque>().List<Estoque>()
                select r).ToList();

            return Estoques;
        }
    }
}
