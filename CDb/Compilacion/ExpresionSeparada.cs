using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class ExpresionSeparada : ExpresionBase
    {

        public ExpresionSeparada(int posicion, string contenido,
            Tuple<char, char> separador, int jerarquia)
            : base(posicion, contenido)
        {
            Separador = separador;
            Jerarquia = jerarquia;
            Identificador = CrearIdentificador();
        }

        public string Identificador { get; internal set; }

        public Tuple<char, char> Separador { get; internal set; }

        public int Jerarquia { get; internal set; }

        public List<ExpresionSeparada> ExpresionesInterna { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} | {1} |", Identificador, Contenido);
        }


        private static string CrearIdentificador()
        {//65-90 97-122
            //EX`
            //var letras = new char[] { 
            //    (char)randomizer.Next(65, 91), 
            //    (char)randomizer.Next(97, 123), 
            //    (char)randomizer.Next(65, 91) };

            return string.Format("EX`{0}", ++_cuenta);
        }
        private static int _cuenta = 0;
        //private static Random randomizer = new Random(123);

    }
}
