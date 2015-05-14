using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace WPF.Cliente.Util
{
    #region Miembros para los eventos
    public class EjecucionIniciadaEventArgs : EventArgs
    {
        public EjecucionIniciadaEventArgs(object valor) { Valor = valor; }

        public object Valor { get; private set; }
        public object Resultado { get; set; }
    }

    public delegate void EjecucionIniciadaHandler(object sender, EjecucionIniciadaEventArgs e);
    public delegate void ReporteProgresoHandler(object sender, ProgressChangedEventArgs e);
    public delegate void EjecucionTerminadaHandler(object sender, RunWorkerCompletedEventArgs e);
    #endregion

    public class EjecutorConRetraso : IDisposable
    {
        #region Lógica de eventos
        public event EjecucionIniciadaHandler EjecucionIniciada;
        public event ReporteProgresoHandler ReporteProgreso;
        public event EjecucionTerminadaHandler EjecucionTerminada;

        private void OnEjecucionIniciada(EjecucionIniciadaEventArgs argumento)
        {
            var handler = EjecucionIniciada;
            if (handler != null)
                handler(this, argumento);
        }

        private void OnReporteProgreso(ProgressChangedEventArgs e)
        {
            var handler = ReporteProgreso;
            if (handler != null)
                handler(this, e);
        }

        private void OnEjecucionTerminada(RunWorkerCompletedEventArgs e)
        {
            var handler = EjecucionTerminada;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        readonly TimeSpan _retraso;
        BackgroundWorker _worker;

        /// <summary>
        /// Crea un nuevo Ejecutor con un valor entero que indica el número
        /// de milisegundos que se deben esperar para iniciar la ejecución
        /// </summary>
        /// <param name="ms">Número de milisegundos</param>
        public EjecutorConRetraso(int ms) : this(new TimeSpan(0, 0, 0, 0, ms)) { }

        /// <summary>
        /// Crea un nuevo Ejecutor con un valor TimeSpan que indica el lapso
        /// de tiempo que se debe esperar para iniciar la ejecución
        /// </summary>
        /// <param name="ms">Lapso de tiempo a esperar</param>
        public EjecutorConRetraso(TimeSpan retraso) { _retraso = retraso; }

        public void IniciarEjecucion(object valor = null)
        {
            Action esperarRetraso = delegate { Thread.Sleep(_retraso); };

            esperarRetraso.BeginInvoke(delegate
            {
                if (!EsperandoCancelar)
                    ConstruirBackgroundWorker(valor);
                else
                    OnEjecucionTerminada(new RunWorkerCompletedEventArgs(null, null, true));
            }, null);
        }

        private void ConstruirBackgroundWorker(object valor)
        {
            _worker = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

            _worker.DoWork += (s, e) =>
            {
                var arg = new EjecucionIniciadaEventArgs(valor);
                if (!EsperandoCancelar)
                    OnEjecucionIniciada(arg);

                e.Result = arg.Resultado;

                if (EsperandoCancelar) { e.Cancel = true; }
            };

            _worker.ProgressChanged += (s, e) => { OnReporteProgreso(e); };

            _worker.RunWorkerCompleted += (s, e) => { OnEjecucionTerminada(e); };

            if (!EsperandoCancelar)
                _worker.RunWorkerAsync();
        }

        public void ReportarProgreso(int porcentaje, object valor = null)
        {
            if (_worker != null) _worker.ReportProgress(porcentaje, valor);
        }

        public bool EsperandoCancelar { get; private set; }

        public void CancelarEjecucion() { EsperandoCancelar = true; }

        public TimeSpan Retraso { get { return _retraso; } }

        public void Dispose()
        {
            //TODO: Definir qué hacer en el Dispose, ya que 
            //eliminar la referencia al BackgroundWorker no es una opción
        }
    }
}
