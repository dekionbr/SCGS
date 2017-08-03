using NHibernate.Criterion;
using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class ProntuarioBusiness
    {


        public static Prontuario Obter(int Id)
        {
            var Prontuario = (
                from r in Session.Current.QueryOver<Prontuario>()
                where r.Id == Id
                select r).SingleOrDefault();

            return Prontuario;
        }

        public static Prontuario Save(Prontuario Prontuario)
        {
            using (var scope = new TransactionScope())
            {
                Prontuario = Session.Current.Merge<Prontuario>(Prontuario);
                if (Prontuario.Id != 0)
                    Session.Current.Update(Prontuario);
                else
                    Session.Current.Save(Prontuario);
                scope.Complete();
            }
            return Prontuario;
        }

        public static Prontuario Deletar(Prontuario Prontuario)
        {
            using (var scope = new TransactionScope())
            {
                Prontuario = Session.Current.Merge<Prontuario>(Prontuario);
                if (Prontuario.Id != 0)
                    Session.Current.Delete(Prontuario);
                scope.Complete();
            }
            return Prontuario;
        }

        public static List<Prontuario> ObterByParametroUsuario(string campo, string valor)
        {
            var Prontuarios = (
                 from p in Session.Current.CreateCriteria<Prontuario>().Future<Prontuario>()
                 join u in Session.Current.CreateCriteria<Usuario>()
                 .Add(Restrictions.Like(campo, valor, MatchMode.Anywhere)).Future<Usuario>() 
                    on p.Usuario.Id equals u.Id
                 select p).ToList();

            return Prontuarios;

        }
        
        public static List<Prontuario> ObterTodos()
        {
            var Prontuarios = (
                from r in Session.Current.CreateCriteria<Prontuario>().List<Prontuario>()
                select r).ToList();

            return Prontuarios;
        }
        
    }
}
