using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class ExpresionBase
    {
        internal ExpresionBase(int posicion, string contenido)
            : this(contenido, contenido)
        {
            Posicion = posicion;
        }

        internal ExpresionBase(string contenido) : this(contenido, contenido) { }

        internal ExpresionBase(string contenido, string contenidoPresentacion)
        {
            Contenido = contenido;
            ContenidoPresentacion = contenidoPresentacion;
        }

        public int Posicion { get; protected set; }

        public string Contenido { get; protected set; }
        public string ContenidoPresentacion { get; protected set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Contenido) ?
                base.ToString() :
                string.Format("|   {0}   |", Contenido);
        }
    }
}
