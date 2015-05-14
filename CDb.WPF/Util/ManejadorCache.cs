using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using WPF.Cliente.Nucleo;

namespace WPF.Cliente.Util
{
    public static class ManejadorCache
    {
        /// <summary>
        /// Obtiene un objeto de la memoria cache.
        /// </summary>
        /// <typeparam name="T">El tipo de objeto</typeparam>
        /// <param name="nombre">El nombre de la propiedad</param>
        /// <param name="sinError">Indica si se levanta una excepción si el valor no se consigue</param>
        /// <returns></returns>
        public static T ObtenerObjeto<T>(ValoresCache nombre, bool sinError = false)
        {
            ObjectCache cache = MemoryCache.Default;

            CacheItem val = cache.GetCacheItem(nombre.ToString());

            if (val == null || val.Value == null || !(val.Value is T))
            {
                //if (!sinError)
                //    throw new Exception(string.Format(Recursos.Mensajes.Generales.Error_ValorNoEncontradoMemoriaCache, nombre.ToString()));

                return default(T);
            }

            return (T)val.Value;
        }


        public static T ObtenerObjetoODefault<T>(ValoresCache nombre)
        {
            return ObtenerObjeto<T>(nombre, sinError: true);
        }

        /// <summary>
        /// Guarda un objeto en la memoria cache.
        /// 
        /// Lanza ArgumentException si el objeto es null.
        /// 
        /// </summary>
        /// <param name="nombre">El nombre a tener el valor en la cache. Se usa para obtener luego el valor.</param>
        /// <param name="obj">El objeto que se quiere guardar. No puede ser null.</param>        
        public static void GuardarObjeto<T>(ValoresCache nombre, T obj)
        {
            if (obj == null)
                throw new Exception();// ArgumentException(Recursos.Mensajes.Generales.Error_ArgumentoInvalido);


            if (!Existe(nombre))
            {
                ObjectCache cache = MemoryCache.Default;
                cache.Set(nombre.ToString(), obj, new DateTimeOffset(DateTime.Now.AddDays(100)));
            }
            else
            {
                Eliminar(nombre);
                GuardarObjeto(nombre, obj);
            }

        }

        /// <summary>
        /// Eliminar una entrada de la memoria cache
        /// </summary>
        /// <param name="nombre">El nombre del valor a eliminar</param>
        public static void Eliminar(ValoresCache nombre)
        {
            ObjectCache cache = MemoryCache.Default;

            cache.Remove(nombre.ToString());
        }

        public static bool Existe(ValoresCache nombre)
        {
            ObjectCache cache = MemoryCache.Default;

            return cache.Contains(nombre.ToString());
        }


        /// <summary>
        /// Obtiene el total de elementos que existen en la cache
        /// </summary>
        public static long Cuenta { get { return MemoryCache.Default.GetCount(); } }

        public static Dictionary<ValoresCache, T> TodosElementos<T>()
        {
            return MemoryCache.Default.Where(v => v.Value is T)
                .ToDictionary(key => (ValoresCache)Enum.Parse(typeof(ValoresCache), key.Key), sel => (T)sel.Value);
        }

        public static Dictionary<string, object> TodosElementos()
        {
            return MemoryCache.Default.ToDictionary(key => key.Key, val => val.Value);
        }

        public static List<T> TodosValores<T>()
        {
            return MemoryCache.Default.Where(p => p.Value is T).Select(p => (T)p.Value).ToList();
        }

        public static List<object> TodosValores()
        {
            return MemoryCache.Default.Select(p => p.Value).ToList();
        }
    }

    public enum ValoresCache
    {

    }
}
