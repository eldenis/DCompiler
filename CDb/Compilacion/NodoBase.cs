using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    public class NodoBinario : ExpresionBase
    {
        public NodoBinario(string contenido, string contenidoPresentacion) :
            base(contenido, contenidoPresentacion) { }


        public ExpresionBase Izquierdo { get; internal set; }

        public ExpresionBase Derecho { get; internal set; }

        public string Separador { get; internal set; }

    }

    public class NodoRaiz : NodoBinario
    {

        public List<ExpresionSeparada> ExpresionesIzquierdas { get; private set; }
        public List<ExpresionSeparada> ExpresionesDerechas { get; private set; }
        public List<int> Niveles { get; private set; }


        private NodoRaiz(string contenido, string contenidoPresentacion)
            : base(contenido, contenidoPresentacion) { }


        internal static NodoRaiz CrearNodoRaiz(string izquierdo, string derecho,
            List<ExpresionSeparada>[] expresiones, TablaSimbolos simbolos)
        {
            string izquierdoPresentacion = izquierdo;
            string derechoPresentacion = derecho;


            while (expresiones[0].Any(y => izquierdoPresentacion.Contains(y.Identificador)))
                expresiones[0].ForEach(x => { izquierdoPresentacion = izquierdoPresentacion.Replace(x.Identificador, x.Contenido); });

            while (expresiones[1].Any(y => derechoPresentacion.Contains(y.Identificador)))
                expresiones[1].ForEach(x => derechoPresentacion = derechoPresentacion.Replace(x.Identificador, x.Contenido));

            NodoRaiz nodoRaiz = new NodoRaiz(string.Format("{0}={1}", izquierdo, derecho),
                string.Format("{0}={1}", izquierdoPresentacion, derechoPresentacion))
                {
                    ExpresionesIzquierdas = expresiones[0],
                    ExpresionesDerechas = expresiones[1],

                    Separador = "=",
                    Izquierdo = ConstruirExpresionHija(simbolos, expresiones[0], izquierdo, izquierdoPresentacion),
                    Derecho = ConstruirExpresionHija(simbolos, expresiones[1], derecho, derechoPresentacion),
                };

            nodoRaiz.Niveles = Enumerable.Range(0, ContarNiveles(nodoRaiz)).ToList();
            return nodoRaiz;
        }

        private static int ContarNiveles(ExpresionBase nodo)
        {
            int retval = 0;

            if (nodo is NodoBinario)
            {
                retval++;
                var nb = nodo as NodoBinario;

                var izquierdo = ContarNiveles(nb.Izquierdo);
                var derecho = ContarNiveles(nb.Derecho);

                retval += izquierdo > derecho ? izquierdo : derecho;

            }
            else
                retval++;


            return retval;
        }


        private static List<Tuple<char, int, int>> UbicarOperadores(TablaSimbolos simbolos, string contenido)
        {

            #region Obtener operadores en contenido
            var simboloOperador = simbolos.Where(x => x.TipoSimbolo == TiposSimbolos.Operador).Single();

            var operadores = new List<Tuple<char, int, int>>();

            for (int i = 0; i < contenido.Length; i++)
            {
                var c = contenido[i];

                if (c.PerteneceAlTipo(simboloOperador)) //caracter, posicion, jerarquía
                    operadores.Add(Tuple.Create(c, i, TablaSimbolos.Operadores.IndexOf(c)));
            }

            return operadores;
            #endregion
        }

        private static ExpresionBase ConstruirExpresionHija(TablaSimbolos simbolos,
            List<ExpresionSeparada> expresiones, string contenido, string contenidoPresentacion)
        {
            var operadores = UbicarOperadores(simbolos, contenido);
            //ExpresionSeparada identificadorUbicado = null;// = expresiones.Where(x => x.Identificador == contenido).SingleOrDefault();

            #region Eliminar Separadores sin operadores y ubicar expresión al inicio
            while (operadores.Count == 0)
            {
                #region Eliminar Separadores sin operadores
                var simboloSeparador = simbolos.Where(x => x.TipoSimbolo == TiposSimbolos.Separador).Single();
                var nuevoContenido = new StringBuilder();

                foreach (var c in contenido)
                    if (!c.PerteneceAlTipo(simboloSeparador))
                        nuevoContenido.Append(c);

                contenido = nuevoContenido.ToString();
                #endregion
                operadores = UbicarOperadores(simbolos, contenido);
                var identificadorUbicado = expresiones.Where(x => x.Identificador == contenido).SingleOrDefault();

                if (identificadorUbicado == null) break;
                else
                {
                    #region Actualizar contenido por el identificador encontrado
                    contenido = identificadorUbicado.Contenido;
                    #endregion
                }
            }
            #endregion

            if (operadores.Count > 0)
            {
                var sel = operadores.OrderBy(x => x.Item3).ThenBy(x => x.Item2)
                   .Select(x => new { Caracter = x.Item1, Posicion = x.Item2, Jerarquia = x.Item3 })
                   .First();

                var partes = contenido.Split(new char[] { sel.Caracter }, 2);
                var partesPresentacion = contenido.Split(new char[] { sel.Caracter }, 2);

                #region Eliminación de separadores rogue

                TablaSimbolos.Separadores.ForEach(x =>
                {
                    if (partes[0].Count(y => y == x.Item1) != partes[0].Count(y => y == x.Item2))
                    {
                        partes[0] = partes[0].Remove(partes[0].IndexOf(x.Item1), 1);
                        partes[1] = partes[1].Remove(partes[1].LastIndexOf(x.Item2), 1);
                        partesPresentacion[0] = partesPresentacion[0].Remove(partesPresentacion[0].IndexOf(x.Item1), 1);
                        partesPresentacion[1] = partesPresentacion[1].Remove(partesPresentacion[1].LastIndexOf(x.Item2), 1);
                    }
                });
                #endregion

                #region Reemplazo de EX` por su valor real
                while (expresiones.Any(y => partesPresentacion[0].Contains(y.Identificador)))
                {
                    expresiones.ForEach(x =>
                    {
                        if (partes[0] == x.Identificador) partes[0] = x.Contenido;
                        partesPresentacion[0] = partesPresentacion[0].Replace(x.Identificador, x.Contenido);
                    });
                }

                while (expresiones.Any(y => partesPresentacion[1].Contains(y.Identificador)))
                {
                    expresiones.ForEach(x =>
                    {
                        if (partes[1] == x.Identificador) partes[1] = x.Contenido;
                        partesPresentacion[1] = partesPresentacion[1].Replace(x.Identificador, x.Contenido);
                    });
                }
                #endregion


                var nodo = new NodoBinario(contenido, contenidoPresentacion)
                      {
                          Izquierdo = ConstruirExpresionHija(simbolos, expresiones, partes[0], partesPresentacion[0]),
                          Derecho = ConstruirExpresionHija(simbolos, expresiones, partes[1], partesPresentacion[1]),
                          Separador = sel.Caracter.ToString()
                      };

                return nodo;
            }
            else
                return new ExpresionBase(contenido);
        }
    }
}
