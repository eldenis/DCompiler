using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{
    public class PropiedadIDAttribute : Attribute
    {
        public PropiedadIDAttribute(Type tipoEntidad)
        {
            TipoEntidad = tipoEntidad;
        }

        public Type TipoEntidad { get; set; }
    }
}
