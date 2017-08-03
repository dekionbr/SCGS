using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NHibernate.Linq;
using SCGS.CORE.Entity;
using SCGS.CORE;
using NHibernate.Criterion;

namespace SCGS.CORE.Business
{
    public class PatogenoBusiness
    {
        public static Patogeno Obter(int Id)
        {
            var Patogeno = (
                from r in Session.Current.QueryOver<Patogeno>()
                where r.Id == Id
                select r).SingleOrDefault();

            return Patogeno;
        }

        public static Patogeno Save(Patogeno Patogeno)
        {
            using (var scope = new TransactionScope())
            {
                Patogeno = Session.Current.Merge<Patogeno>(Patogeno);
                if (Patogeno.Id != 0) 
                    Session.Current.Update(Patogeno);
                else
                    Session.Current.Save(Patogeno);
                scope.Complete();
            }
            return Patogeno;
        }

        public static Patogeno Deletar(Patogeno Patogeno)
        {
            using (var scope = new TransactionScope())
            {
                Patogeno = Session.Current.Merge<Patogeno>(Patogeno);
                if (Patogeno.Id != 0)
                    Session.Current.Delete(Patogeno);
                scope.Complete();
            }
            return Patogeno;
        }





        public static List<Patogeno> ObterByParametro(string campo, string valor)
        {
            var Patogenos = (
                from r in Session.Current.CreateCriteria<Patogeno>()
                            .Add(Restrictions.Like(campo, valor, MatchMode.Anywhere)).List<Patogeno>()
                select r ).ToList();

            return Patogenos;
        }


        public static List<Patogeno> ObterTodos()
        {
            var Patogenos = (
                from r in Session.Current.CreateCriteria<Patogeno>().List<Patogeno>()
                select r).ToList();

            return Patogenos;
        }

    }




    
}


