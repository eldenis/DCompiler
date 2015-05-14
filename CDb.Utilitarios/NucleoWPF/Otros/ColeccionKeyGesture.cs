using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace WPF.Cliente.Nucleo
{
    public class ColeccionKeyGesture : ObservableCollection<KeyGesture>
    {

        public new void Add(KeyGesture item)
        {
            //TODO: Cambiar la cultura, leyéndola del archivo XML de configuración
            if (string.IsNullOrWhiteSpace(item.DisplayString))
                item = new KeyGesture(item.Key, item.Modifiers,
                    item.GetDisplayStringForCulture(new CultureInfo("es-VE")));

            base.Add(item);
        }
    }
}
