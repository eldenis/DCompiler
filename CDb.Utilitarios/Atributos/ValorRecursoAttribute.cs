using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{
    public class ValorRecursoAttribute : Attribute
    {

        public ValorRecursoAttribute(Type tipoArchivoRecursos, string valorArchivoRecursos)
            : this(tipoArchivoRecursos, valorArchivoRecursos, null) { }

        public ValorRecursoAttribute(Type tipoArchivoRecursos, string valorArchivoRecursos,
            string valorPredeterminado)
        {
            TipoArchivoRecursos = tipoArchivoRecursos;
            ValorArchivoRecursos = valorArchivoRecursos;

            try
            {
                Valor = (string)tipoArchivoRecursos
                    .GetProperty(valorArchivoRecursos).GetValue(null, null);
            }
            catch (Exception) { Valor = valorPredeterminado; }
        }

        public Type TipoArchivoRecursos { get; private set; }
        public string ValorArchivoRecursos { get; private set; }
        public string Valor { get; private set; }

    }
}
