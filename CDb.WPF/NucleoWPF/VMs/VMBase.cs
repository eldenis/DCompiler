using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using WPF.Cliente;
using WPF.Cliente.Nucleo.MensajesApp;
using CDb.Transversal.Utilitarios;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF.Cliente.Nucleo;

namespace WPF.Cliente.VistaModelo.Nucleo
{
    /// <summary>
    /// Clase base para todas las clases ViewModel de la aplicación.
    /// Soporta las notificaciones de cambio de propiedad y las validaciones
    /// de ViewModel y de Entidades.
    /// </summary>
    public class VMBase : ObjetoCambioPropiedadValidado, IDisposable, IVMResultado
    {
        #region Eventos
        public delegate void ComandosCambioHandler(object sender, EventArgs e);
        private ComandosCambioHandler _comandosCambio;
        public event ComandosCambioHandler ComandosCambio
        {
            add
            {
                _comandosCambio += value;
                if (Comandos != null)
                    OnComandosCambio();
            }
            remove { _comandosCambio -= value; }
        }

        public void OnComandosCambio()
        {
            var handler = _comandosCambio;
            if (handler != null)
                _comandosCambio(this, new EventArgs());
        }
        #endregion

        #region Constructores
        public VMBase()
        {
            Comandos = new ColeccionComando<ComandoBase>();
            CargarComandos();
        }

        public VMBase(string nombre)
            : this()
        {
            _nombreFormulario = nombre ?? string.Empty;
        }
        #endregion

        #region Propiedades
        private readonly string _nombreFormulario;
        public string NombreFormulario { get { return _nombreFormulario; } }

        public DependencyObject ArbolObjetos { get; set; }

        public virtual Action ComandoCerrarVista { private get; set; }

        private ColeccionComando<ComandoBase> _comandos;
        public ColeccionComando<ComandoBase> Comandos
        {
            get { return _comandos; }
            private set
            {
                _comandos = value;
                OnComandosCambio();
                LevantarCambioPropiedad(() => Comandos);
            }
        }

        protected virtual bool ArbolValidado
        {
            get { return ArbolObjetos == null ? true : Validar(ArbolObjetos); }
        }

        public virtual ResultadoDialogo Resultado { get; protected set; }
        #endregion

        #region Comandos
        /// <summary>
        /// Cierra la ventana y retorna un resultado vacío
        /// </summary>
        private RelayCommand _cancelar;
        public virtual RelayCommand Cancelar
        {
            get
            {
                return _cancelar ?? (_cancelar =
                    new RelayCommand(() =>
                    {
                        Cerrar(new ResultadoDialogo(MessageBoxResult.Cancel));
                    }));
            }
        }
        #endregion

        #region Métodos privados
        private bool Validar(DependencyObject obj)
        {
            return !Validation.GetHasError(obj) &&
                LogicalTreeHelper.GetChildren(obj)
                .OfType<DependencyObject>().All(child => Validar(child));
        }
        #endregion

        #region Métodos Virtuales sin cuerpo
        public virtual void CargarComandos() { }
        #endregion

        #region Métodos Protected
        protected virtual void MostrarDialogo<T>(
           EVistas vista,
           VMBase modelo,
           Action<T> accion,
           bool modal = true,
           object receptor = null)
        {
            MensajeMostrarDialogo<T> msj =
                new MensajeMostrarDialogo<T>(this, vista, modelo, accion, modal, receptor);

            msj.Enviar();
        }

        protected virtual void MostrarVista(
            EVistas vista,
            VMBase modelo,
            bool modal = true,
            object receptor = null)
        {
            MensajeMostrarVista msj =
                new MensajeMostrarVista(this, vista, modelo, modal, receptor);

            msj.Enviar();
        }
        #endregion

        #region Métodos Públicos
        /// <summary>
        /// Cierra la vista actual retornando un resultado <see cref="ResultadoDialogo"/>
        /// </summary>
        /// <param name="res">El resultado del VM.</param>
        public virtual void Cerrar(ResultadoDialogo res)
        {
            Resultado = res;
            if (ComandoCerrarVista != null)
                ComandoCerrarVista();
        }

        public virtual void InputRecibido(object sender, KeyEventArgs teclas)
        {
            Comandos.UbicarComando(teclas);
        }
        #endregion

        #region Implementación de IDisposable

        /// <summary>        
        /// Se invoca cuando este objeto está siendo removido de la aplicación
        /// y será marcado para ser recolectado por el GC
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>        
        /// Las clases hijas pueden hacer override de este método
        /// para relizar lógica de limpiado, como eliminar los handlers de eventos.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Util para asegurarse de que los objetos del ViewModel están siendo recolectados por el GC     
        /// SOLO PARA DEBUG.
        /// </summary>
        ~VMBase()
        {
            string msg = string.Format("{0} ({1}) Finalizado", this.GetType().Name, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion // Implementación de IDisposable

    }
}
