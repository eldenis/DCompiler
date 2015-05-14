using System; using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WPF.Cliente.Nucleo.MensajesApp;

namespace WPF.Cliente.VistaModelo.Nucleo
{
    /// <summary>
    /// Interfaz que permite especificar si un VM trae un resultado
    /// de la operación realizada.
    /// </summary>
    public interface IVMResultado
    {
        ResultadoDialogo Resultado { get; }
    }
}
