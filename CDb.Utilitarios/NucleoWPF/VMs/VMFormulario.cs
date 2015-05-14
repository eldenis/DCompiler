using System.Windows;
using GalaSoft.MvvmLight.Command;
//using Infraestructura.Transversal.IoC;
//using Dominio.Nucleo;
//using Aplicacion.Nucleo;
using WPF.Cliente.Nucleo.MensajesApp;
using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
//using Dominio.Nucleo.Entidades;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.Nucleo;
using System.Windows.Input;
using System.ComponentModel;
using CDb.Transversal.Utilitarios.ObjetosPropios;
using WPF.Cliente.Util;
//using Infraestructura.Transversal.TIoC;


namespace WPF.Cliente.VistaModelo.Nucleo
{
    ///// <summary>
    ///// Clase abstracta que sirve de base para todos los ViewModels que 
    ///// utilicen CRUD de una entidad específica.
    ///// </summary>
    ///// <typeparam name="TServicio">El tipo de la interfaz del servicio de aplicación</typeparam>
    ///// <typeparam name="TEntidad">El tipo de la entidad</typeparam>
    //public abstract class VMFormulario<TServicio, TEntidad> : VMElemento
    //    where TServicio : IServicioEntidadBase<TEntidad>
    //    where TEntidad : EntidadBase
    //{

    //    #region Miembros Públicos
    //    public delegate void EntidadCambioHandler(object sender, EventArgs e);
    //    public delegate void ServicioCambioHandler(object sender, EventArgs e);

    //    private event EntidadCambioHandler _entidadCambio;
    //    public event EntidadCambioHandler EntidadCambio
    //    {
    //        add
    //        {
    //            _entidadCambio += value;
    //            /*Si es el primero que se agrega y la instancia ya ha sido construida
    //             * se levanta el evento.*/
    //            if (_entidadCambio.GetInvocationList().Length == 1 && InstanciaEntidad != null)
    //                OnEntidadCambio();
    //        }
    //        remove
    //        {
    //            _entidadCambio -= value;
    //        }
    //    }
    //    public event ServicioCambioHandler ServicioCambio;

    //    public delegate void IdEntidadCambioHandler(object sender, IdEntidadCambioEventArgs e);
    //    public delegate void IdEntidadCambiandoHandler(object sender, IdEntidadCambioEventArgs e);

    //    public event IdEntidadCambioHandler IdEntidadCambio;
    //    public event IdEntidadCambiandoHandler IdEntidadCambiando;

    //    public class IdEntidadCambioEventArgs : PropiedadCambiandoEventArgs
    //    {
    //        public bool CancelarCargaEntidad { get; set; }

    //        public IdEntidadCambioEventArgs(object valorActual, object valorNuevo)
    //            : base(valorActual, valorNuevo)
    //        {
    //            CancelarCargaEntidad = false;
    //        }
    //    }

    //    public void CargarEntidad(int id) { IdEntidad = id; }
    //    #endregion

    //    #region Constructores
    //    protected VMFormulario(string formName) :
    //        base(formName)
    //    {
    //        CargarServicio();

    //        ConstruirInstancia();
    //    }

    //    #endregion

    //    #region Métodos privados
    //    private void CargarEntidadBase()
    //    {
    //        InstanciaEntidad = ServicioMaestro.ObtenerRegistro<TEntidad>(IdEntidad);
    //    }

    //    private void ConstruirInstancia()
    //    {
    //        if (!typeof(TEntidad).IsAbstract)
    //            InstanciaEntidad = Activator.CreateInstance(typeof(TEntidad)) as TEntidad;
    //    }

    //    private void CargarServicio()
    //    {
    //        try { ServicioMaestro = IoCFactory.Instance.CurrentContainer.Resolve<TServicio>(); }
    //        catch (Exception)
    //        {
    //            try { ServicioMaestro = TransversalIoCFactory.Instance.CurrentContainer.Resolve<TServicio>(); }
    //            catch (Exception) { ManejadorError.MostrarError("No se pudo cargar el servicio: " + typeof(TServicio).Name); }
    //        }
    //    }
    //    #endregion

    //    #region Commandos


    //    private RelayCommand _guardar;
    //    public virtual RelayCommand Guardar
    //    {
    //        get
    //        {
    //            return _guardar ?? (_guardar = new RelayCommand(
    //                () =>
    //                {
    //                    CargarServicio();

    //                    if (NuevoRegistro)
    //                        ServicioMaestro.IncluirRegistro(InstanciaEntidad);
    //                    else
    //                        ServicioMaestro.ModificarRegistro(InstanciaEntidad);

    //                    Cerrar(new ResultadoDialogo(MessageBoxResult.OK));
    //                },
    //                () => { return EsValido && (InstanciaEntidad != null ? InstanciaEntidad.EsValido : true); }));
    //        }
    //    }


    //    #endregion

    //    #region Propiedades

    //    private int _idEntidad;
    //    protected virtual int IdEntidad
    //    {
    //        get
    //        {
    //            return _idEntidad;
    //        }
    //        private set
    //        {
    //            var e = new IdEntidadCambioEventArgs(_idEntidad, value);
    //            OnIdEntidadCambiando(e);
    //            if (!e.Cancelar)
    //            {
    //                _idEntidad = value;
    //                OnIdEntidadCambio(e);
    //                if (!e.CancelarCargaEntidad)
    //                    CargarEntidadBase();
    //            }
    //        }
    //    }
    //    private TServicio _servicioMaestro;
    //    protected TServicio ServicioMaestro
    //    {
    //        get { return _servicioMaestro; }
    //        private set
    //        {
    //            _servicioMaestro = value;
    //            OnServicioCambio();
    //        }
    //    }

    //    private TEntidad _instanciaEntidad;
    //    protected TEntidad InstanciaEntidad
    //    {
    //        get
    //        {
    //            return _instanciaEntidad;
    //        }
    //        set
    //        {
    //            if (value == null)
    //                throw new ArgumentException(Recursos.Mensajes.Generales.Error_ArgumentoInvalido);
    //            else
    //            {
    //                _instanciaEntidad = value;

    //                var estado = ((IObjectWithChangeTracker)_instanciaEntidad).ChangeTracker.State;

    //                if (estado != ObjectState.Added)
    //                    NuevoRegistro = false;

    //                OnEntidadCambio();

    //            }
    //        }
    //    }

    //    private bool _nuevoRegistro = true;
    //    public virtual bool NuevoRegistro
    //    {
    //        get
    //        {
    //            return _nuevoRegistro;
    //        }
    //        private set
    //        {
    //            _nuevoRegistro = value;
    //            LevantarCambioPropiedad(() => NuevoRegistro);
    //        }
    //    }
    //    #endregion

    //    #region Métodos para eventos

    //    private void OnIdEntidadCambio(IdEntidadCambioEventArgs e)
    //    {
    //        var handler = IdEntidadCambio;
    //        if (handler != null)
    //            handler(this, e);
    //    }

    //    private void OnIdEntidadCambiando(IdEntidadCambioEventArgs e)
    //    {
    //        var handler = IdEntidadCambiando;
    //        if (handler != null)
    //            handler(this, e);
    //    }

    //    private void OnServicioCambio()
    //    {
    //        var handler = ServicioCambio;
    //        if (handler != null)
    //            handler(this, new EventArgs());
    //    }

    //    private void OnEntidadCambio()
    //    {
    //        var handler = _entidadCambio;
    //        if (handler != null)
    //            handler(this, new EventArgs());
    //    }
    //    #endregion

    //    #region Overrides
    //    /*/// <summary>
    //    /// Método que oculta el método CerrarVista de la clase VMBase.
    //    /// Para que todos los VMEdicion que usen esta clase como base 
    //    /// se vean obligados a devolver un MessageBoxResult
    //    /// </summary>
    //    /// <param name="res">El resultado del formulario</param>
    //    protected new void CerrarVista(ResultadoDialogo res)
    //    {
    //        CerrarVistaConResultado(res);
    //    }*/



    //    public override void CargarComandos()
    //    {
    //        Comandos.Add(new ComandoBase(new KeyGesture(Key.F1))
    //        {
    //            Titulo = WPF.General.Recursos.UI.Generales.Guardar,
    //            Comando = Guardar
    //        });

    //        Comandos.Add(new ComandoBase(new KeyGesture(Key.Escape))
    //        {
    //            Titulo = WPF.General.Recursos.UI.Generales.Cancelar,
    //            Comando = Cancelar
    //        });

    //        base.CargarComandos();
    //    }
    //    #endregion
    //}

}
