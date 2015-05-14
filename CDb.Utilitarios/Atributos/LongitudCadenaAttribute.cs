using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CDb.Transversal.Utilitarios
{
    public class LongitudCadenaValidatorAttribute : ValueValidatorAttribute
    {
        int UpperBound { get; set; }
        int LowerBound { get; set; }
        RangeBoundaryType LowerBoundType { get; set; }
        RangeBoundaryType UpperBoundType { get; set; }

        public LongitudCadenaValidatorAttribute(int upperBound)
            : this(0, RangeBoundaryType.Ignore, upperBound, RangeBoundaryType.Inclusive) { }


        public LongitudCadenaValidatorAttribute(int lowerBound, int upperBound)
            : this(upperBound, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive) { }


        public LongitudCadenaValidatorAttribute(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType)
        {
            UpperBound = upperBound;
            LowerBoundType = lowerBoundType;
            UpperBound = upperBound;
            UpperBoundType = upperBoundType;
        }


        protected override Validator DoCreateValidator(Type targetType)
        {
            return new LongitudCadenaValidator(LowerBound, LowerBoundType, UpperBound, UpperBoundType, this.MessageTemplate, this.Negated);
        }
    }

    public class LongitudCadenaValidator : ValueValidator<string>
    {
        int UpperBound { get; set; }
        int LowerBound { get; set; }
        RangeBoundaryType LowerBoundType { get; set; }
        RangeBoundaryType UpperBoundType { get; set; }


        public LongitudCadenaValidator(
            int lowerBound,
            RangeBoundaryType lowerBoundType,
            int upperBound,
            RangeBoundaryType upperBoundType,
            string messageTemplate,
            bool negated)
            : base(messageTemplate, null, negated)
        {
            UpperBound = upperBound;
            LowerBoundType = lowerBoundType;
            UpperBound = upperBound;
            UpperBoundType = upperBoundType;
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            var huboError = false;

            if (objectToValidate is string)
            {
                var cadena = objectToValidate as string;

                if (LowerBoundType != RangeBoundaryType.Ignore)
                {
                    if (LowerBoundType == RangeBoundaryType.Inclusive)
                        huboError = huboError || (Negated ? !(cadena.Length <= LowerBound) : cadena.Length <= LowerBound);
                    else
                        huboError = huboError || (Negated ? !(cadena.Length < LowerBound) : cadena.Length < LowerBound);
                }

                if (UpperBoundType != RangeBoundaryType.Ignore)
                {
                    if (UpperBoundType == RangeBoundaryType.Inclusive)
                        huboError = huboError || (Negated ? !(cadena.Length >= UpperBound) : cadena.Length >= UpperBound);
                    else
                        huboError = huboError || (Negated ? !(cadena.Length > UpperBound) : cadena.Length > UpperBound);
                }
            }

            if (huboError)
                LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
        }

        protected override string DefaultNegatedMessageTemplate
        {
            get { return "La longitud no es válida"; }
        }

        protected override string DefaultNonNegatedMessageTemplate
        {
            get { return "La longitud máxima del campo ha sido alcanzada"; }
        }
    }
}
