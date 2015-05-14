using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace WPF.Cliente.Util
{
    public class ConversorAlineacion : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
         System.Globalization.CultureInfo culture)
        {
            if (value != null && value is TextBox)
            {
                var tb = value as TextBox;

                BindingExpression binding = tb.GetBindingExpression(TextBox.TextProperty);

                if (binding != null && binding.DataItem != null)
                {
                    var prop = binding.GetType().GetProperty("SourceValue", BindingFlags.NonPublic | BindingFlags.Instance);
                    var valor = prop.GetValue(binding, null);
                    switch (valor.GetType().Name.ToLower())
                    {
                        case "decimal":
                        case "Int32":
                            return HorizontalAlignment.Right;
                    }
                }
            }

            return HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }
}
