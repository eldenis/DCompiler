using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CDb.WPF.Vistas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VPrincipal : Window
    {
        public VPrincipal()
        {
            //TextBox t;
            //t.
            InitializeComponent();
        }


        //public int CursorPosition
        //{
        //    get { return (int)GetValue(CursorPositionProperty); }
        //    set { SetValue(CursorPositionProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CursorPosition.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CursorPositionProperty =
        //    DependencyProperty.Register(
        //        "CursorPosition",
        //        typeof(int),
        //        typeof(VPrincipal),
        //        new UIPropertyMetadata(
        //            0,
        //            (o, e) =>
        //            {
        //                if (e.NewValue != e.OldValue)
        //                {
        //                    VPrincipal t = (VPrincipal)o;
        //                    t.tbxFuente.CaretIndex = (int)e.NewValue;
        //                }
        //            }));

        //private void tbxFuente_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    this.SetValue(CursorPositionProperty, tbxFuente.CaretIndex);
        //}

    }
}
