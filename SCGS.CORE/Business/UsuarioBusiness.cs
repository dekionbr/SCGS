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
    public class UsuarioBusiness
    {
        public static Usuario Obter(int Id)
        {
            var usuario = (
                from r in Session.Current.QueryOver<Usuario>()
                where r.Id == Id
                select r).SingleOrDefault();

            return usuario;
        }

        public static Usuario Save(Usuario Usuario)
        {
            using (var scope = new TransactionScope())
            {
                Usuario = Session.Current.Merge<Usuario>(Usuario);
                if (Usuario.Id != 0) 
                    Session.Current.Update(Usuario);
                else
                    Session.Current.Save(Usuario);
                scope.Complete();
            }
            return Usuario;
        }

        public static Usuario Deletar(Usuario Usuario)
        {
            using (var scope = new TransactionScope())
            {
                Usuario = Session.Current.Merge<Usuario>(Usuario);
                if (Usuario.Id != 0)
                    Session.Current.Delete(Usuario);
                scope.Complete();
            }
            return Usuario;
        }





        public static List<Usuario> ObterByParametro(string campo, string valor)
        {
            var usuarios = (
                from r in Session.Current.CreateCriteria<Usuario>()
                            .Add(Restrictions.Like(campo, valor, MatchMode.Anywhere)).List<Usuario>()
                select r ).ToList();

            return usuarios;
        }


        public static List<Usuario> ObterTodos()
        {
            var usuarios = (
                from r in Session.Current.CreateCriteria<Usuario>().List<Usuario>()
                select r).ToList();

            return usuarios;
        }        
    }




    
}


