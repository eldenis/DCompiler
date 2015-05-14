using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{
    /// <summary>
    /// Una tupla de dos elementos modificables (Dupla)
    /// </summary>
    /// <typeparam name="T1">Tipo de Item1</typeparam>
    /// <typeparam name="T2">Tipo de Item2</typeparam>
    public class Dupla<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Dupla() { }
        public Dupla(T1 v1, T2 v2)
        {
            Item1 = v1;
            Item2 = v2;
        }
    }

    public static class Dupla
    {
        public static Dupla<Tipo1, Tipo2> Crear<Tipo1, Tipo2>(Tipo1 v1, Tipo2 v2)
        {
            return new Dupla<Tipo1, Tipo2>(v1, v2);
        }
    }
}
