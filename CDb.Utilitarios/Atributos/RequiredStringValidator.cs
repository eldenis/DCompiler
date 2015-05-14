using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;


namespace CDb.Transversal.Utilitarios
{
    public class RequiredStringValidatorAttribute : ValueValidatorAttribute
    {
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new RequiredStringValidator(this.MessageTemplate, this.Negated);
        }
    }

    public class RequiredStringValidator : ValueValidator<string>
    {
        public RequiredStringValidator(string messageTemplate, bool negated)
            : base(messageTemplate, null, negated)
        {
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (string.IsNullOrWhiteSpace(objectToValidate) != Negated)
            {
                LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
            }
        }

        protected override string DefaultNegatedMessageTemplate
        {
            get
            {
                return "Field cannot have a value.";
            }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get
            {
                return "Field is required.";
            }
        }
    } 
}
