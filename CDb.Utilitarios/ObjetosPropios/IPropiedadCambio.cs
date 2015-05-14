using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CDb.Transversal.Utilitarios
{
    public interface IPropiedadCambioConEvento :
        IPropiedadCambio, INotifyPropertyChanged { }


    public interface IPropiedadCambio
    {
        void LevantarCambioPropiedad(string nombre);

        void LevantarCambioPropiedad(Expression<Func<object>> propiedad);

        void DespuesPropiedadCambiada(Expression<Func<object>> propiedad);

        void DespuesPropiedadCambiada(string propiedad);
    }
}

