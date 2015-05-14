using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{

    /// <summary>
    /// Clase pública que define un atributo que permite agregar un valor cadena
    /// </summary>
    public class ValorCadenaAttribute : Attribute
    {
        private string _valor;

        public ValorCadenaAttribute(string valor) { _valor = valor; }

        public string Value { get { return _valor; } }

    }
}
