using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class ExpresionMatematica : ExpresionBase
    {
        public NodoRaiz Raiz { get; private set; }

        internal ExpresionMatematica(int posicion, string contenido)
            : base(posicion, contenido) { }

        internal void CrearNodoRaiz(TablaSimbolos simbolos, List<string> palabrasDefinidas)
        {
            var cuentaRaiz = Contenido.Count(c => c == '=');

            if (cuentaRaiz == 0)
                throw new ExCompilacion(string.Format(Errores.SimboloRaizFaltante, Contenido));

            if (cuentaRaiz > 1)
                throw new ExCompilacion(string.Format(Errores.SimboloRaizMultipleVeces, Contenido, cuentaRaiz));

            var jerarquiaMayorSeparadores = TablaSimbolos.Separadores.Count - 1;

            for (int i = 0; i < TablaSimbolos.Separadores.Count; i++)
            {
                var separador = TablaSimbolos.Separadores[i];

                var cuentaInicio = Contenido.Count(x => x == separador.Item1);
                var cuentaFinal = Contenido.Count(x => x == separador.Item2);

                if (cuentaInicio != cuentaFinal)
                    throw new ExCompilacion(string.Format(Errores.SeparadoresDesbalanceados,
                        separador.Item1, separador.Item2, cuentaInicio,
                        separador.Item1, cuentaFinal, separador.Item2));
            }

            var partes = Contenido.Split('=');
            var listaExpresiones = new List<ExpresionSeparada>[] { new List<ExpresionSeparada>(), new List<ExpresionSeparada>() };

            for (int j = 0; j < partes.Length; j++) //Deberían ser siempre dos.
            {

                char? valorAnterior = null;
                var simboloValorAnterior = new Simbolo(TiposSimbolos.Ninguno, "");

                //caracter, posicion, jerarquía
                var separadoresAbiertos = new List<Tuple<char, int, int>>();
                var separadoresCerrados = new List<Tuple<char, int, int>>();

                var cadenaSeparada = new StringBuilder();

                for (int i = 0; i < partes[j].Length; i++)
                {
                    var c = partes[j][i];

                    var simboloC = simbolos.Where(x => c.PerteneceAlTipo(x)).Single();

                    if (valorAnterior != null)
                    {
                        if (simboloValorAnterior.TipoSimbolo == TiposSimbolos.Operador && simboloC.TipoSimbolo == TiposSimbolos.Operador)
                            throw new ExCompilacion(string.Format(Errores.DosOperadoresJuntos, valorAnterior, c, Contenido));

                        if ((simboloValorAnterior.TipoSimbolo == TiposSimbolos.Letra || simboloValorAnterior.TipoSimbolo == TiposSimbolos.Número)
                            && (simboloC.TipoSimbolo != TiposSimbolos.Operador && simboloC.TipoSimbolo != TiposSimbolos.Separador))
                            throw new ExCompilacion(string.Format(Errores.DosLetrasNumerosJuntos, valorAnterior,
                                simboloValorAnterior.TipoSimbolo, c, simboloC.TipoSimbolo, Contenido));

                        if (simboloC.TipoSimbolo == TiposSimbolos.Letra && !palabrasDefinidas.Contains(c.ToString()))
                            throw new ExCompilacion(string.Format(Errores.IdentificadorNoDeclarado, c, Contenido));

                    }

                    valorAnterior = c;
                    simboloValorAnterior = simboloC;

                    cadenaSeparada.Append(valorAnterior);

                    if (simboloValorAnterior.TipoSimbolo == TiposSimbolos.Separador)
                    {
                        var simbolo = TablaSimbolos.Separadores.Where(x => x.Item1 == valorAnterior || x.Item2 == valorAnterior).Single();

                        var jerarquiaSeparador = TablaSimbolos.Separadores.IndexOf(simbolo);

                        var abriendo = valorAnterior == simbolo.Item1;

                        if (abriendo)
                        {
                            var ultimoSeparador = separadoresAbiertos.LastOrDefault();

                            if (ultimoSeparador != null && //Validar jerarquía de separador
                                ultimoSeparador.Item3 <= jerarquiaSeparador &&
                                ultimoSeparador.Item3 != jerarquiaMayorSeparadores)
                                throw new ExCompilacion(Errores.JerarquiaSeparadoresInvalida);

                            separadoresAbiertos.Add(Tuple.Create(valorAnterior.Value, i, jerarquiaSeparador));
                        }
                        else
                        {
                            if (cadenaSeparada.Length == 1)//Si hay un solo caracter y es de cierre.
                                throw new ExCompilacion(string.Format(Errores.SeparadorInvalido, valorAnterior, Contenido));

                            var caracteresSeparacion = Tuple.Create(separadoresAbiertos.Last().Item1, valorAnterior.Value);

                            var separador = TablaSimbolos.Separadores
                                .Where(sep => sep.Item1 == caracteresSeparacion.Item1 && sep.Item2 == caracteresSeparacion.Item2)
                                .SingleOrDefault();

                            if (separador == null)
                                throw new ExCompilacion(string.Format(Errores.SeparadoresInvalidos,
                                    caracteresSeparacion.Item1, caracteresSeparacion.Item2, Contenido));

                            separadoresCerrados.Add(Tuple.Create(caracteresSeparacion.Item2, i, jerarquiaSeparador));

                            var inicio = separadoresAbiertos.Last().Item2;

                            var contenido = cadenaSeparada.ToString().Substring(inicio, separadoresCerrados.Last().Item2 - inicio + 1);

                            var posicionInicial = i - contenido.Length + 1;

                            var exResultante = new ExpresionSeparada(posicionInicial, contenido, separador, jerarquiaSeparador);

                            listaExpresiones[j].Add(exResultante);

                            #region Reemplazo de cadena en partes[j] y en SB
                            partes[j] = partes[j].Remove(posicionInicial, contenido.Length).Insert(posicionInicial, exResultante.Identificador);
                            cadenaSeparada.Remove(posicionInicial, contenido.Length).Insert(posicionInicial, exResultante.Identificador);

                            i -= exResultante.Contenido.Length - exResultante.Identificador.Length;
                            #endregion

                            separadoresAbiertos.RemoveAt(separadoresAbiertos.Count - 1);
                        }
                    }
                }
            }

            Raiz = NodoRaiz.CrearNodoRaiz(partes[0], partes[1], listaExpresiones, simbolos);
        }//fin de creación de nodo raíz
    }
}
