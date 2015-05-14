using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CDb.Transversal.Utilitarios
{
    public class ValorEnumValidatorAttribute : ValueValidatorAttribute
    {
        public Type TipoEnum { get; private set; }

        public ValorEnumValidatorAttribute(Type tipoEnum)
        {
            TipoEnum = tipoEnum;
        }

        protected override Validator DoCreateValidator(Type targetType)
        {
            return new ValorEnumValidator(TipoEnum, MessageTemplate, Negated);
        }
    }

    public class ValorEnumValidator : ValueValidator<Enum>
    {
        public Type TipoEnum { get; private set; }


        public ValorEnumValidator(Type tipoEnum,
            string messageTemplate,
            bool negated)
            : base(messageTemplate, null, negated)
        {
            TipoEnum = tipoEnum;
        }

        protected override void DoValidate(Enum objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {


            if (Negated ? Enum.IsDefined(TipoEnum, objectToValidate) : !Enum.IsDefined(TipoEnum, objectToValidate))
                LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
        }

        protected override string DefaultNegatedMessageTemplate
        {
            get { return "El valor está dentro del Enum"; }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get { return "El valor no está dentro del enum"; }
        }
    }
}
