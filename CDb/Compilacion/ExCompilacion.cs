using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class ExCompilacion : Exception
    {
        internal ExCompilacion(string mensaje) : base(mensaje) { }

        internal ExCompilacion(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
