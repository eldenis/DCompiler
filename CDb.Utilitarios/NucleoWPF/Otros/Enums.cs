using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Cliente.Util;
//using WPF.Cliente.Vista.Ventas.PoS;
//using WPF.Cliente.Vista.Bancos;
//using WPF.Cliente.Vista.Nomina;
//using WPF.Cliente.Vista.Finanzas;
//using WPF.Cliente.Vista.Seguridad;
//using WPF.Cliente.Vista.Generales;
//using WPF.Cliente.Vista.Inventario;
//using WPF.Cliente.Vista.Ventas;
//using WPF.Cliente.Vista.Compras;
//using WPF.Cliente.Vista.General;
using CDb.Transversal.Utilitarios;


namespace WPF.Cliente.Nucleo
{
    /// <summary>
    /// Enum que sirve para mantener todas las vistas que hay en el sistema.
    /// Esta Enum se usa para enviar los mensajes de cambio de vista.
    /// </summary>
    public enum Vistas
    {
        //#region Vistas Generales - De 0 a 99
        UserControl = -1,//Preguntar a Denis.


        Principal = 0

        //[ValorTipo(typeof(EdicionUsuario))]
        //SegEdicionUsuario = 901,
        //#endregion
    }

    //public enum NivelMenu
    //{
    //    Inventario = 100,
    //    Inventario_DatosMaestros = 101,
    //    Inventario_Procesos = 102,
    //    Inventario_Consultas = 103,
    //    Inventario_Reportes = 104,
    //    Inventario_Reportes_Existencias = 105,
    //    Inventario_Reportes_Articulos = 106,

    //    Compras = 300,
    //    Compras_DatosMaestros = 301,
    //    Compras_Procesos = 302,
    //    Compras_Consultas = 303,
    //    Compras_Reportes = 304,
    //    Compras_Reportes_OrdenDeCompra = 305,
    //    Compras_Reportes_Devoluciones = 306,

    //    Ventas = 200,
    //    Ventas_DatosMaestros = 201,
    //    Ventas_Procesos = 202,
    //    Ventas_Consultas = 203,
    //    Ventas_Reportes = 204,

    //    Bancos = 600,
    //    Bancos_DatosMaestros = 601,
    //    Bancos_Procesos = 602,
    //    Bancos_Consultas = 603,
    //    Bancos_Reportes = 604,

    //    Nomina = 700,
    //    Nomina_DatosMaestros = 701,
    //    Nomina_Procesos = 702,
    //    Nomina_Consultas = 703,
    //    Nomina_Reportes = 704,

    //    Finanzas = 800,
    //    Finanzas_DatosMaestros = 801,
    //    Finanzas_Procesos = 802,
    //    Finanzas_Consultas = 803,
    //    Finanzas_Reportes = 804,

    //    Configuracion = 900
    //}

    //public enum BusquedaArticulos
    //{
    //    Todos = 0,
    //    Componentes = 1,
    //    Usos = 2,
    //    Atributos = 3
    //}


}
