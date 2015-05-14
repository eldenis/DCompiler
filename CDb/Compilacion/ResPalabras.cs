using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class ResPalabras
    {
        public List<string> TodasPalabras { get; internal set; }
        public List<string> PalabrasSimples { get; internal set; }
        public List<string> PalabrasCompuestas { get; internal set; }
        public List<string> ErroresSintacticos { get; internal set; }
        public List<ExpresionMatematica> ExpresionesMatematicas { get; internal set; }

        internal ResPalabras()
        {
            TodasPalabras = new List<string>();
            PalabrasSimples = new List<string>();
            PalabrasCompuestas = new List<string>();
            ErroresSintacticos = new List<string>();
            ExpresionesMatematicas = new List<ExpresionMatematica>();
        }
    }
}
