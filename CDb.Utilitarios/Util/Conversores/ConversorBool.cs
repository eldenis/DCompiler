using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace WPF.Cliente.Util
{
    public class ConversorBool : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //var predeterminado = parameter != null && parameter is Visibility ? (Visibility)parameter : Visibility.Collapsed;
            bool negado = false;
            if (parameter is string)
            {
                try
                {
                    var valorParametro = int.Parse(parameter as string);
                    negado = valorParametro != 0;
                }
                catch (Exception) { }
            }

            //Para todos los valores, si el valor no es null retorna visible
            return (value == null) == negado; //? Visibility.Visible : Visibility.Collapsed;
            //return retval;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //return (Visibility)value == Visibility.Visible ? true : false;
            return value;
        }
    }
}
