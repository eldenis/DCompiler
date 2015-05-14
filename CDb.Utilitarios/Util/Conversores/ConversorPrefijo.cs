using System; using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Cliente.Util
{
    public class ConversorPrefijo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string valor = value as string;
                if (string.IsNullOrWhiteSpace(valor)) valor = value.ToString();

                if (string.IsNullOrEmpty(valor)) return string.Empty;

                if (parameter is string)
                {
                    string prefijo = parameter as string;
                    if (!string.IsNullOrEmpty(prefijo))
                    {
                        return string.Format("{0} {1}", prefijo, valor);
                    }
                }

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string valor = value as string;
                if (parameter is string)
                {
                    string prefijo = parameter as string;

                    if (!string.IsNullOrEmpty(prefijo) && string.IsNullOrEmpty(valor))
                        valor.Replace(prefijo, string.Empty);

                    return valor;
                }
            }

            return value;
        }
    }
}
