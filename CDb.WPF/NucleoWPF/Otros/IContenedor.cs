using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.VistaModelo.Nucleo;
using System.Collections.ObjectModel;
using WPF.Cliente.Nucleo.MensajesApp;

namespace WPF.Cliente.Nucleo
{
    public interface IContenedor
    {
        /// <summary>
        /// Elemento actual del contenedor
        /// </summary>
        VMElemento ElementoActual { get; }

        /// <summary>
        /// Diálogo actual del contenedor
        /// </summary>
        VMElemento DialogoActual { get; }

        /// <summary>
        /// Carga un elemento y lo coloca como el actual.
        /// </summary>
        /// <param name="elemento">El VM que se colocará como actual.</param>
        void CargarElemento(VMElemento elemento);

        /// <summary>
        /// Descarga el elemento actual y retorna al padre del mismo
        /// </summary>
        void DescargarElementoActual();

        /// <summary>
        /// Carga un elemento y lo coloca como el actual.
        /// </summary>
        /// <param name="elemento">El VM que se colocará como actual.</param>
        void CargarDialogo<T>(VMElemento dialogo, Action<T> accion);

        /// <summary>
        /// Descarga el elemento actual y retorna al padre del mismo
        /// </summary>
        void DescargarDialogoActual();

        /// <summary>
        /// Retorna una colección de todos los hijos que se han agregado al contenedor
        /// </summary>
        ObservableCollection<VMElemento> Hijos { get; }

        /// <summary>
        /// Cierra el contenedor
        /// </summary>
        void Cerrar(ResultadoDialogo res);
    }
}
