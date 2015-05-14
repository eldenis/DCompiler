using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF.Cliente.Nucleo
{
    public interface IPoSInfoBox
    {
        object InfoBoxIzquierdoSuperior { get; }
        object InfoBoxIzquierdoInferior { get; }
        object InfoBoxDerecho { get; }
    }
}
