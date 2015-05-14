#region Usings
using System; using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using CDb.Transversal.Utilitarios;
using System.Windows.Controls;
#endregion

namespace WPF.Cliente.Util
{
    /// <summary>
    /// Conversor para SelectionMode.
    /// Con Bool, true es Extended, False es Single
    /// </summary>
    public class ConversorMetodoSeleccion : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var predeterminado = parameter != null && parameter is SelectionMode ? (SelectionMode)parameter : SelectionMode.Extended;

            if (value is bool)
                return !((bool)value) ? SelectionMode.Single : predeterminado;

            var retval = value != null ? SelectionMode.Single : predeterminado;

            return retval;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
