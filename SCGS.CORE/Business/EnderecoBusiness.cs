using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class EnderecoBusiness
    {


        public static Endereco Obter(int Id)
        {
            var endereco = (
                from r in Session.Current.CreateCriteria<Endereco>().List<Endereco>()
                where r.Id == Id
                select r).SingleOrDefault();

            return endereco;
        }



        public static List<Endereco> ObterByMicroArea(MicroArea microarea)
        {
            return (
                from r in Session.Current.CreateCriteria<Endereco>().List<Endereco>()
                where r.MicroArea != null
                select r).Where(a => a.MicroArea.Id == microarea.Id).ToList();
                
        }



        public static Endereco Save(Endereco endereco)
        {
            using (var scope = new TransactionScope())
            {
                endereco = Session.Current.Merge<Endereco>(endereco);
                Session.Current.SaveOrUpdate(endereco);
                scope.Complete();
            }
            return endereco;
        }

        public static List<Endereco> ObterTodos()
        {
            var enderecos = (
                from r in Session.Current.CreateCriteria<Endereco>().List<Endereco>()
                select r).ToList();

            return enderecos;
        }        
    }
}
