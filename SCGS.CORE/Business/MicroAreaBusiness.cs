using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class MicroAreaBusiness
    {

        public static List<MicroArea> ObterByEquipe(Equipe equipe)
        {
            var microareas = (
                from r in Session.Current.CreateCriteria<MicroArea>().List<MicroArea>()
                where r.Equipe.Id == equipe.Id
                select r).ToList();
            return microareas;
        }


        public static MicroArea Obter(int Id)
        {
            var microarea = (
                from m in Session.Current.QueryOver<MicroArea>()
                where m.Id == Id
                select m).SingleOrDefault();

            return microarea;
        }

        public static MicroArea Save(MicroArea microarea)
        {
            using (var scope = new TransactionScope())
            {
                microarea = Session.Current.Merge<MicroArea>(microarea);
                if (microarea.Id != 0)
                    Session.Current.Update(microarea);
                else
                    Session.Current.Save(microarea);
                scope.Complete();
               
            }
            return microarea;
        }
        public static List<MicroArea> ObterTodos()
        {
            var microareas = (
                from r in Session.Current.CreateCriteria<MicroArea>().List<MicroArea>()
                select r).ToList();

            return microareas;
        }      
    }
}
