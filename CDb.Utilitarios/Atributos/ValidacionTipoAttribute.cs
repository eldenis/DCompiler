using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace CDb.Transversal.Utilitarios
{
    public class ValidacionTipoAttribute : ValidationAttribute
    {
        public ValidacionTipoAttribute(Type tipo)
        {
            Tipo = tipo;

            var metodoParse = Tipo.GetMethod("Parse");
            if (metodoParse != null)
            {
                _metodoParse = (obj) =>
                {
                    try
                    {
                        var valor = metodoParse.Invoke(null, new object[] { obj });
                        return valor;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                };
            }
        }

        private Func<object, object> _metodoParse = (obj) => { return obj; };

        private Type _tipo;
        public Type Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public override bool IsValid(object value)
        {
            if (value.GetType() != Tipo)
            {
                if (value.GetType() == typeof(string))
                {
                    var resParse = _metodoParse(value);
                    if (resParse != null && resParse.GetType() == Tipo)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
        /*
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            return IsValid(value) ? new ValidationResult(string.Empty)
                : new ValidationResult("MALO TODO");


            //return base.IsValid(value, validationContext);
        }*/
    }
}
