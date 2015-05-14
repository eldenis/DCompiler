using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.VistaModelo.Nucleo;
using System.Collections.ObjectModel;
using CDb.Compilacion;

namespace CDb.WPF.VistaModelos
{
    public class VMAnalisisLexico : VMCompilacionBase
    {

        public override void CambioResultadoCompilacion()
        {
            if (ResultadoCompilacion != null)
            {
                Comentarios = new ObservableCollection<Comentario>(ResultadoCompilacion.Comentarios);
            }
        }

        public ObservableCollection<Comentario> _comentarios;
        public ObservableCollection<Comentario> Comentarios
        {

            get { return _comentarios; }
            set
            {
                _comentarios = value;
                LevantarCambioPropiedad(() => Comentarios);
            }
        }
    }
}
