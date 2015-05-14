using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Linq.Expressions;
using CDb.Transversal.Utilitarios;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CDb.Transversal.Utilitarios
{
    public static class ExtensionesVarias
    {
        public static string EliminarCaracteresEscape(this string valor)
        {
            if (valor == null) throw new ArgumentNullException(valor, "La cadena no puede ser null");

            return valor.Replace(@"\", @"");
        }

        public static string ValorRecursos(this MemberInfo mi)
        {
            if (mi != null)
            {
                var tipoAtributo = typeof(ValorRecursoAttribute);

                if (Attribute.IsDefined(mi, tipoAtributo))
                {
                    var valorRecurso = (ValorRecursoAttribute)Attribute
                        .GetCustomAttribute(mi, tipoAtributo);

                    return valorRecurso.Valor;
                }
            }
            return null;
        }

        public static int MaxOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
        {
            try { return enumerable.Max(selector); }
            catch (Exception) { }

            return default(int);
        }

        public static int MinOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
        {
            try { return enumerable.Min(selector); }
            catch (Exception) { }

            return default(int);
        }

        public static T Instanciar<T>(this Type tipo, params object[] args) where T : class
        {
            return (T)Activator.CreateInstance(tipo, args);
        }

        public static T CrearInstancia<T>(this T objeto, params object[] args) where T : class
        {
            return objeto.GetType().Instanciar<T>(args);
        }

        /// <summary>
        /// Obtiene un valor que indica si una <see cref="ICollection"/> es nula
        /// o se encuentra vacía.
        /// </summary>
        /// <param name="col"></param>
        /// <returns>true si la colección está vacía o nula.</returns>
        public static bool EsNuloOVacio(this ICollection col)
        {
            return col == null || col.Count == 0;
        }

        /// <summary>
        /// Obtiene un valor que indica si la variable es nula
        /// </summary>        
        /// <returns>True si el valor es nulo</returns>
        public static bool EsNulo(this object valor) { return valor == null; }

        public static bool EsNullable(this MemberInfo mi)
        {
            var retval = false;

            var pi = (mi as PropertyInfo);

            if (pi != null)
                retval = pi.PropertyType.IsGenericType &&
                    pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

            return retval;
        }

        public static MetadataTypeAttribute GetMetadataType(this Type tipo)
        {
            return (MetadataTypeAttribute)
                      tipo.GetCustomAttributes(typeof(MetadataTypeAttribute), true)
                      .FirstOrDefault();
        }

        /// <summary>
        /// Retorna la Action(T) como Action(object)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accion"></param>
        /// <returns></returns>
        public static Action<object> ConvertirAObject<T>(this Action<T> accion)
        {
            return (o) => { accion((T)o); };
        }

        /// <summary>
        /// Suma el porcentaje pasado al valor decimal.
        /// </summary>        
        /// <param name="porcentaje">El porcentaje a sumar. Ej. 12 -> 12%</param>
        /// <returns>El mismo decimal con el porcentaje sumado</returns>
        public static decimal SumarPorcentaje(this decimal valor, decimal porcentaje)
        {
            return valor * (1 + (porcentaje / 100));
        }

        /// <summary>
        /// Resta el porcentaje pasado al valor decimal.
        /// </summary>        
        /// <param name="porcentaje">El porcentaje a restar. Ej. 12 -> 12%</param>
        /// <returns>El mismo decimal con el porcentaje restado</returns>
        public static decimal RestarPorcentaje(this decimal valor, decimal porcentaje)
        {
            return valor / (1 + (porcentaje / 100));
        }

        public static decimal CalcularPorcentaje(this decimal valor, decimal porcentaje)
        {
            return valor * (porcentaje / 100);
        }

        /// <summary>
        /// Retorna true si la cadena "otra" está contenida en la "cadena"
        /// </summary>
        /// <param name="cadena">Cadena original</param>
        /// <param name="otra">Cadena que se desea buscar</param>
        /// <returns>true si "cadena" contiene "otra"</returns>
        public static bool ContainsInvariant(this string cadena, string otra)
        {
            if (string.IsNullOrWhiteSpace(cadena) || string.IsNullOrWhiteSpace(otra))
                return false;

            return cadena.ToUpper().SinDiacriticos().Contains(otra.ToUpper().SinDiacriticos());
        }

        /// <summary>
        /// Retorna el contenido de todas las propiedades del objeto junto con su nombre
        /// </summary>
        /// <param name="obj">El objeto para el que se quieren las propiedades</param>
        /// <returns>Una cadena con el nombre y valor de todas las propiedades.</returns>        
        public static string Dump(this object obj)
        {
            var props = obj.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();

            try { props.ForEach(pi => sb.AppendFormat("{0}:{1}\n", pi.Name, pi.GetValue(obj, null))); }
            catch (Exception e) { }

            return sb.ToString();
        }

        /// <summary>
        /// Retorna el contenido de todas las propiedades del objeto.
        /// </summary>
        /// <param name="obj">El objeto para el que se quieren las propiedades</param>
        /// <returns>Una cadena con el VALOR de todas las propiedades</returns>
        public static string ValuesToString(this object obj)
        {
            return obj.ValuesToString(null);
        }

        /// <summary>
        /// Retorna el contenido de las propiedades del objeto especificadas.
        /// </summary>
        /// <param name="obj">El objeto para el que se quieren las propiedades</param>
        /// <returns>Una cadena con el VALOR de las propiedades especificadas</returns>
        public static string ValuesToString(this object obj, ICollection<PropertyInfo> props)
        {
            if (props == null || props.Count == 0) props = obj.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();

            try { props.ForEach(pi => sb.Append(pi.GetValue(obj, null))); }
            catch (Exception e) { }

            return sb.ToString();
        }

        public static String SinDiacriticos(this String s)
        {
            String cadenaNormalizada = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cadenaNormalizada.Length; i++)
            {
                Char c = cadenaNormalizada[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString();
        }


        public static MethodInfo GetMethodInfo(this Expression<Action> a)
        {
            return Utilitarios.ObtenerMethodInfo(a);
        }

        public static string GetMemberName(this Expression<Func<object>> a)
        {
            return Utilitarios.ObtenerNombreMiembro(a);
        }

        public static string GetMemberName<T>(this Expression<Func<T, object>> a)
        {
            return Utilitarios.ObtenerNombreMiembro(a);
        }

        public static bool ComienzaEnMinuscula(this string valor)
        {
            return char.IsLower(valor, 0);
        }

        public static IQueryable SelectAnonymous(this IQueryable objectSet,
            List<string> propiedadesAListar, string otros = "")
        {
            if (otros.Length > 0) otros = ", " + otros;

            var res = objectSet.Select("new (" + string.Join(",",
                propiedadesAListar.Select(p => p)
                ) + otros + ")");

            return res;
        }

        public static string MayusculaAlInicio(this string valor)
        {
            return Char.ToUpper(valor[0]) + valor.Substring(1, valor.Length - 1);
        }


        public static List<T> AsListOf<T>(this ICollection iLista, bool sinError = false)
        {
            if (sinError) //Retorna los elementos de tipo T que consiga. 
                return iLista.OfType<T>().ToList();
            else//Si hay algún elemento de tipo != T levanta una excepción
                return iLista.Cast<T>().ToList();
        }

        public static void ForEach<T>(this ICollection<T> elementos, Action<T> accion)
        {
            foreach (T i in elementos)
                accion(i);
        }
        public static void ForEach<T>(this IEnumerable<T> elementos, Action<T> accion)
        {
            elementos.ToArray().ForEach(accion);
        }

        public static void AddRange<T>(this ICollection<T> col, IEnumerable<T> itemes)
        {
#if DEBUG
            if (col == null)
                throw new Exception("La colección es null");
#endif
            if (col != null)
                itemes.Where(item => item != null).ToList().ForEach(item => col.Add(item));
        }


        /// <summary>
        /// Agrega elementos a una lista sin agregar instancias duplicadas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <param name="itemes"></param>
        public static void AddNoDuplicates<T>(this ICollection<T> col, ICollection<T> itemes)
        {
            foreach (T item in itemes)
            {
                if (item != null && !col.Contains(item))
                {
                    col.Add(item);
                }
            }
        }
        /// <summary>
        /// Agrega elementos a una colección cuando cumplen con una condición
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <param name="itemes"></param>
        /// <param name="funcion"></param>
        /// <returns>Los elementos que se agregar a la colección</returns>
        public static ICollection<T> AddWithCondition<T>(this ICollection<T> col, ICollection<T> itemes, Func<T, T, bool> funcion)
        {
            var aAgregar = itemes;

            if (col != null && col.Count > 0)
            {
                aAgregar = (from i in itemes
                            where col.All(c2 => funcion(c2, i))
                            select i).ToList();
            }

            col.AddRange(aAgregar);

            return aAgregar;
        }


        public static ICollection<T> AddWithCondition<T, T2>(this ICollection<T> col, ICollection<T2> itemes,
          Func<T, T2, bool> funcion,
          Func<T2, T> funcionConvertir
          )
        {
            ICollection<T> aAgregar = null;

            if (col.Count > 0)
            {
                aAgregar = (from i in itemes
                            where col.All(c => funcion(c, i))
                            select funcionConvertir(i)).ToList();
            }
            else
                aAgregar = itemes.Select(item => funcionConvertir(item)).ToList();


            col.AddRange(aAgregar);

            return aAgregar;
        }

        public static ICollection<T> AddWithCondition<T>(this ICollection<T> col, ICollection<T> itemes, Func<T, bool> funcion)
        {
            var aAgregar = itemes.Where(item => funcion(item)).ToList();

            col.AddRange(aAgregar);

            return aAgregar;
        }

        public static bool ContainsWithCondition<T>(this ICollection<T> col, T item, Func<T, T, bool> funcion)
        {
            /*var query = from i in col
                        select funcion(i, item);*/

            //al conseguir la primera que coincida, retorna true
            //foreach (var i in query) { if (i) return true; }
            var retval = col.Any(i => funcion(i, item));
            return retval;
        }

        public static void RemoveWithCondition<T>(this ICollection<T> col, T item, Func<T, T, bool> funcion)
        {
            var elementos = (from i in col
                             where funcion(i, item)
                             select i).ToList();

            col.RemoveRange<T>(elementos);
        }

        public static void RemoveRange<T>(this ICollection<T> col, ICollection<T> itemes)
        {
            itemes.ForEach(it => col.Remove(it));
        }


        /// <summary>
        /// Crea una lista nueva con los elementos de listaNueva y si llega 
        /// a encontrar un duplicado entre listaOriginal y listaNueva toma 
        /// el de listaOriginal y lo elimina de listaOriginal.
        /// </summary>
        /// <typeparam name="T">El tipo de dato de las listas</typeparam>
        /// <param name="listaOriginal">La lista original. Esta lista 
        /// tendrá preferencia. Al encontrar duplicados se tomará la instancia en esta lista</param>
        /// <param name="listaNueva">La lista de donde se tomarán los otros registros que 
        /// no se encuentren ya en listaOriginal</param>
        /// <param name="funcion">La función que permite saber cuándo dos artículos son iguales.</param>
        /// <returns>Una tercera lista con los elementos que estaban en listaOriginal
        /// y los que se pasaron en listaNueva, dejando listaOriginal sin las instancias
        /// que se copiaron a esta lista.</returns>
        public static List<T> ObtenerListaEntidadesUnicas<T>(this ICollection<T> listaOriginal,
                    ICollection<T> listaNueva, Func<T, T, bool> funcion)
        {

            IEnumerable<T> listaDuplicados = from elOriginal in listaOriginal
                                             from elNuevo in listaNueva
                                             where funcion(elOriginal, elNuevo)
                                             select elOriginal;

            List<T> listaFinal = listaDuplicados.ToList();

            listaOriginal.RemoveRange<T>(listaFinal);

            listaFinal.AddWithCondition(listaNueva, (v1, v2) => { return !funcion(v1, v2); });

            return listaFinal;
        }
    }
}
