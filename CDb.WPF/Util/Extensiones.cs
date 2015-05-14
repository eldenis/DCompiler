using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using CDb.Transversal.Utilitarios;
using System.Collections;

namespace WPF.Cliente.Util
{
    /// <summary>
    /// Clase para colocar extensiones varias
    /// </summary>
    public static class Extensiones
    {

        //public static IList ObtenerObjetosAnonimos(this IList lista)
        //{
        //    if (lista != null && lista.Count > 0)
        //    {
        //        if (lista[0] is EntidadBase)
        //        {
        //            var tipoEntidad = lista[0].GetType();

        //            var valoresListaGenerica = ObtenerMetadataListaGenerica(lista[0]);

        //            if (valoresListaGenerica != null)
        //            {
        //                var nombrePropiedades = valoresListaGenerica.Item2
        //                .Select(prop => prop.Name).ToList();

        //                var anonimos = lista.AsQueryable()
        //                .SelectAnonymous(tipoEntidad, nombrePropiedades)
        //                .Cast<object>()
        //                .ToArray();

        //                return anonimos;
        //            }
        //        }
        //    }
        //    return lista;
        //}

        public static Dupla<Type, PropertyInfo[]>
            ObtenerMetadataListaGenerica(this object objeto)
        {
            var tipoObjeto = objeto.GetType();

            var tipoMetadata = tipoObjeto
                .GetCustomAttributes(typeof(MetadataTypeAttribute), false)
                .OfType<MetadataTypeAttribute>()
                .Select(md => md.MetadataClassType)
                .FirstOrDefault();

            if (tipoMetadata != null)
            {
                var propiedades = tipoMetadata
                    .GetProperties()
                    .Where(prop => prop.GetCustomAttributes(
                            typeof(CampoListaGenericaAttribute), false).Length > 0)
                    .ToArray();

                return Dupla.Crear<Type, PropertyInfo[]>(
                    tipoMetadata, propiedades);
            }
            return null;
        }

        //public static object ObtenerObjetoAnonimo(this EntidadBase entidad)
        //{
        //    if (entidad != null)
        //    {
        //        var tipoEntidad = entidad.GetType();

        //        var valoresListaGenerica = ObtenerMetadataListaGenerica(entidad);

        //        if (valoresListaGenerica != null)
        //        {
        //            var nombrePropiedades = valoresListaGenerica.Item2
        //                .Select(prop => prop.Name).ToList();

        //            var anonimos = (new object[] { entidad }).AsQueryable()
        //                .SelectAnonymous(tipoEntidad, nombrePropiedades)
        //                .Cast<object>()
        //                .ToArray();

        //            if (anonimos != null && anonimos.Length > 0)
        //                return anonimos[0];
        //        }
        //    }

        //    return entidad;
        //}


        public static IQueryable Select(this IQueryable source, string selector, Type tipoIt, params object[] values)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            LambdaExpression lambda = CDb.Transversal.Utilitarios.DynamicExpression
                .ParseLambda(tipoIt, null, selector, values);
            try
            {
                var metodo = typeof(Queryable).GetMethods().First(
                    met => met.Name == "Select")
                    .MakeGenericMethod(tipoIt, lambda.ReturnType);

                var cast = typeof(Queryable).GetMethods().First(
                     met => met.Name == "Cast")
                     .MakeGenericMethod(tipoIt)
                     .Invoke(null, new object[] { source });

                var res = metodo.Invoke(null, new object[] { cast, lambda });


                return (IQueryable)res;//((IQueryable)res).Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                //throw ex;
            }

            return null;
        }


        public static IQueryable SelectAnonymous(this IQueryable objectSet, Type tipo,
            List<string> propiedadesAListar, string otros = "")
        {
            if (otros.Length > 0) otros = ", " + otros;

            var res = objectSet.Select("new (" + string.Join(",",
                propiedadesAListar.Select(p => p)
                ) + otros + ")", tipo);

            return res;
        }
    }
}
