using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Cliente.Util
{

    public static class ManejadorFoco
    {
        public static bool GetTieneFoco(DependencyObject obj)
        {
            return (bool)obj.GetValue(TieneFocoProperty);
        }

        public static void SetTieneFoco(DependencyObject obj, bool value)
        {
            obj.SetValue(TieneFocoProperty, value);
        }

        public static readonly DependencyProperty TieneFocoProperty =
                DependencyProperty.RegisterAttached("TieneFoco", typeof(bool), typeof(ManejadorFoco),
                new UIPropertyMetadata(false, OnTieneFocoCambio));


        private static void OnTieneFocoCambio(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;

            if ((bool)e.NewValue && uie != null) uie.Focus();
        }
    }

}
