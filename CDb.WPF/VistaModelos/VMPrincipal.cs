using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.VistaModelo.Nucleo;
using GalaSoft.MvvmLight.Command;
using CDb.Compilacion;
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;
using CDb.Datos.DatosTableAdapters;
using Microsoft.Windows.Controls;

namespace CDb.WPF.VistaModelos
{
    public class VMPrincipal : VMBase
    {
        #region Constructores
        public VMPrincipal()
        {

            /*
             (U+2)-{F+[W/(A-C)+2]*J}*H=M/K^(F+2)*P
             {X+[P-K/(Y+4)]*J}^(U+F)-2=[(H*2)-P+5]*J/P^4
             */

//            TextoFuente = @"$Programa modelo para prueba de Compilador Db$
//            Prg CompiladorDbPrueba
//            Inip
//                Vent A
//                Vent U
//                Vent F
//                Vent W
//                Vent C
//                Vent J
//                Vent H
//                Vent K
//                Vent P
//                IEX
//                   (U+2)-{F+[W/(A-C)+2]*J}*H=M/K^(F+2)*P          
//                FEX
//            Finp";

//            TextoFuente = @"$Programa modelo para prueba de Compilador Db$
//            Prg CompiladorDbPrueba
//            Inip
//                Vent A
//                Vent X
//                Vent P
//                Vent K
//                Vent Y
//                Vent J
//                Vent U
//                Vent F
//                Vent H
//                IEX
//                   {X+[P-K/(Y+4)]*J}^(U+F)-2=[(H*2)-P+5]*J/P^4           
//                FEX
//            Finp";
        }
        #endregion

        #region Propiedades

        public string _textoFuente;
        public string TextoFuente
        {

            get { return _textoFuente; }
            set
            {
                _textoFuente = value;
                LevantarCambioPropiedad(() => TextoFuente);

                RealizarCompilacion();
            }
        }

        private VMAnalisisSintactico _analisisSintactico = new VMAnalisisSintactico();
        public VMAnalisisSintactico AnalisisSintactico
        {
            get { return _analisisSintactico; }
            set
            {
                _analisisSintactico = value;
                LevantarCambioPropiedad(() => AnalisisSintactico);
            }
        }

        private VMAnalisisLexico _analisisLexico = new VMAnalisisLexico();
        public VMAnalisisLexico AnalisisLexico
        {
            get { return _analisisLexico; }
            set
            {
                _analisisLexico = value;
                LevantarCambioPropiedad(() => AnalisisLexico);
            }
        }

        private string _archivoAbierto;
        public string ArchivoAbierto
        {
            get { return _archivoAbierto; }
            set
            {
                _archivoAbierto = value;
                TituloVentana = "Compilador Db - " + _archivoAbierto;
                LevantarCambioPropiedad(() => ArchivoAbierto);
            }
        }

        private string _tituloVentana = "Compilador Db";
        public string TituloVentana
        {
            get { return _tituloVentana; }
            set
            {
                _tituloVentana = value;
                LevantarCambioPropiedad(() => TituloVentana);
            }
        }

        private string _ultimoError = null;
        public string UltimoError
        {
            get { return _ultimoError; }
            set
            {
                _ultimoError = value;
                LevantarCambioPropiedad(() => UltimoError);
            }
        }

        private bool compilacionExitosa;
        public bool CompilacionExitosa
        {
            get { return compilacionExitosa; }
            set
            {
                compilacionExitosa = value;
                LevantarCambioPropiedad(() => CompilacionExitosa);
            }
        }

        private NodoRaiz _nodoRaiz;
        public NodoRaiz NodoRaiz
        {
            get { return _nodoRaiz; }
            set
            {
                _nodoRaiz = value;
                LevantarCambioPropiedad(() => NodoRaiz);
            }
        }


        #endregion

        #region Métodos privados
        private void RealizarCompilacion()
        {
            string error = string.Empty;
            ResCompilacion res = null;

            try { res = CompiladorDb.Compilar(TextoFuente); }
            catch (Exception e) { error = e.Message; }
            finally
            {
                CompilacionExitosa = res != null;
                UltimoError = error;
                AnalisisLexico.ResultadoCompilacion = res;
                AnalisisSintactico.ResultadoCompilacion = res;

                var exMat = res != null ? res.Palabras.ExpresionesMatematicas.First() : null;
                NodoRaiz = exMat != null ? exMat.Raiz : null;

            }
        }

        #endregion

        #region Comandos
        RelayCommand _abrirArchivo;
        public RelayCommand AbrirArchivo
        {
            get
            {
                return (_abrirArchivo ?? (_abrirArchivo = new RelayCommand(() =>
                {
                    OpenFileDialog ofd = new OpenFileDialog { DefaultExt = "txt", AddExtension = true, CheckFileExists = true, Filter = "Archivos fuentes | *.txt" };
                    var res = ofd.ShowDialog();
                    if (res.HasValue && res == true && !string.IsNullOrWhiteSpace(ofd.FileName))
                    {
                        TextoFuente = File.ReadAllText(ofd.FileName);
                        ArchivoAbierto = ofd.FileName;
                    }
                })));
            }
        }

        RelayCommand _guardarArchivo;
        public RelayCommand GuardarArchivo
        {
            get
            {
                return (_guardarArchivo ?? (_guardarArchivo = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(ArchivoAbierto))
                        GuardarArchivoComo.Execute(null);
                    else
                        File.WriteAllText(ArchivoAbierto, TextoFuente);
                })));
            }
        }



        RelayCommand _guardarArchivoComo;
        public RelayCommand GuardarArchivoComo
        {
            get
            {
                return (_guardarArchivoComo ?? (_guardarArchivoComo = new RelayCommand(() =>
                {
                    SaveFileDialog sfd = new SaveFileDialog { DefaultExt = "txt", AddExtension = true, Filter = "Archivos fuentes | *.txt" };

                    var res = sfd.ShowDialog();
                    if (res.HasValue && res == true && !string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        File.WriteAllText(sfd.FileName, TextoFuente);
                        ArchivoAbierto = sfd.FileName;
                    }
                })));
            }
        }

        #endregion
    }
}
