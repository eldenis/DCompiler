using System; using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF.Cliente.Util
{
    public class Booleano
    {
        public bool Valor { get; set; }
        private Booleano() : this(false) { }
        private Booleano(bool valor) { Valor = valor; }

        public static implicit operator Booleano(bool d)
        {
            return new Booleano(d);
        }

        public static explicit operator bool(Booleano valorBooleano)
        {
            return valorBooleano.Valor;
        }
    }
}
