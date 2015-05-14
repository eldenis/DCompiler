using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.Nucleo;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.Nucleo.MensajesApp;
using System.Windows;
using System.Windows.Controls;
using CDb.Transversal.Utilitarios;

namespace WPF.Cliente.VistaModelo.Nucleo
{
    /// <summary>
    /// Un VM que sirve para los elementos de un IContenedor
    /// </summary>
    public class VMElemento : VMBase
    {
        #region Constructores
        public VMElemento(IContenedor contenedor = null,
           VMElemento padre = null)
            : this("", contenedor, padre) { }

        public VMElemento(string nombreFormulario, IContenedor contenedor = null,
            VMElemento padre = null)
            : base(nombreFormulario)
        {
            if (Contenedor == null) Contenedor = contenedor;
            if (Padre == null) Padre = padre;
            ComandosCambio += delegate { LevantarCambioPropiedad(() => ComandosElemento); };
        }
        #endregion

        #region Propiedades

        private VMElemento _padre;
        public VMElemento Padre
        {
            get { return _padre; }
            set
            {
                _padre = value;
                LevantarCambioPropiedad(() => Padre);
            }
        }

        private VMElemento _hijo;
        public VMElemento Hijo
        {
            get { return _hijo; }
            set
            {
                _hijo = value;
                LevantarCambioPropiedad(() => Hijo);
            }
        }

        private IContenedor _contenedor;
        public IContenedor Contenedor
        {
            get { return _contenedor; }
            set
            {
                _contenedor = value;
                LevantarCambioPropiedad(() => Contenedor);
            }
        }

        public ColeccionComando<ComandoBase> ComandosElemento
        {
            //get { return Dialogo == null ? Comandos : Dialogo.Comandos; }
            get { return Comandos; }
        }
        #endregion

        #region Dialogo
        public virtual bool EsDialogo { protected get; set; }
        public virtual Action<object> AccionDialogo { get; set; }

        private void CerrarDialogo(ResultadoDialogo res)
        {
            if (ArbolObjetos != null && ArbolObjetos.GetType().BaseType == typeof(Window))
            {
                base.Cerrar(res);
            }
            else
            {
                //Dialogo = null;
                Contenedor.DescargarDialogoActual();
                if (AccionDialogo != null)
                    AccionDialogo(res);
            }
        }

        //private VMElemento _dialogo;
        //public VMElemento Dialogo
        //{
        //    get { return _dialogo; }
        //    set
        //    {
        //        if (value == null && _dialogo != null)
        //            _dialogo.Dispose();
        //        _dialogo = value;
        //        LevantarCambioPropiedad(() => Dialogo);
        //        LevantarCambioPropiedad(() => ComandosElemento);
        //    }
        //}
        #endregion

        #region Overrides de VMBase

        protected override void MostrarDialogo<T>(
          EVistas vista,
          VMBase modelo,
          Action<T> accion,
          bool modal = true,
          object receptor = null)
        {
            if (vista == EVistas.UserControl || ValorTipoAttribute.ObtenerValorTipo(vista).BaseType == typeof(UserControl))
            {
                var dialogo = modelo as VMElemento;
                //dialogo.EsDialogo = true;
                //Contenedor.CargarDialogo(dialogo);
                //AccionDialogo = accion.ConvertirAObject();
                Contenedor.CargarDialogo<T>(dialogo, accion);

                //dialogo.Padre = this;
            }
            else
            {
                base.MostrarDialogo<T>(vista, modelo, accion, modal, receptor);
            }
            //Contenedor.MostrarDialogo<T>(vista, modelo, accion, modal, receptor);

        }

        protected override void MostrarVista(
           EVistas vista,
           VMBase modelo,
           bool modal = true,
           object receptor = null)
        {
            if (ValorTipoAttribute.ObtenerValorTipo(vista).BaseType == typeof(UserControl))
            {
                Contenedor.CargarElemento(modelo as VMElemento);
            }
            else
            {
                base.MostrarVista(vista, modelo, modal, receptor);
            }
        }

        public override void Cerrar(ResultadoDialogo res)
        {
            //TODO: Quitar este workaround una vez que todas las vistas estén como UserControl
            if (ArbolObjetos != null && ArbolObjetos.GetType().BaseType == typeof(Window))
            {
                base.Cerrar(res);
            }
            else
            {
                //if (EsDialogo)// && Padre != null)
                //    //Padre.CerrarDialogo(res);

                if (EsDialogo)
                {
                    Contenedor.DescargarDialogoActual();
                    if (AccionDialogo != null) AccionDialogo(res);
                }
                else
                    Contenedor.DescargarElementoActual();
            }
        }

        //public override void InputRecibido(object sender, System.Windows.Input.KeyEventArgs teclas)
        //{
        //    if (Dialogo != null)
        //        Dialogo.InputRecibido(sender, teclas);
        //    else//else if (!EsDialogo)
        //        base.InputRecibido(sender, teclas);
        //}
        #endregion
    }
}
