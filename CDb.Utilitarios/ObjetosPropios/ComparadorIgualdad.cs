using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{
    public class ComparadorIgualdad<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> Funcion { get; private set; }
        public bool Negado { get; private set; }

        public ComparadorIgualdad(Func<T, T, bool> funcion, bool negado = false)
        {
            Funcion = funcion;
            Negado = negado;
        }

        public bool Equals(T x, T y)
        {
            return Negado ? !Funcion(x, y) : Funcion(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    public static class ComparadorFuncion
    {
        public static ComparadorIgualdad<T> CrearComparador<T>(Func<T, T, bool> funcion, bool negado = false)
        {
            return new ComparadorIgualdad<T>(funcion, negado);
        }
    }
}
