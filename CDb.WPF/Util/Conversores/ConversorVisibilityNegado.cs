using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace WPF.Cliente.Util
{

    public class ConversorVisibilityNegado : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var predeterminado = parameter != null && parameter is Visibility ? (Visibility)parameter : Visibility.Collapsed;

            //Para todos los valores, si el valor no es null retorna visible
            var retval = value == null ? Visibility.Visible : Visibility.Collapsed;
            return retval;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}
