using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CDb.Transversal.Utilitarios
{
    /// <summary>
    /// Clase pública que define un atributo que permite agregar un valor Type
    /// </summary>
    public class ValorTipoAttribute : Attribute
    {
        private Type _valor;

        public ValorTipoAttribute(Type valor) { _valor = valor; }

        public Type Value { get { return _valor; } }


        /// <summary>
        /// Gets a string value for a particular enum value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>String Value associated via a <see cref="ValorCadena"/> attribute, or null if not found.</returns>
        public static Type ObtenerValorTipo(Enum value)
        {
            Type output = null;
            Type type = value.GetType();

            //Look for our 'ValorTipo' in the field's custom attributes
            FieldInfo fi = type.GetField(value.ToString());
            ValorTipoAttribute[] attrs = fi.GetCustomAttributes(typeof(ValorTipoAttribute), false) as ValorTipoAttribute[];
            
            if (attrs.Length > 0)
                output = attrs[0].Value;

            return output;

        }
    }
}
