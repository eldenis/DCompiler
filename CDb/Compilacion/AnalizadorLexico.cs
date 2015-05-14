using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Transversal.Utilitarios;

namespace CDb.Compilacion
{

    internal static class AnalizadorLexico
    {
        internal static List<Comentario> ExtraerComentarios(string texto, char simboloComentarios)
        {
            var simboloComentariosEncontrados = texto.Count(t => t == simboloComentarios);

            if ((simboloComentariosEncontrados & 1) != 0)
                throw new ExCompilacion(Errores.NumeroIdentificadorComentariosInvalido);

            var comentarios = new List<Comentario>();
            var skip = 0;
            var longitudTexto = texto.Length;

            while (skip < longitudTexto)
            {
                var posInicioCom = texto.Skip(skip).TakeWhile(t => t != simboloComentarios).Count() + skip;
                if (posInicioCom >= longitudTexto) break;

                skip = posInicioCom + 1;
                var posFinCom = texto.Skip(skip).TakeWhile(t => t != simboloComentarios).Count() + skip;

                comentarios.Add(new Comentario(posicion: posInicioCom,
                    contenido: texto.Substring(posInicioCom, posFinCom - posInicioCom + 1)));

                skip += posFinCom - posInicioCom;
            }

            return comentarios;
        }

        internal static string EliminarCaracteresEspaciado(string texto, List<CuentaCaracter> cuentasSimbolos)
        {
            var retval = texto;
            for (int i = 0; i < cuentasSimbolos.Count; i++)
            {
                var nuevaCuenta = retval.Count(caracter => caracter == cuentasSimbolos[i].Caracter);

                cuentasSimbolos[i].Cuenta = nuevaCuenta;

                retval = retval.Replace(cuentasSimbolos[i].Caracter.ToString(), "");
            }

            cuentasSimbolos.Add(new CuentaCaracter
            {
                Cuenta = retval.Length,
                NombreCaracter = "Caracter"
            });
            return retval;
        }

        internal static void RealizarClasificacionTokens(string texto, List<CuentaCaracter> cuentasTokens,
            TablaSimbolos tablaSimbolos)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                var caracter = texto[i].ToString();
                for (int j = 0; j < tablaSimbolos.Count; j++)
                {
                    var simbolo = tablaSimbolos[j];
                    if (simbolo.RegularExpression.IsMatch(caracter))
                    {
                        cuentasTokens.Find(p => p.TipoToken == simbolo.TipoSimbolo).ListaCaracteres.Append(caracter);
                        break;
                    }
                    if (j == tablaSimbolos.Count - 1)
                        throw new ExCompilacion(string.Format(Errores.CaracterInvalido, caracter, i + 1));
                }
            }
        }

        internal static string EliminarComentarios(string texto, ICollection<Comentario> comentarios)
        {
            foreach (var comentario in comentarios.Select(c => c.Contenido).Distinct())
                texto = texto.Replace(comentario, "");

            return texto;
        }


    }
}
