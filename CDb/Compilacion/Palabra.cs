using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class Palabra
    {
        public string Valor { get; internal set; }
        public ClasesPalabras Clase { get; internal set; }
        public string Palabra1 { get; internal set; }
        public string Palabra2 { get; internal set; }

        internal Palabra() { }
        public Palabra(string palabra, ClasesPalabras clase,
            string palabra1 = null, string palabra2 = null)
        {
            Valor = palabra;
            Clase = clase;
            Palabra1 = palabra1;
            Palabra2 = palabra2;
        }
    }

    public class TablaPalabras : Dictionary<string, Palabra>
    {
        internal TablaPalabras() { }
    }

    public enum ClasesPalabras
    {
        Simple = 1,
        Compuesta = 2
    }
}
