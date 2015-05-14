using System;
//using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using CDb.Transversal.Utilitarios;
using System.Windows.Controls;
using System.Reflection;
using System.Threading;

namespace WPF.Cliente.Util
{
    public class EnumsWPF : IValueConverter
    {
        #region Enums para WPF

        public static void CargarEnumsEnCombo(Type tipoEnum, ComboBox combo)
        {
            var convertidor = new EnumsWPF();

            combo.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = ObtenerNombresEnum(tipoEnum)
            });

            var bindingAnterior = combo.GetBindingExpression(ComboBox.SelectedItemProperty);

            if (bindingAnterior != null)
            {


                combo.SetBinding(ComboBox.SelectedItemProperty,
                    new Binding(bindingAnterior.ParentBinding.Path.Path)
                    {
                        Converter = convertidor,
                        ConverterParameter = tipoEnum,
                        Mode = bindingAnterior.ParentBinding.Mode,
                        NotifyOnSourceUpdated = bindingAnterior.ParentBinding.NotifyOnSourceUpdated,
                        UpdateSourceTrigger = bindingAnterior.ParentBinding.UpdateSourceTrigger,
                        NotifyOnValidationError = bindingAnterior.ParentBinding.NotifyOnValidationError,
                        ValidatesOnDataErrors = bindingAnterior.ParentBinding.ValidatesOnDataErrors,
                        ValidatesOnExceptions = bindingAnterior.ParentBinding.ValidatesOnExceptions
                    });
            }
        }

        public static void CargarEnumsEnCombo(Type tipoEnum, DataGridComboBoxColumn fe)
        {
            var convertidor = new EnumsWPF();

            fe.ItemsSource = ObtenerNombresEnum(tipoEnum);

            BindingOperations.SetBinding(fe,
                DataGridComboBoxColumn.ItemsSourceProperty,
                new Binding() { Source = ObtenerNombresEnum(tipoEnum) });

            var bindingAnterior = (Binding)fe.SelectedItemBinding;

            if (bindingAnterior != null)
            {
                var bindingSelectedItem = new Binding(bindingAnterior.Path.Path)
                   {
                       Converter = convertidor,
                       ConverterParameter = tipoEnum,
                       Mode = bindingAnterior.Mode,
                       NotifyOnSourceUpdated = bindingAnterior.NotifyOnSourceUpdated,
                       UpdateSourceTrigger = bindingAnterior.UpdateSourceTrigger,
                       NotifyOnValidationError = bindingAnterior.NotifyOnValidationError,
                       ValidatesOnDataErrors = bindingAnterior.ValidatesOnDataErrors,
                       ValidatesOnExceptions = bindingAnterior.ValidatesOnExceptions
                   };
                fe.SelectedItemBinding = bindingSelectedItem;
            }
        }
        #endregion //Enums para WPF

        #region Métodos públicos para Enums

        static Dictionary<Type, Dictionary<string, string>> dicEnums =
            new Dictionary<Type, Dictionary<string, string>>();

        static readonly Type RecursoPredeterminado;// = typeof(Recursos.UI.Enums);

        public static string[] ObtenerNombresEnum(Type tipoEnum)
        {
            if (dicEnums.ContainsKey(tipoEnum))
            {
                return dicEnums[tipoEnum].Select(val => val.Value).ToArray();
            }
            else
            {
                var vals = Enum.GetNames(tipoEnum);

                var res = from nombre in vals
                          select new
                          {
                              Key = nombre,
                              Valor = ValorRecurso(RecursoPredeterminado, tipoEnum.Name + "_" + nombre)
                          };

                Dictionary<string, string> dicNuevo = res.ToDictionary(v => v.Key, v => v.Valor);

                dicEnums.Add(tipoEnum, dicNuevo);

                return dicNuevo.Select(val => val.Value).ToArray();
            }
        }

        public static Dictionary<string, string> ObtenerDiccionario(Type t)
        {
            return dicEnums[t];
        }

        public static string ValorRecurso(Type t, string nombre)
        {
            PropertyInfo p = t.GetProperty(nombre);

            var culture = Thread.CurrentThread.CurrentCulture;

            if (p != null)
            {
                try { return (string)p.GetValue(null, null); }
                catch (Exception) { }
            }

            return nombre;
        }

        public static string ValorReversoRecurso(Type tipoRecurso, string nombre)
        {
            PropertyInfo p = tipoRecurso.GetProperty(nombre);

            if (p != null)
            {
                try { return (string)p.GetValue(null, null); }
                catch (Exception) { }
            }

            return nombre;
        }

        #endregion Métodos públicos para Enums

        #region Implementación de IValueConverter

        public object Convert(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {

            Type tipo = parameter as Type;
            if (tipo != null && value != null)
            {
                int valorEntero = 0;
                if (int.TryParse(value.ToString(), out valorEntero) && valorEntero > 0)
                {
                    var valorEnum = Enum.GetName(tipo, value);
                    if (valorEnum != null)
                        value = valorEnum;
                }


                var valorReal = ValorReversoRecurso(RecursoPredeterminado, tipo.Name + "_" + value.ToString());
                if (!string.IsNullOrEmpty(valorReal))
                {
                    return valorReal;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Type tipoAConvertir = (Type)parameter;

                var dic = ObtenerDiccionario(tipoAConvertir);

                try
                {
                    var valorConvertido = dic.AsEnumerable()
                        .Where(e => e.Value == (string)value)
                        .Select(e => e.Key).Single();

                    var metodo = Utilitarios.ObtenerMethodInfo(() => Enum.Parse(tipoAConvertir, ""));

                    var resultado = metodo.Invoke(null, new object[] { tipoAConvertir, valorConvertido });

                    if (resultado != null)
                    {
                        return resultado;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Hubo un error al resolver el enum: " + e.Message);
                }

            }
            return value;
        }
        #endregion //Implementación de IValueConverter

    }
}

