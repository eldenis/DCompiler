using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.VistaModelo.Nucleo;
using CDb.Compilacion;

namespace CDb.WPF.VistaModelos
{
    public abstract class VMCompilacionBase : VMBase, IVMCompilacion
    {

        public abstract void CambioResultadoCompilacion();

        private ResCompilacion _resultadoCompilacion;
        public ResCompilacion ResultadoCompilacion
        {
            get
            {
                return _resultadoCompilacion;
            }
            set
            {
                _resultadoCompilacion = value;
                LevantarCambioPropiedad(() => ResultadoCompilacion);

                CambioResultadoCompilacion();
            }
        }

    }
}
