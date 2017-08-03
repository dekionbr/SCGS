using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;

namespace SCGS.CORE.Business
{
    internal static class Validador
    {

        public static void Validar(object obj)
        {
            var context = new ValidationContext(obj);
            var factory = new AttributedValidatorFactory();
            var validator = factory.GetValidator(obj.GetType());
            if (validator != null)
            {
                var results = validator.Validate(context);
                if (!results.IsValid)
                {
                    throw new MyValidationException(results.Errors, "Verifique os erros e tente novamente.");
                }
            }
        }
    }

    class MyValidationException : ValidationException
    {
        private string message;
        public MyValidationException(IList<ValidationFailure> errors, string message)
            : base(errors)
        {
            this.message = message;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }
    }
     
}
