using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios.ObjetosPropios
{
    /// <summary>
    /// Clase que sirve para un evento de PropiedadCambiando
    /// que permite cancelar el cambio de valor y consultar
    /// los dos valores (actual y nuevo)
    /// </summary>
    public class PropiedadCambiandoEventArgs
    {
        public bool Cancelar { get; set; }
        public object ValorNuevo { get; private set; }
        public object ValorActual { get; private set; }


        public PropiedadCambiandoEventArgs(object valorActual, object valorNuevo)
        {
            ValorActual = valorActual;
            ValorNuevo = valorNuevo;
            Cancelar = false;
        }
    }
}
