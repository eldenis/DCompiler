using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Transversal.Utilitarios;

namespace WPF.Cliente.VistaModelo.Generales
{
    public class OpcionesListaGenerica : ObjetoCambioPropiedad
    {
        private bool _agregarActivo = true;
        public bool AgregarActivo
        {
            get { return _agregarActivo; }
            set
            {
                _agregarActivo = value;
                LevantarCambioPropiedad(() => AgregarActivo);
            }
        }

        private bool _esSeleccionador = false;
        public bool EsSeleccionador
        {
            get { return _esSeleccionador; }
            set
            {
                _esSeleccionador = value;
                LevantarCambioPropiedad(() => EsSeleccionador);
            }
        }

        private bool _seleccionMultiple = false;
        public bool SeleccionMultiple
        {
            get { return _seleccionMultiple; }
            set
            {
                _seleccionMultiple = value;
                LevantarCambioPropiedad(() => SeleccionMultiple);
            }
        }

        private bool _retornaEntidades = true;
        public bool RetornaEntidades
        {
            get { return _retornaEntidades; }
            set
            {
                _retornaEntidades = value;
                LevantarCambioPropiedad(() => RetornaEntidades);
            }
        }


        private bool _eliminarActivo = true;
        public bool EliminarActivo
        {
            get { return _eliminarActivo; }
            set
            {
                _eliminarActivo = value;
                LevantarCambioPropiedad(() => EliminarActivo);
            }
        }


        private bool _retornaObjetosAnonimos;
        public bool RetornaObjetosAnonimos
        {
            get { return _retornaObjetosAnonimos; }
            set
            {
                _retornaObjetosAnonimos = value;
                LevantarCambioPropiedad(() => RetornaObjetosAnonimos);
            }
        }



    }
}
