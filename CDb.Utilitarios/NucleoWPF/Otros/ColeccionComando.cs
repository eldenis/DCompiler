using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CDb.Transversal.Utilitarios;

namespace WPF.Cliente.Nucleo
{
    public class ColeccionComando<TComando> :
        ObservableCollection<TComando> where TComando : ComandoBase
    {

        public void UbicarComando(KeyEventArgs gesto)
        {
            this.Where(comando => comando.AccesosDirectos.Any(c => c.Matches(null, gesto)))
                 .ForEach(comando =>
                 {
                     if (comando.Comando.CanExecute(null))
                         comando.Comando.Execute(null);
                 });
        }

    }
}
