using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CDb.Transversal.Utilitarios
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CampoListaGenericaAttribute : Attribute
    {
        public CampoListaGenericaAttribute(String nombreEnPantalla, bool visible = true, bool ordernarPor = false)
        {
            _visibleEnPantalla = visible;
            _nombreEnPantalla = nombreEnPantalla;
            _ordernarPor = ordernarPor;
        }

        public CampoListaGenericaAttribute(Type archivoRecursos, string nombreValorArchivoRecursos, bool visible = true, bool ordernarPor = false)
        {
            _visibleEnPantalla = visible;
            _archivoRecursos = archivoRecursos;
            _nombreValorArchivoRecursos = nombreValorArchivoRecursos;
            _ordernarPor = ordernarPor;

            _nombreEnPantalla = (string)_archivoRecursos
                .GetProperty(_nombreValorArchivoRecursos).GetValue(null, null);
        }

        private string _nombreEnPantalla;
        public string NombreEnPantalla { get { return _nombreEnPantalla; } }

        private bool _visibleEnPantalla;
        public bool VisibleEnPantalla { get { return _visibleEnPantalla; } }

        private bool _ordernarPor;
        public bool OrdernarPor { get { return _ordernarPor; } }

        private Type _archivoRecursos;
        public Type ArchivoRecursos { get { return _archivoRecursos; } }

        private string _nombreValorArchivoRecursos;
        public string NombreValorArchivoRecursos { get { return _nombreValorArchivoRecursos; } }

    }

}
