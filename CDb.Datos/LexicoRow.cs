using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Compilacion;
using CDb.Datos.DatosTableAdapters;

namespace CDb.Datos
{
    public partial class Datos
    {

        public static Dictionary<TiposSimbolos, string> ObtenerTablaSimbolos()
        {
            LexicoTableAdapter ta = new LexicoTableAdapter();
            Datos.LexicoDataTable res = ta.GetData();

            return res.ToDictionary(lx => lx.Tipo, lx => lx.Elemento);
        }

        public static List<Palabra> ObtenerPalabrasReservadas()
        {
            SintacticoTableAdapter ta = new SintacticoTableAdapter();
            Datos.SintacticoDataTable res = ta.GetData();

            return res.Select(p =>
                new Palabra(p.Palabra, p.Clase, p.Palabra1, p.Palabra2))
                .ToList();
        }

        public partial class LexicoRow
        {
            public TiposSimbolos Tipo
            {
                get { return (TiposSimbolos)tipo; }
                set { tipo = (short)value; }
            }

        }

        public partial class SintacticoRow
        {
            public ClasesPalabras Clase
            {
                get { return (ClasesPalabras)clase; }
                set { clase = (short)value; }
            }
        }
    }

}
