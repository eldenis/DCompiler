using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDb.Compilacion
{
    internal static class Errores
    {
        internal const string NumeroIdentificadorComentariosInvalido = "El número de identificadores de comentarios es inválido.";
        internal const string TextoVacio = "El texto no puede estar vacío.";
        internal const string TablaSimbolosNoAsignada = "La tabla de símbolos no ha sido asignada. La compilación no puede proseguir.";
        internal const string PalabrasReservadasNoAsignadas = "Las palabras reservadas no han sido asignadas. La compilación no puede proseguir.";
        internal const string DiccionarioSimbolosVacio = "El diccionario de datos no puede estar vacío.";
        internal const string CaracterInvalido = "El caracter {0} no es válido. Posición: {1}";
        internal const string PalabraFaltante = "La palabra {0} falta en el editor.";
        internal const string SimboloRaizFaltante = "Falta el símbolo raíz en la expresión {0}.";
        internal const string DosOperadoresJuntos = "Se encontró el operador {0} junto al operador {1} en la expresión {2}.";
        internal const string DosLetrasNumerosJuntos = "Se encontró el símbolo '{0}' de tipo '{1}' junto al símbolo '{2}' de tipo '{3}' en la expresión {4}";
        internal const string IdentificadorNoDeclarado = "Se encontró el identificador (palabra) '{0}' no declarado en la expresión {1}";
        internal const string SeparadoresDesbalanceados = "El separador {0}{1} no está balanceado. Se encontraron {2} {3} y {4} {5}.";
        internal const string SimboloRaizMultipleVeces = "El símbolo raíz en la expresión {0} fue encontrado {1} veces";
        internal const string SeparadorInvalido = "Se encontró el separador {0} en una posición inválida en la expresión {1}";
        internal const string SeparadoresInvalidos = "Se encontraron separadores inválidos {0} y {1} en la expresión {2}";
        internal const string JerarquiaSeparadoresInvalida = "Error con la jerarquía de separadores";
    }
}
