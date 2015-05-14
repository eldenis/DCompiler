using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Cliente.Util
{
    public class ConversorTexto : IValueConverter
    {
        public static Dictionary<PConversor, string> Formatos = LeerFormatos();


        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return string.Format(ObtenerFormato(value.GetType(), parameter as PConversor?), value);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }

        private string ObtenerFormato(Type tipo, PConversor? parametro)
        {
            if (parametro.HasValue)
                return Formatos[parametro.Value];
            else
            {
                try
                {
                    var valorEnum = Enum.Parse(typeof(PConversor),
                        string.Format("P{0}", tipo.Name), ignoreCase: true);

                    if (valorEnum != null)
                        return Formatos[(PConversor)valorEnum];
                }
                catch (Exception) { }
            }

            return "{0}";
        }


        private static Dictionary<PConversor, string> LeerFormatos()
        {
            var retval = new Dictionary<PConversor, string>();

            retval.Add(PConversor.PDinero, "{0:C2}");
            retval.Add(PConversor.Pdecimal, "{0:F2}");
            retval.Add(PConversor.PPorcentaje, "{0:P2}");
            retval.Add(PConversor.PDateTime, "{0:d}");
            retval.Add(PConversor.PFechaCorta, "{0:d}");
            retval.Add(PConversor.PFechaLarga, "{0:D}");

            return retval;
        }
    }



    public enum PConversor
    {
        PDinero = 1,
        Pdecimal = 2,
        PPorcentaje = 3,
        PDateTime = 4,
        PFechaCorta = 5,
        PFechaLarga = 6
    }
}
