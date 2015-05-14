using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Transversal.Utilitarios
{
    public class RangoFechas : ObjetoCambioPropiedad
    {

        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set
            {
                _fechaInicio = value;
                LevantarCambioPropiedad(() => FechaInicio);
            }
        }


        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set
            {
                _fechaFin = value;
                LevantarCambioPropiedad(() => FechaFin);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}",
                FechaInicio.ToShortDateString(),
                FechaFin.ToShortDateString());
        }


    }
}
