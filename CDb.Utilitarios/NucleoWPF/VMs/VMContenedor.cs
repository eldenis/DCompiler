using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.Nucleo;
using System.Collections.ObjectModel;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.Nucleo.MensajesApp;
using System.Collections.Specialized;
//using WPF.Cliente.VistaModelo.Ventas.PoS;
using System.Windows.Input;

namespace WPF.Cliente.VistaModelo.Nucleo
{
    public class VMContenedor : VMElemento, IContenedor
    {
        public VMContenedor()//VMElemento inicial)
        {
            //CargarElemento(inicial);
        }

        public void CargarElemento(VMElemento elemento)
        {
            if (elemento != null)
            {
                if (ElementoActual != null)
                    elemento.Padre = ElementoActual;

                ElementoActual = elemento;

                if (ElementoActual.Contenedor == null)
                    ElementoActual.Contenedor = this;

                Hijos.Add(elemento);
            }
        }

        public void DescargarElementoActual()
        {
            //El último elemento no se debería poder descargar.
            //Para eso es la validación de ElementoActual.Padre!=null
            if (ElementoActual != null && ElementoActual.Padre != null)
            {
                var elemento = ElementoActual;
                ElementoActual = ElementoActual.Padre;
                elemento.Dispose();
            }
        }

        public void CargarDialogo<T>(VMElemento dialogo, Action<T> accion)
        {
            if (dialogo != null)
            {
                if (DialogoActual != null)
                    dialogo.Padre = DialogoActual;

                DialogoActual = dialogo;

                if (DialogoActual.Contenedor == null)
                    DialogoActual.Contenedor = this;

                dialogo.EsDialogo = true;
                dialogo.AccionDialogo = accion.ConvertirAObject();

                //Hijos.Add(elemento);
            }
        }

        public void DescargarDialogoActual()
        {
            if (DialogoActual != null)
            {
                if (DialogoActual.Padre != null)
                {//Pasar a diálogo padre
                    var diag = DialogoActual;
                    DialogoActual = DialogoActual.Padre;
                    diag.Dispose();
                }
                else
                {//Descargar diálogo para regresar a la vista normal
                    DialogoActual.Dispose();
                    DialogoActual = null;
                }
            }
        }

        #region Propiedades
        ObservableCollection<VMElemento> _hijos;
        public virtual ObservableCollection<VMElemento> Hijos
        {
            get
            {
                return _hijos ?? (Hijos =
                    new ObservableCollection<VMElemento>());
            }
            private set
            {
                _hijos = value;
                if (_hijos != null)
                {
                    _hijos.CollectionChanged += (s, e) =>
                    {
                        if (e.Action == NotifyCollectionChangedAction.Add)
                        {
                            e.NewItems.AsListOf<VMElemento>().ForEach(i =>
                            {
                                if (i.Contenedor == null)
                                    i.Contenedor = this;
                            });
                        }
                    };
                }

                LevantarCambioPropiedad(() => Hijos);
            }
        }

        VMElemento _elementoActual;
        public virtual VMElemento ElementoActual
        {
            get { return _elementoActual; }
            private set
            {
                _elementoActual = value;
                LevantarCambioPropiedad(() => ElementoActual);
            }
        }


        private VMElemento _dialogoActual;
        public virtual VMElemento DialogoActual
        {
            get { return _dialogoActual; }
            private set
            {
                _dialogoActual = value;
                LevantarCambioPropiedad(() => DialogoActual);
            }
        }

        #endregion

        #region Overrides de VMElemento
        //protected override void CerrarVistaConResultado(ResultadoDialogo res)
        //{
        //    CerrarDialogo(res);            
        //}

        protected override void MostrarDialogo<T>(
            Vistas vista,
            VMBase modelo,
            Action<T> accion,
            bool modal = true,
            object receptor = null)
        {
            if (modelo is VMElemento)
                Contenedor.CargarDialogo<T>(modelo as VMElemento, accion);
            else
                base.MostrarDialogo<T>(vista, modelo, accion, modal, receptor);
        }

        protected override void MostrarVista(
            Vistas vista,
            VMBase modelo,
            bool modal = true,
            object receptor = null)
        {

            if (modelo is VMElemento)
                CargarElemento(modelo as VMElemento);
            else
                base.MostrarVista(vista, modelo, modal, receptor);
        }

        public override void InputRecibido(object sender, KeyEventArgs teclas)
        {
            if (DialogoActual != null)
                DialogoActual.InputRecibido(sender, teclas);
            else if (ElementoActual != null)
                ElementoActual.InputRecibido(sender, teclas);
            else
                base.InputRecibido(sender, teclas);
        }
        #endregion



    }
}
