using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class ContatoBusiness
    {

        public static Contato Obter(int Id)
        {
            var Contato = (
                from r in Session.Current.CreateCriteria<Contato>().List<Contato>()
                where r.Id == Id
                select r).SingleOrDefault();

            return Contato;
        }



        public static List<Contato> ObterByPessoa(Pessoa pessoa)
        {
            
            return (
                from r in Session.Current.CreateCriteria<Contato>().List<Contato>()
                where r.Pessoa != null
                      select r).Where(a => a.Pessoa.Id == pessoa.Id).ToList();

        }



        public static Contato Save(Contato Contato)
        {
            using (var scope = new TransactionScope())
            {
                Contato = Session.Current.Merge<Contato>(Contato);
                Session.Current.SaveOrUpdate(Contato);
                scope.Complete();
            }
            return Contato;
        }







        public static Contato Deletar(Contato Contato)
        {
            using (var scope = new TransactionScope())
            {
                Contato = Session.Current.Merge<Contato>(Contato);
                if (Contato.Id != 0)
                    Session.Current.Delete(Contato);
                scope.Complete();
            }
            return Contato;
        }





        public static List<Contato> ObterTodos()
        {
            var Contatos = (
                from r in Session.Current.CreateCriteria<Contato>().List<Contato>()
                select r).ToList();

            return Contatos;
        }
    }
}
