using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using CDb.Compilacion;
using System.Windows.Threading;
using WPF.Cliente.Util;
using System.Globalization;
using WPF.Cliente.Nucleo.MensajesApp;
using System.Windows.Markup;
using System.Threading;
using WPF.Cliente.VistaModelo.Nucleo;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.Nucleo;
using CDb.WPF.VistaModelos;
using DatosCompilacion = CDb.Datos.Datos;

namespace CDb.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string CadenaLocalizacionPredeterminada = "es-VE";

        public static readonly CultureInfo LocalizacionPredeterminada = new CultureInfo(CadenaLocalizacionPredeterminada);

        public static Window Principal { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            //Carga la localización de la aplicación.
            CargarLocalizacion(LocalizacionPredeterminada);

            //Crea un evento manejador de excepciones no capturadas
            CrearManejadorExcepciones();

            //Se suscribe a todos los mensajes de aplicación
            SuscribirseMensajes();

            //Carga la tabla de símbolos de la base de datos
            CargarTablaSimbolos();

            //Carga la tabla de palabras de la base de datos
            CargarPalabrasReservadas();

            //Cargar primera vista
            (new MensajeMostrarVista(null, EVistas.Principal, new VMPrincipal())).Enviar();
        }

        #region Otros Métodos

        private void CargarTablaSimbolos()
        {
            var tablaSimbolos = DatosCompilacion.ObtenerTablaSimbolos();
            CompiladorDb.EstablecerTablaSimbolos(tablaSimbolos);
        }

        private void CargarPalabrasReservadas()
        {
            var tablaPalabras = DatosCompilacion.ObtenerPalabrasReservadas();
            CompiladorDb.EstablecerPalabrasReservadas(tablaPalabras);
        }

        /// <summary>
        /// Método que crea un Dispatcher para las excepciones no manejadas
        /// de toda la aplicación. Cualquier excepción que ocurra sin ser
        /// manejada llega hasta este punto
        /// </summary>
        private void CrearManejadorExcepciones()
        {
            //Agrega un handler al evento DispatcherUnhandledException, 
            //de esa manera cada vez que ocurra una excepción que no se 
            //manejó llegué hasta el método CapturarExcepcionNoManejada()
            DispatcherUnhandledException +=
            new DispatcherUnhandledExceptionEventHandler((sender, e) =>
            {
                e.Handled = true;//Definir si se manejarán los errores desde aquí

                //2011-06-27 Denis González
                //TODO: Cambiar estos mensajes acordes al evento y a la localización
                Exception exc = e.Exception;

                ManejadorError.MostrarError(exc, mostrarTrazaError: true);
            });
        }

        /// <summary>
        /// Carga la localización especificada.
        /// </summary>
        /// <param name="localizacion">El CultureInfo con información de la localización</param>
        private void CargarLocalizacion(CultureInfo localizacion)
        {
            //TODO: Cargar cultura de archivo de configuración o de BD.            
            Thread.CurrentThread.CurrentCulture = localizacion;
            Thread.CurrentThread.CurrentUICulture = localizacion;

            //TODO: Acomodar este WORKAROUND. Sirve para que los elementos de la interfaz
            //tomen el cambio de cultura.
            //http://stackoverflow.com/questions/4041197/how-to-set-and-change-the-culture-in-wpf
            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
            XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag)));
        }

        /// <summary>
        /// Método que permite al obj App suscribirse a los mensajes de aplicación
        /// </summary>
        private void SuscribirseMensajes()
        {
            MensajeMostrarVista.Suscribirse(this, OnMensajeMostrarVista);
            MensajeMostrarDialogo<ResultadoDialogo>.Suscribirse(this, OnMensajeMostrarDialogo);
        }

        /// <summary>
        /// /// Al recibir un mensaje de MostrarVista se ejecuta este evento
        /// </summary>
        /// <param name="args">Los args para el método.</param>
        private void OnMensajeMostrarVista(MensajeMostrarVista args)
        {
            /*if (args.Receptor != null && args.Receptor is IContenedor)
            {
                //(args.Receptor as IContenedor).CargarElemento(args.Modelo);
                return;
            }*/

            Window vista = CargarVista(args) as Window;
            if (Principal == null) Principal = vista;
            if (args != null)
            {
                if (args.Emisor is VMBase)
                {
                    if ((args.Emisor as VMBase).ArbolObjetos is Window)
                    {
                        vista.Owner = (args.Emisor as VMBase).ArbolObjetos as Window;
                    }
                }
            }

            //if (vista.Owner == null && vista != Principal) vista.Owner = Principal;

            if (vista != null)
                if (args.Modal && vista.Owner != null) vista.ShowDialog(); else vista.Show();
            else
                throw new Exception("Vista no encontrada");
        }

        /// <summary>
        /// Al recibir un mensaje de MostrarDialogo se ejecuta este evento
        /// </summary>
        /// <param name="args">Los args para el método.</param>
        private void OnMensajeMostrarDialogo(MensajeMostrarDialogo<ResultadoDialogo> args)
        {
            Window vista = CargarVista(args) as Window;

            if (args.Emisor != null)
            {
                if (args.Emisor is VMBase)
                {
                    if ((args.Emisor as VMBase).ArbolObjetos is Window)
                    {
                        vista.Owner = (args.Emisor as VMBase).ArbolObjetos as Window;
                    }
                }
            }

            //if (vista.Owner == null && vista != Principal) vista.Owner = Principal;

            if (vista != null)
            {
                if (args.Modal && vista.Owner != null)
                {
                    vista.ShowDialog();
                    if (vista.DataContext != null && args.Accion != null)
                    {
                        ResultadoDialogo resultado = null;
                        IVMResultado iRes = vista.DataContext as IVMResultado;

                        if (iRes != null && iRes.Resultado != null) resultado = iRes.Resultado;
                        else resultado = new ResultadoDialogo(MessageBoxResult.None);

                        args.EjecutarAccion(resultado);
                    }
                }
                else vista.Show();

            }
        }

        /// <summary>
        /// Método que permite cargar una vista utilizando los parámetros
        /// de una instancia de MensajeMostrarVista
        /// </summary>
        /// <param name="args">Los argumentos para mostrar la vista</param>
        /// <returns>La ventana construida.</returns>
        private Window CargarVista(MensajeMostrarVista args)
        {
            Type tipoVista = ValorTipoAttribute.ObtenerValorTipo(args.Vista);

            if (tipoVista == null)
                throw new Exception("Vista no encontrada");


            Window vista = (Window)Activator.CreateInstance(tipoVista);

            if (vista == null)
                throw new Exception("Vista no encontrada: " + tipoVista);
            else
            {
                MensajeCerrarVista msjCerrar = new MensajeCerrarVista(
                      this, args.Vista, new Random().Next());


                MensajeCerrarVista.Subscribirse(vista, TipoMensajes.CerrarVista,
                   mensaje =>
                   {
                       if (mensaje.Token == msjCerrar.Token)
                           vista.Close();
                   });


                if (args.Modelo != null)
                {
                    args.Modelo.ComandoCerrarVista = () => { msjCerrar.Enviar(); };

                    vista.PreviewKeyDown += (s, e) => { args.Modelo.InputRecibido(s, e); };

                    //vista.KeyDown += (s, e) => { args.Modelo.InputRecibido(s, e); };

                    vista.DataContext = args.Modelo;

                    args.Modelo.ArbolObjetos = vista;
                }

                return vista;
            }
        }
        #endregion

    }
}
