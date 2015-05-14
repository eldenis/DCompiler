using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using WPF.Cliente.Nucleo;
using System.Windows;
using WPF.Cliente.VistaModelo;
using WPF.Cliente.VistaModelo.Nucleo;


namespace WPF.Cliente.Nucleo.MensajesApp
{
    //TODO: DENIS. Terminar comentarios
    #region enum TipoMensajes
    /// <summary>
    /// Enum que tiene los tipos de mensajes (Tokens) que se utilizarán en la aplicación.
    /// </summary>
    public enum TipoMensajes
    {
        //Tipo de Mensaje que se envía para mostrar una vista.
        MostrarVista = 1,
        //TIpo de Mensaje que se envía para mostrar un diálogo
        MostrarDialogo = 2,
        //Tipo de Mensaje que se envía para cerrar una vista
        CerrarVista = 3
    }
    #endregion //TipoMensajes

    #region Clase MensajeMostrarDialogo<T>

    /// <summary>
    /// Definición de clase contenedora (wrapper) para hacer 
    /// Register y Send de mensajes de Mostrar Dialogo.        
    /// </summary>
    public class MensajeMostrarDialogo<T> : MensajeMostrarVista
    {

        public MensajeMostrarDialogo(
            object emisor,
            EVistas vista,
            VMBase modelo,
            Action<T> accion,
            bool modal = true,
            object receptor = null)
            : base(emisor, vista, modelo, modal, receptor)
        {
            Accion = accion;
        }

        public Action<T> Accion { get; set; }

        #region Métodos

        public override void Enviar()
        {            
            Messenger.Default.Send<MensajeMostrarDialogo<T>>(this, TipoMensajes.MostrarDialogo);
        }

        public void EjecutarAccion(T resultado)
        {
            if (Accion != null) Accion.Invoke(resultado);
        }

        public static void Suscribirse(object receptor, Action<MensajeMostrarDialogo<T>> accion)
        {
            Messenger.Default.Register<MensajeMostrarDialogo<T>>(receptor, TipoMensajes.MostrarDialogo, accion);
        }

        #endregion //Métodos
    }
    #endregion //Clase MensajeMostrarDialogo<T>

    #region Clase ResultadoDialogo
    public class ResultadoDialogo
    {
        object[] _objetos;
        MessageBoxResult _resultado = MessageBoxResult.Cancel;

        /// <summary>
        /// Crea un nuevo objeto ResultadoDialogo
        /// </summary>
        /// <param name="resultado">El tipo de resultado que tuvo el diálogo</param>
        /// <param name="objetos">Una colección de objetos que se desean retornar</param>
        public ResultadoDialogo(MessageBoxResult resultado, params object[] objetos)
        {
            _resultado = resultado;
            _objetos = objetos;
        }

        public MessageBoxResult Resultado { get { return _resultado; } }

        /// <summary>
        /// Retorna el primer elemento conseguido de tipo T
        /// </summary>
        /// <typeparam name="T">El tipo del elemento esperado</typeparam>
        /// <param name="conExcepcion">Define si se lanza una excepción al no conseguir un objeto de tipo T.</param>
        /// <returns>El objeto de tipo T conseguido.</returns>
        public T ObtenerPrimero<T>()
        {
            try { return _objetos.OfType<T>().First(); }
            catch (Exception)
            {
                throw new ArgumentException
                        (string.Format("No se encontró ningún objeto de tipo {0}", typeof(T).Name));
            }
        }

        /// <summary>
        /// Retorna el primer objeto conseguido de tipo T o default(T) si no 
        /// se consigue ningún elemento de ese tipo
        /// </summary>
        /// <typeparam name="T">El tipo de objeto que se quiere conseguir</typeparam>
        /// <returns>El primer objeto de tipo T conseguido en la colección</returns>
        public T ObtenerPrimeroODefault<T>()
        {
            return _objetos.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Retorna un booleano indicando si hay al menos un valor de tipo T
        /// </summary>
        /// <typeparam name="T">El tipo del cual se desean buscar valores</typeparam>
        /// <returns>true si consigue al menos un elemento de tipo T</returns>
        public bool HayValores<T>() { return _objetos.Any(o => o is T); }

        /// <summary>
        /// Retorna un valor booleano indicando si hay o no valores en la colección
        /// </summary>
        /// <returns>true si consigue elementos en la colección</returns>
        public bool HayValores() { return _objetos.Any(); }

        /// <summary>
        /// Obtiene todos los objetos de tipo T de la colección de objetos pasados
        /// </summary>
        /// <typeparam name="T">El tipo de los elementos que se desean buscar</typeparam>
        /// <returns>Una lista con los elementos de tipo T conseguidos.</returns>
        public List<T> ObtenerTodos<T>() { return _objetos.OfType<T>().ToList(); }

        /// <summary>
        /// Obtiene todos los elementos que se pasen al objeto
        /// </summary>
        /// <returns>Una lista de tipo object que contiene todos los elementos pasados a la colección</returns>
        public List<object> ObtenerTodos() { return _objetos.ToList(); }
    }
    #endregion //ResultadoDialogo

    #region Clase MensajeMostrarVista

    /// <summary>
    /// Definición de clase contenedora (wrapper) para hacer 
    /// Register y Send de mensajes de Mostrar Dialogo.        
    /// </summary>
    public class MensajeMostrarVista
    {

        public MensajeMostrarVista(
            object emisor,
            EVistas vista,
            VMBase modelo,
            bool modal = false,
            object receptor = null)
        {
            Emisor = emisor;
            Vista = vista;
            Modelo = modelo;
            Modal = modal;
            Receptor = receptor;
        }

        public object Emisor { get; set; }
        public EVistas Vista { get; set; }
        public VMBase Modelo { get; set; }
        public bool Modal { get; set; }
        public object Receptor { get; set; }

        #region Métodos

        public virtual void Enviar()
        {
            Messenger.Default.Send<MensajeMostrarVista>(this, TipoMensajes.MostrarVista);
        }

        public static void Suscribirse(object receptor, Action<MensajeMostrarVista> accion)
        {
            Messenger.Default.Register<MensajeMostrarVista>(receptor, TipoMensajes.MostrarVista, accion);
        }

        #endregion //Métodos
    }

    #endregion //MensajeMostrarVista

    #region Clase MensajeCerrarVista
    /// <summary>
    /// Definición de clase contenedora (wrapper) para hacer 
    /// Register y Send de mensajes de Cerrar Vista.        
    /// </summary>
    public class MensajeCerrarVista
    {

        public MensajeCerrarVista(object emisor, EVistas vista, object token)
        {
            Emisor = emisor;
            Token = token;
            Vista = vista;
        }

        public object Emisor { get; set; }
        public object Token { get; set; }
        public EVistas Vista { get; set; }

        public virtual void Enviar()
        {
            Messenger.Default.Send<MensajeCerrarVista>(this, TipoMensajes.CerrarVista);
        }

        public static void Subscribirse(object suscriptor, TipoMensajes mensaje, Action<MensajeCerrarVista> accion)
        {
            Messenger.Default.Register<MensajeCerrarVista>(suscriptor, mensaje, accion);
        }
    }
    #endregion //MensajeCerrarVista

}
