using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Controls;

namespace WPF.Cliente.Util
{
    public class InstaladorConversor : DependencyObject
    {
        /// <summary>
        /// Nombre de la Dependency Property
        /// </summary>
        private const string DpConversorTexto = "ConversorTexto";



        public static IValueConverter GetConversorTexto(DependencyObject obj)
        {
            return (IValueConverter)obj.GetValue(ConversorTextoProperty);
        }

        public static void SetConversorTexto(DependencyObject obj, IValueConverter value)
        {
            obj.SetValue(ConversorTextoProperty, value);
        }

        public static readonly DependencyProperty ConversorTextoProperty =
            DependencyProperty.RegisterAttached(DpConversorTexto,
            typeof(IValueConverter), typeof(InstaladorConversor), new PropertyMetadata
            {
                PropertyChangedCallback = (obj, e) =>
                {
                    var box = (TextBox)obj;
                    box.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        var binding = BindingOperations.GetBinding(box, TextBox.TextProperty);
                        if (binding == null) return;

                        var newBinding = new Binding
                        {
                            Converter = GetConversorTexto(box),

                            ConverterParameter = binding.ConverterParameter,
                            Path = binding.Path,
                            Mode = binding.Mode,
                            UpdateSourceTrigger = binding.UpdateSourceTrigger,
                            NotifyOnValidationError = binding.NotifyOnValidationError,
                            StringFormat = binding.StringFormat
                        };
                        if (binding.Source != null) newBinding.Source = binding.Source;
                        if (binding.RelativeSource != null) newBinding.RelativeSource = binding.RelativeSource;
                        if (binding.ElementName != null) newBinding.ElementName = binding.ElementName;

                        BindingOperations.SetBinding(box, TextBox.TextProperty, newBinding);
                    }));
                }
            });
    }
}
