using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Exceptions;
using System.Data.Common;
using FluentValidation;
using FluentValidation.Results;
using SCGS.CORE.Business;

namespace SCGS.CORE
{
    class SqlExceptionConverter : ISQLExceptionConverter
    {
        private static Dictionary<string, string> exceptions = new Dictionary<string, string>();

        public SqlExceptionConverter()
        {
            exceptions.Clear();
            //exceptions.Add("IX_Email", "Já existe um cadastro com esse email. Você esqueceu sua senha?");
            //exceptions.Add("IX_CpfCnpj", "O documento informado já está em uso.");
        }

        public Exception Convert(AdoExceptionContextInfo exInfo)
        {
            var sqlEx = ADOExceptionHelper.ExtractDbException
                (exInfo.SqlException) as DbException;
            if (sqlEx != null)
            {
                //TODO: i18n
                foreach (var key in exceptions.Keys)
                {
                    if (sqlEx.Message.ToUpper().Contains(key.ToUpper()))
                        return new MyValidationException(new ValidationFailure[] { 
                            new ValidationFailure(key.Substring(3), exceptions[key])
                        }, exceptions[key]);
                }
            }
            return SQLStateConverter.HandledNonSpecificException
                (exInfo.SqlException, exInfo.Message, exInfo.Sql);
        }
    }
}
