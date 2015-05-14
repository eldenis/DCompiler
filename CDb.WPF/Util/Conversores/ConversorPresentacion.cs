
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Cliente.Util
{
    public class ConversorPresentacion : IValueConverter
    {
        //Se debe usar siempre en Mode=OneWay
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value != null && value is IDescripcionPresentacion)
            //{
            //    var dp = value as IDescripcionPresentacion;
            //    return dp.TieneValorPresentacion ? dp.DescripcionPresentacion : string.Empty;
            //}
            //else
            return value;
        }

        //Este método nunca debería ocurrir ya que el conversor es de sólo lectura.
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
