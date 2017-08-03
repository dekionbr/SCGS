using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using SCGS.CORE.Entity;
using SCGS.CORE.Business;

namespace SCGS.CORE.Security
{
    public class RoleManager : RoleProvider
    {
        public const string GERENTE_GERAL = "GerenteGeral";
        public const string GERENTE = "Gerente";
        public const string MEDICO = "MEdico";
        public const string EMFERMEIRO = "Enfermeiro";
        public const string ENFERMEIRO_TECNICO = "EnfermeiroTecnico";
        public const string AGENTE = "Agente";
        public const string FARMACEUTICO = "Farmaceutico";
        public const string FUNCIONARIO = "Funcionario";
        public const string ADMIN = "Admin";

        public override void AddUsersToRoles(string[] matriculas, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string matricula)
        {
            var context = HttpContext.Current;
            Funcionario usu = null;
            if (context != null)
                usu = context.Cache.Get(matricula) as Funcionario;

            if (matricula.Equals("99999")) {
                usu = new Funcionario() { Nome = "Admin", TipoFuncionario = TipoFuncionario.Admin };
                context.Cache.Add(matricula, usu, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.Default, null);
            }
                

            if (usu == null)
            {
                usu = FuncionarioBusiness.ObterByMatricula(matricula);
                if (usu == null)
                    throw new InvalidOperationException();
                else
                    context.Cache.Add(matricula, usu, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.Default, null);
            }

            //#if DEBUG
            //            return new string[]{
            //                Perfil.Administrador.ToString(), 
            //                Perfil.Fornecedor.ToString(),
            //                Perfil.Distribuidor.ToString(),
            //                Perfil.Consultor.ToString()
            //            };

            //#else
            switch (usu.TipoFuncionario)
            {
                case TipoFuncionario.GerenteGeral:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.GerenteGeral.ToString(), TipoFuncionario.Gerente.ToString() };
                case TipoFuncionario.Gerente:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Gerente.ToString() };
                case TipoFuncionario.Medico:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Medico.ToString() };
                case TipoFuncionario.Enfermeiro:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Enfermeiro.ToString(), TipoFuncionario.Medico.ToString() };
                case TipoFuncionario.EnfermeiroTecnico:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.EnfermeiroTecnico.ToString() };
                case TipoFuncionario.Agente:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Agente.ToString() };
                case TipoFuncionario.Farmaceutico:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Farmaceutico.ToString() };
                case TipoFuncionario.Admin:
                    return new string[] { TipoFuncionario.Funcionario.ToString(), TipoFuncionario.Farmaceutico.ToString(),
                                          TipoFuncionario.Agente.ToString(), TipoFuncionario.Agente.ToString(),
                                          TipoFuncionario.EnfermeiroTecnico.ToString(), TipoFuncionario.Enfermeiro.ToString(), TipoFuncionario.Medico.ToString(),
                                          TipoFuncionario.Medico.ToString(), TipoFuncionario.GerenteGeral.ToString(), TipoFuncionario.Gerente.ToString()
                                           };
                default:
                    throw new NotImplementedException();
            }
            //#endif
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string matricula, string roleName)
        {
            return GetRolesForUser(matricula).Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] matricula, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string matricula)
        {
            throw new NotImplementedException();
        }
    }
}
