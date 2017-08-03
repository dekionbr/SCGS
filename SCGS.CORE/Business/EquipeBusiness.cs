using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class EquipeBusiness
    {



        public static Equipe Obter(int Id)
        {
            var equipe = (
                from r in Session.Current.CreateCriteria<Equipe>().List<Equipe>()
                where r.Id == Id
                select r).SingleOrDefault();

            return equipe;
        }




        public static Equipe Deletar(Equipe Equipe)
        {
            using (var scope = new TransactionScope())
            {
                Equipe = Session.Current.Merge<Equipe>(Equipe);
                if (Equipe.Id != 0)
                    Session.Current.Delete(Equipe);
                scope.Complete();
            }
            return Equipe;
        }

        public static IList<Equipe> ObterPorUnidade(int idUnidade)
        {
            List<Equipe> equipes = (
                from r in Session.Current.CreateCriteria<Equipe>().List<Equipe>()
                where r.Unidade.Id == idUnidade
                select r).ToList();

            return equipes;
        }

        public static Equipe Save(Equipe Equipe)
        {
            using (var scope = new TransactionScope())
            {
                Equipe = Session.Current.Merge<Equipe>(Equipe);

                if (Equipe.Id != 0)
                    Session.Current.Update(Equipe);
                else
                    Session.Current.Save(Equipe);

                scope.Complete();
            }
            return Equipe;
        }

        public static List<Equipe> ObterTodos()
        {
            var equipes = (
                from r in Session.Current.CreateCriteria<Equipe>().List<Equipe>()
                select r).ToList();

            return equipes;
        }
    }
}
