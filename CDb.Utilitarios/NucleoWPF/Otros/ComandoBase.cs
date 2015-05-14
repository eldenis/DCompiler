using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Transversal.Utilitarios;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace WPF.Cliente.Nucleo
{
    public class ComandoBase : ObjetoCambioPropiedad
    {
        public ComandoBase()
        {
            AccesosDirectos = new ColeccionKeyGesture();
        }

        /// <summary>
        /// Inicia un nuevo ComandoBase tomando un acceso directo predeterminado
        /// </summary>
        /// <param name="acceso">Acceso directo predeterminado</param>
        public ComandoBase(KeyGesture acceso)
            : this()
        {
            if (acceso != null)
                AccesosDirectos.Add(acceso);
        }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                LevantarCambioPropiedad(() => Titulo);
            }
        }

        private RelayCommand _comando;
        public RelayCommand Comando
        {
            get { return _comando; }
            set
            {
                _comando = value;
                LevantarCambioPropiedad(() => Comando);
            }
        }


        private ColeccionKeyGesture _accesosDirectos;
        public ColeccionKeyGesture AccesosDirectos
        {
            get { return _accesosDirectos; }
            private set
            {
                _accesosDirectos = value;
                LevantarCambioPropiedad(() => AccesosDirectos);
                LevantarCambioPropiedad(() => CadenaAccesoDirecto);
            }
        }

        public string CadenaAccesoDirecto
        {
            get
            {
                if (AccesosDirectos != null && AccesosDirectos.Count > 0)
                    return AccesosDirectos.FirstOrDefault().DisplayString;

                return string.Empty;
            }
        }


    }
}
