using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Windows;


namespace WPF.Cliente.Util
{
    public static class ManejadorError
    {
        private static readonly string TituloPredeterminado;
        private static readonly string MensajePredeterminado;

        static ManejadorError()
        {
            //TODO: Cambiar esto a un archivo de recursos
            TituloPredeterminado = "Error";
            MensajePredeterminado = "Se capturó una excepción";
        }

        /// <summary>
        /// Muestra un mensaje de erorr
        /// </summary>
        /// <param name="mensaje">El mensaje que se desea mostrar.</param>
        public static void MostrarError(string mensaje)
        {
            MostrarInterfazError(mensaje, TituloPredeterminado, null);
        }

        /// <summary>
        /// Muestra un mensaje de error, construyéndolo a partir de la excepción.
        /// Permite establecer si se desea mostrar la traza del error.
        /// </summary>
        /// <param name="ex">La excepción que levantó el error</param>
        /// <param name="mensaje">El mensaje que se desea mostrar antes de la excepción.</param>
        /// <param name="mostrarTrazaError">Indica si se desea mostrar la traza del error.</param>
        public static void MostrarError(Exception ex, string mensaje = "", bool mostrarTrazaError = false)
        {
            if (string.IsNullOrWhiteSpace(mensaje)) mensaje = MensajePredeterminado;

            string traza = null;

            PrepararInfoExcepcion(ex, ref mensaje, ref traza, mostrarTrazaError);

            MostrarInterfazError(mensaje, TituloPredeterminado, traza);
        }

        private static void PrepararInfoExcepcion(Exception ex, ref string mensaje, ref string traza, bool mostrarTrazaError)
        {
            if (ex != null)
            {
                if (mostrarTrazaError)
                    traza = traza != null ? traza + "\n" + ObtenerTraza(ex) : ObtenerTraza(ex);

                mensaje += (ex != null ? ": " + ex.Message : ": Excepción nula.");

                if (ex.InnerException != null)
                    PrepararInfoExcepcion(ex.InnerException, ref mensaje, ref traza, mostrarTrazaError);
            }
        }

        public static string ObtenerTraza(Exception e)
        {
            string traza = null;

            if (e != null)
            {
                var strackTrace = new System.Diagnostics.StackTrace(e);

                var frames = strackTrace.GetFrames();

                if (frames != null)
                {
                    var sb = new StringBuilder();
                    foreach (var frame in frames)
                    {
                        sb.AppendFormat("{0}({1}): {2}()\n", frame.GetFileName(), frame.GetFileLineNumber(), frame.GetMethod().Name);
                    }

                    traza = sb.ToString();
                }
            }
            return traza;
        }

        private static void MostrarInterfazError(string mensaje, string titulo, string traza)
        {
            try
            {
                MessageBox.Show(
                    mensaje + "\n\n" + traza,
                    titulo,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error & MessageBoxImage.Exclamation);
            }
            catch (Exception) { }

        }
    }
}
