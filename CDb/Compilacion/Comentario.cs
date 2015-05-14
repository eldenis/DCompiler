using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
   

    public class Comentario : ExpresionBase
    {
        internal Comentario(int posicion, string contenido) : base(posicion, contenido) { }
    }

  
}
