#region Usings
using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using CDb.Transversal.Utilitarios;
#endregion

namespace WPF.Cliente.Util
{
    public class ConversorVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //var predeterminado = parameter != null && parameter is Visibility ? (Visibility)parameter : Visibility.Collapsed;


            //if (value is bool)//Para booleano, debe ser true para verse, sino retorna al 
            //    return ((bool)value) ? Visibility.Visible : predeterminado;

            //if (value is ObjectState)//para objectstate, el state no debe ser deleted para verse
            //    return ((ObjectState)value) != ObjectState.Deleted ? Visibility.Visible : predeterminado;

            //if (value is string)//para cadena, la cadena debe tener un valor
            //    return !string.IsNullOrEmpty(((string)value)) ? Visibility.Visible : predeterminado;


            //if (value is Enum && parameter is Enum)//Para todos los enums, si son iguales retorna visible
            //    return Enum.Equals(value, parameter) ? Visibility.Visible : predeterminado;


            //Para todos los valores, si el valor no es null retorna visible
            var retval = value != null ? Visibility.Visible : Visibility.Collapsed;
            return retval;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }

}
