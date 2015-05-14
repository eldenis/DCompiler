using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CDb.Compilacion
{
    public enum TiposSimbolos
    {
        Ninguno = -1,
        Comentario = 0,
        Letra = 1,
        Número = 2,
        Operador = 3,
        Separador = 4
    }

    public class Simbolo
    {

        public TiposSimbolos TipoSimbolo { get; internal set; }
        public Regex RegularExpression { get; internal set; }
        internal string _regex;

        public Simbolo(TiposSimbolos tipo, string regex)
        {
            TipoSimbolo = tipo;
            _regex = regex;
            RegularExpression = new Regex(regex);
        }


    }

    internal class TablaSimbolos : List<Simbolo>
    {

        internal static List<Tuple<char, char>> Separadores =
            new List<Tuple<char, char>> { Tuple.Create('(', ')'), Tuple.Create('[', ']'), Tuple.Create('{', '}') };

        internal static List<char> Operadores = new List<char> { '+', '-', '*', '/', '^' };

        internal TablaSimbolos() { }
    }

    public static class Extensiones
    {
        public static bool PerteneceAlTipo(this char caracter, Simbolo simbolo)
        {
            return caracter.ToString().PerteneceAlTipo(simbolo);
        }

        public static bool PerteneceAlTipo(this string caracter, Simbolo simbolo)
        {
            return simbolo.RegularExpression.IsMatch(caracter);
        }
    }
}
