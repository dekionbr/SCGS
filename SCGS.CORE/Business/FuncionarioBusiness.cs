using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SCGS.CORE.Business
{
    public class FuncionarioBusiness
    {


        public static Funcionario Obter(int Id)
        {
            var funcioanrio = (
                from r in Session.Current.CreateCriteria<Funcionario>().List<Funcionario>()
                where r.Id == Id
                select r).SingleOrDefault();

            return funcioanrio;
        }




        public static Funcionario Deletar(Funcionario Funcionario)
        {
            using (var scope = new TransactionScope())
            {
                Funcionario = Session.Current.Merge<Funcionario>(Funcionario);
                if (Funcionario.Id != 0)
                    Session.Current.Delete(Funcionario);
                scope.Complete();
            }
            return Funcionario;
        }


        public static Funcionario Save(Funcionario Funcionario)
        {
            using (var scope = new TransactionScope())
            {
                Funcionario = Session.Current.Merge<Funcionario>(Funcionario);

                if (Funcionario.Id != 0)
                {
                    if (Funcionario.Senha == null)
                        Funcionario.Senha = Obter(Funcionario.Id).Senha;
                    Session.Current.Update(Funcionario);
                }
                else
                    Session.Current.Save(Funcionario);

                scope.Complete();
            }
            return Funcionario;
        }

        public static List<Funcionario> ObterTodos()
        {
            var funcioanrios = (
                from r in Session.Current.CreateCriteria<Funcionario>().List<Funcionario>()
                select r).ToList();
            return funcioanrios;
        }

        public static Funcionario ObterByMatricula(string matricula)
        {
            int Id = int.Parse(matricula.Substring(5));

            var funcioanrio = (
                 from r in Session.Current.CreateCriteria<Funcionario>().List<Funcionario>()
                 where r.Id == Id
                 select r).SingleOrDefault();

            return funcioanrio;
        }

        public static bool Autenticar(string matricula, string senha)
        {
            int Id = int.Parse(matricula.Substring(5));
            var funcioanrio = (
                 from r in Session.Current.CreateCriteria<Funcionario>().List<Funcionario>()
                 where (r.Id == Id &&
                        r.Senha == senha &&
                        r.Equipe != null) ||
                       (r.Id == Id &&
                        r.Senha == senha &&
                        r.TipoFuncionario.Equals(TipoFuncionario.GerenteGeral))
                 select r).SingleOrDefault();

            return funcioanrio != null;
        }

        public static IList<Funcionario> ObterPorEquipe(int idEquipe)
        {
            var funcionarios = Session.Current.QueryOver<Funcionario>()
                            .Where(x => x.Equipe.Id == idEquipe).List<Funcionario>();
                            
            return funcionarios;
        }
    }
}
