using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Transversal.Utilitarios;

namespace CDb.Compilacion
{
    public class CuentaCaracter
    {
        public CuentaCaracter()
        {
            TipoToken = TiposSimbolos.Ninguno;
        }

        public string NombreCaracter { get; internal set; }
        public char Caracter { get; internal set; }
        public int Cuenta { get; internal set; }
        public StringBuilder ListaCaracteres { get; internal set; }
        public string Caracteres { get; internal set; }
        public TiposSimbolos TipoToken { get; internal set; }
    }

    public class ResCompilacion
    {


        internal ResCompilacion() { }

        public string TextoInicial { get; internal set; }
        public string TextoSinComentarios { get; internal set; }
        public string TextoSinCaracteresEspaciado { get; internal set; }
        public List<CuentaCaracter> CuentasCaracteres { get; internal set; }
        public List<CuentaCaracter> CuentasTokens { get; internal set; }
        public List<Comentario> Comentarios { get; internal set; }
        public ResPalabras Palabras { get; internal set; }
    }
}
