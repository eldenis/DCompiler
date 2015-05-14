using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    internal class AnalizadorSintactico
    {
        internal static ResPalabras ExtraerPalabras(string texto, TablaPalabras tablaPalabras,
            TablaSimbolos simbolos, char[] simbolosEspacio,
            Tuple<Palabra, Palabra> separadoresExMatematica)
        {
            var res = new ResPalabras();
            var listaPalabras = tablaPalabras.Select(c => c.Value.Valor).ToList();
            var textoDividido = texto.Split(simbolosEspacio, StringSplitOptions.RemoveEmptyEntries);

            foreach (var palabraLeida in textoDividido)
            {
                res.TodasPalabras.Add(palabraLeida);

                if (listaPalabras.Contains(palabraLeida))
                {
                    var palabra = tablaPalabras[palabraLeida];

                    if (palabra.Clase == ClasesPalabras.Simple)//Palabra simple
                        res.PalabrasSimples.Add(palabra.Valor);
                    else if (palabra.Clase == ClasesPalabras.Compuesta)//Palabra compuesta
                    {
                        res.PalabrasCompuestas.Add(palabra.Valor);
                        var cantPalabras = textoDividido.Count(x => x == palabra.Valor);

                        if (palabra.Palabra1 != null)
                        {//Procesar palabra1
                            var cantPalabras1 = textoDividido.Count(x => x == palabra.Palabra1);
                            if (cantPalabras != cantPalabras1)
                                throw new ExCompilacion(string.Format(Errores.PalabraFaltante, palabra.Palabra1));

                            res.PalabrasCompuestas.Add(palabra.Palabra1);
                        }
                        if (palabra.Palabra2 != null)
                        {//Procesar Palabra2
                            var cantPalabras2 = textoDividido.Count(x => x == palabra.Palabra2);
                            if (cantPalabras != cantPalabras2)
                                throw new ExCompilacion(string.Format(Errores.PalabraFaltante, palabra.Palabra2));

                            res.PalabrasCompuestas.Add(palabra.Palabra2);
                        }
                    }
                }
                else
                {//Palabra desconocida
                    res.ErroresSintacticos.Add(palabraLeida);
                }
            }

            res.ExpresionesMatematicas = ExtraerExpresionesMatematicas(simbolos,
                textoDividido.ToList(), separadoresExMatematica, res.ErroresSintacticos);


            return res;
        }

        private static List<ExpresionMatematica> ExtraerExpresionesMatematicas(TablaSimbolos simbolos,
            List<string> textoDividido, Tuple<Palabra, Palabra> separadoresExMatematica, List<string> palabrasDefinidas)
        {
            var lista = new List<ExpresionMatematica>();
            var skip = 0;
            var longitudTexto = textoDividido.Count;

            while (skip < longitudTexto)
            {
                var posInicioCom = textoDividido.Skip(skip).TakeWhile(t => t != separadoresExMatematica.Item1.Valor).Count() + skip;
                if (posInicioCom >= longitudTexto) break;

                skip = posInicioCom + 1;
                var posFinCom = textoDividido.Skip(skip).TakeWhile(t => t != separadoresExMatematica.Item2.Valor).Count() + skip;

                var exMatematica = new ExpresionMatematica(posicion: posInicioCom,
                    contenido: string.Concat(textoDividido.Skip(skip).Take(posFinCom - posInicioCom - 1).ToArray()));

                exMatematica.CrearNodoRaiz(simbolos, palabrasDefinidas);

                lista.Add(exMatematica);

                skip += posFinCom - posInicioCom;
            }

            return lista;
        }
    }
}
