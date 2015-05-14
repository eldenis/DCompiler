using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Transversal.Utilitarios;

namespace CDb.Compilacion
{
    public static class CompiladorDb
    {
        #region Miembros Privados
        private static TablaSimbolos _simbolos = new TablaSimbolos();
        private static TablaPalabras _palabras = new TablaPalabras();
        private static Tuple<Palabra, Palabra> _palabrasExMatematica;
        private static readonly Dictionary<char, string> _caracteresEspaciado = new Dictionary<char, string> {
            {'\n',"Nueva Línea"}, {'\t' ,"Tabulación"}, {'\r',"Retorno de carro"}, {' ',"Espacio en blanco"}
        };
        private static char _caracterComentario;
        #endregion

        public static void EstablecerTablaSimbolos(Dictionary<TiposSimbolos, string> diccionario)
        {
            if (diccionario == null)
                throw new ExCompilacion(Errores.DiccionarioSimbolosVacio);

            if (_simbolos.Count > 0) _simbolos.Clear();

            foreach (var simbolo in diccionario)
                _simbolos.Add(new Simbolo(simbolo.Key, simbolo.Value));

            var simboloComentario = _simbolos.Find(p => p.TipoSimbolo == TiposSimbolos.Comentario);

            _caracterComentario = simboloComentario._regex.EliminarCaracteresEscape()[0];
        }

        public static void EstablecerPalabrasReservadas(List<Palabra> lista)
        {
            _palabras.Clear();
            _palabras.AddRange(lista.ToDictionary(l => l.Valor));

            //TODO: QUITAR ESTO DE AQUI TAN PRONTO SE ACABE EL CURSO
            _palabrasExMatematica = new Tuple<Palabra, Palabra>(
                _palabras["IEX"], _palabras["FEX"]);
        }

        public static ResCompilacion Compilar(string texto)
        {
            if (_simbolos.Count == 0)
                throw new ExCompilacion(Errores.TablaSimbolosNoAsignada);

            if (_palabras.Count == 0)
                throw new ExCompilacion(Errores.PalabrasReservadasNoAsignadas);

            if (texto == null || string.IsNullOrWhiteSpace(texto))
                throw new ExCompilacion(Errores.TextoVacio);

            var res = new ResCompilacion
            {
                TextoInicial = texto,
                //Preparar cuenta caracteres
                CuentasCaracteres = _caracteresEspaciado.Select(caracter => new CuentaCaracter
                {
                    Caracter = caracter.Key,
                    Cuenta = 0,
                    NombreCaracter = caracter.Value
                }).ToList(),
                //Preparar cuenta tokens
                CuentasTokens = _simbolos.Where(s => s.TipoSimbolo != TiposSimbolos.Comentario).Select(simbolo => new CuentaCaracter
                {
                    Cuenta = 0,
                    NombreCaracter = simbolo.TipoSimbolo.ToString(),
                    ListaCaracteres = new StringBuilder(),
                    TipoToken = simbolo.TipoSimbolo
                }).ToList()
            };

            #region Proceso de compilación
            //Extracción de comentarios
            res.Comentarios = AnalizadorLexico.ExtraerComentarios(res.TextoInicial, _caracterComentario);

            //Eliminación de comentarios
            res.TextoSinComentarios = AnalizadorLexico.EliminarComentarios(res.TextoInicial, res.Comentarios);

            //Eliminación de caracteres de espacio
            res.TextoSinCaracteresEspaciado = AnalizadorLexico
                .EliminarCaracteresEspaciado(res.TextoSinComentarios, res.CuentasCaracteres);

            AnalizadorLexico.RealizarClasificacionTokens(res.TextoSinCaracteresEspaciado, res.CuentasTokens, _simbolos);
            
            res.CuentasTokens.ForEach(tk => tk.Caracteres = tk.ListaCaracteres.ToString());

            res.Palabras = AnalizadorSintactico.ExtraerPalabras(res.TextoSinComentarios,
                    _palabras, _simbolos, _caracteresEspaciado.Select(c => c.Key).ToArray(), _palabrasExMatematica);


            #endregion

            return res;
        }


    }
}
