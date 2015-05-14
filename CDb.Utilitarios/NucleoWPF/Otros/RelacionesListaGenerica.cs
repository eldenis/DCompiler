using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Dominio.Operacion.Entidades;
//using Aplicacion.Operacion.Bancos;
//using WPF.Cliente.VistaModelo.Bancos;
//using WPF.Cliente.VistaModelo.Compras;
//using WPF.Cliente.VistaModelo.Ventas;
//using WPF.Cliente.VistaModelo.Inventario;
//using WPF.Cliente.VistaModelo.Nomina;
//using Aplicacion.Operacion.Bancos;
//using Aplicacion.Operacion.Compras;
//using Aplicacion.Operacion.Ventas;
//using Aplicacion.Operacion.Inventario;
//using Aplicacion.Operacion.Nomina;
//using Aplicacion.Operacion.Finanzas;
//using WPF.Cliente.VistaModelo.Finanzas;
//using Dominio.Transversal.Entidades;
//using Aplicacion.Transversal.Seguridad;
//using WPF.Cliente.VistaModelo.Seguridad;
//using WPF.General;

namespace WPF.Cliente.Nucleo
{
    //public static class RelacionesListaGenerica
    //{
    //    public struct DatosEntidades
    //    {
    //        public Type ServicioMaestro;
    //        public string Nombre;
    //        public Type VMEdicion;
    //        public Vistas Vista;
    //    }
    //    public static Dictionary<Type, DatosEntidades> Relaciones { get; private set; }

    //    static RelacionesListaGenerica()
    //    {
    //        Relaciones = new Dictionary<Type, DatosEntidades>();

    //        #region General
    //        Relaciones.Add(typeof(SegUsuario),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoUsuarios),
    //                Nombre = WPF.General.Recursos.UI.Seguridad.Usuario,
    //                VMEdicion = typeof(VMEdicionUsuarios),
    //                Vista = Vistas.SegEdicionUsuario
    //            });
    //        #endregion
    //        #region Inventario

    //        //InvCategoria
    //        Relaciones.Add(typeof(InvCategoria),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCategorias),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Categorias,
    //                VMEdicion = typeof(VMEdicionCategoria),
    //                Vista = Vistas.InvEdicionCategoria
    //            });

    //        //InvAtributo
    //        Relaciones.Add(typeof(InvAtributo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoAtributos),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Atributos,
    //                VMEdicion = typeof(VMEdicionAtributo),
    //                Vista = Vistas.InvEdicionAtributo
    //            });

    //        //InvComponente
    //        Relaciones.Add(typeof(InvComponente),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoComponentes),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Componentes,
    //                VMEdicion = typeof(VMEdicionComponente),
    //                Vista = Vistas.InvEdicionComponente
    //            });

    //        //InvMarca
    //        Relaciones.Add(typeof(InvMarca),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoMarcas),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Marcas,
    //                VMEdicion = typeof(VMEdicionMarca),
    //                Vista = Vistas.InvEdicionMarca
    //            });

    //        //InvUnidadMedida
    //        Relaciones.Add(typeof(InvUnidadMedida),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoUnidadMedida),
    //                Nombre = WPF.General.Recursos.UI.Inventario.UnidadesMedida,
    //                VMEdicion = typeof(VMEdicionUnidadMedida),
    //                Vista = Vistas.InvEdicionUnidadMedida
    //            });

    //        //InvCausas
    //        Relaciones.Add(typeof(InvCausa),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCausas),
    //                Nombre = WPF.General.Recursos.UI.Generales.Causas,
    //                VMEdicion = typeof(VMEdicionCausa),
    //                Vista = Vistas.InvEdicionCausas
    //            });

    //        //InvUso
    //        Relaciones.Add(typeof(InvUso),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoUsos),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Usos,
    //                VMEdicion = typeof(VMEdicionUso),
    //                Vista = Vistas.InvEdicionUsos
    //            });

    //        //InvAlmacen
    //        Relaciones.Add(typeof(InvAlmacen),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoAlmacenes),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Almacenes,
    //                VMEdicion = typeof(VMEdicionAlmacen),
    //                Vista = Vistas.InvEdicionAlmacenes
    //            });

    //        //InvArticulo
    //        Relaciones.Add(typeof(InvArticulo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoArticulos),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Articulos,
    //                VMEdicion = typeof(VMEdicionArticulo),
    //                Vista = Vistas.InvEdicionArticulo
    //            });

    //        //InvGrupo
    //        Relaciones.Add(typeof(InvGrupo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoGrupos),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Grupos,
    //                VMEdicion = typeof(VMEdicionGrupo),
    //                Vista = Vistas.InvEdicionGrupo
    //            });

    //        //InvConfiguracionMinMax
    //        Relaciones.Add(typeof(InvConfiguracionMinMax),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoConfiguracionesMinMax),
    //                Nombre = WPF.General.Recursos.UI.Inventario.ConfiguracionesMinMax,
    //                VMEdicion = typeof(VMConfiguracionMinMax),
    //                Vista = Vistas.InvEdicionConfiguracionMinMax
    //            });

    //        //InvAjuste
    //        Relaciones.Add(typeof(InvAjuste),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoAjustes),
    //                Nombre = WPF.General.Recursos.UI.Inventario.Ajustes,
    //                VMEdicion = typeof(VMEdicionAjuste),
    //                Vista = Vistas.InvEdicionAjuste
    //            });
    //        //InvProcesarMinMax
    //        Relaciones.Add(typeof(InvMinMax),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoMinMax),
    //                Nombre = WPF.General.Recursos.UI.Inventario.ProcesarMinMaximos,
    //                VMEdicion = typeof(VMEdicionProcesarMinMax),
    //                Vista = Vistas.InvEdicionProcesoMinMax
    //            });

    //        //InvLoteAlmacen
    //        Relaciones.Add(typeof(InvLoteAlmacen),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoExistencias),
    //                Nombre = WPF.General.Recursos.UI.Inventario.LoteAlmacen,
    //                //VMEdicion = typeof(vmedic),
    //                Vista = Vistas.LoteAlmacen
    //            });


    //        #endregion //InvAjuste
    //        #region Ventas

    //        //VenCajero
    //        Relaciones.Add(typeof(VenCajero),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCajeros),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Cajeros,
    //                VMEdicion = typeof(VMEdicionCajero),
    //                Vista = Vistas.VenEdicionCajero
    //            });

    //        //VenCaja
    //        Relaciones.Add(typeof(VenCaja),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCajas),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Cajas,
    //                VMEdicion = typeof(VMEdicionCaja),
    //                Vista = Vistas.VenEdicionCaja
    //            });
    //        //VenOferta
    //        Relaciones.Add(typeof(VenOferta),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoOfertas),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Ofertas,
    //                VMEdicion = typeof(VMEdicionOferta),
    //                Vista = Vistas.VenEdicionOferta
    //            });

    //        ////VenCondicionVentaCat
    //        Relaciones.Add(typeof(VenCondicionVentaCategoria),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCondicionesVentas),
    //                Nombre = WPF.General.Recursos.UI.Ventas.CondicionesVentaCat,
    //                VMEdicion = typeof(VMEdicionCondicionVentaCat),
    //                Vista = Vistas.VenEdicionCondicionVentaCat
    //            });

    //        ////VenCondicionVentaArt
    //        Relaciones.Add(typeof(VenCondicionVentaArticulo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCondicionesVentas),
    //                Nombre = WPF.General.Recursos.UI.Ventas.CondicionesVentaArt,
    //                VMEdicion = typeof(VMEdicionCondicionVentaArt),
    //                Vista = Vistas.VenEdicionCondicionVentaArt
    //            });

    //        //VenTipoOferta
    //        Relaciones.Add(typeof(VenTipoOferta),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTipoOferta),
    //                Nombre = WPF.General.Recursos.UI.Ventas.TiposOfertas,
    //                VMEdicion = typeof(VMEdicionTipoOferta),
    //                Vista = Vistas.VenEdicionTipoOferta
    //            });

    //        //VenContratantes
    //        Relaciones.Add(typeof(VenContratante),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoContratantes),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Contratantes,
    //                VMEdicion = typeof(VMEdicionContratante),
    //                Vista = Vistas.VenEdicionContratante
    //            });

    //        //VenClientes
    //        Relaciones.Add(typeof(VenCliente),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoClientes),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Clientes,
    //                VMEdicion = typeof(VMEdicionCliente),
    //                Vista = Vistas.VenEdicionCliente
    //            });

    //        //VenCondicionVentaCliente
    //         Relaciones.Add(typeof(VenCondicionVentaCliente),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCondicionesVentas),
    //                Nombre = WPF.General.Recursos.UI.Ventas.CondicionesVentaCliente,
    //                VMEdicion = typeof(VMEdicionCondicionVentaCliente),
    //                Vista = Vistas.VenEdicionCondicionVentaCliente
    //            });
    //         Relaciones.Add(typeof(VenTitular),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTitulares),
    //                Nombre = WPF.General.Recursos.UI.Ventas.Titulares,
    //                VMEdicion = typeof(VMEdicionTitular),
    //                Vista = Vistas.VenEdicionTitular
    //            });
    //         Relaciones.Add(typeof(VenFormatoImpresora),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoFormatosImpresora),
    //                Nombre = WPF.General.Recursos.UI.Ventas.FormatosImpresora,
    //                VMEdicion = typeof(VMEdicionFormatoImpresora),
    //                Vista = Vistas.VenEdicionFormatoImpresora
    //            });
    //        #endregion //Ventas
    //        #region Compras

    //        //ComProveedor
    //        Relaciones.Add(typeof(ComProveedor),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoProveedores),
    //                Nombre = WPF.General.Recursos.UI.Compras.Proveedores,
    //                VMEdicion = typeof(VMEdicionProveedor),
    //                Vista = Vistas.ComEdicionProveedor
    //            });

    //        Relaciones.Add(typeof(ComOrdenCompra),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoOrdenCompra),
    //                Nombre = WPF.General.Recursos.UI.Compras.OrdenesCompras,
    //                VMEdicion = typeof(VMEdicionOrdenCompraManual),
    //                Vista = Vistas.ComEdicionOrdenCompraManual
    //            });

    //        Relaciones.Add(typeof(ComCausaReclamo),
    //        new DatosEntidades()
    //        {
    //            ServicioMaestro = typeof(IServicioManejoCausasReclamo),
    //            Nombre = WPF.General.Recursos.UI.Compras.CausasReclamo,
    //            VMEdicion = typeof(VMEdicionCausaReclamo),
    //            Vista = Vistas.ComEdicionCausaReclamo
    //        });

    //        Relaciones.Add(typeof(ComCatalogoProveedor),
    //        new DatosEntidades()
    //        {
    //            ServicioMaestro = typeof(IServicioManejoCatalogoProveedor),
    //            Nombre = WPF.General.Recursos.UI.Compras.CatalogoProveedor,
    //            VMEdicion = typeof(VMEdicionCatalogoProveedor),
    //            Vista = Vistas.ComEdicionCatalogoProveedor
    //        });

    //        Relaciones.Add(typeof(ComFactura),
    //        new DatosEntidades()
    //        {
    //            ServicioMaestro = typeof(IServicioManejoFacturaCompra),
    //            Nombre = WPF.General.Recursos.UI.Compras.RegistroFactura,
    //            VMEdicion = typeof(VMEdicionRegistroFacturaCompra),
    //            Vista = Vistas.ComEdicionRegistroFacturaCompra
    //        });

    //        Relaciones.Add(typeof(ComReclamo),
    //        new DatosEntidades()
    //        {
    //            ServicioMaestro = typeof(IServicioManejoReclamo),
    //            Nombre = WPF.General.Recursos.UI.Compras.RegistroReclamo,
    //            VMEdicion = typeof(VMEdicionReclamos),
    //            Vista = Vistas.ComEdicionReclamos
    //        });

    //        Relaciones.Add(typeof(ComEntradaMercancia),
    //        new DatosEntidades()
    //        {
    //            ServicioMaestro = typeof(IServicioManejoEntradaMercancia),
    //            Nombre = WPF.General.Recursos.UI.Compras.EntradaMercancia,
    //            VMEdicion = typeof(VMEdicionEntradaMercancia),
    //            Vista = Vistas.ComEdicionEntradaMercancia
    //        });

    //        #endregion //Compras
    //        #region Bancos
    //        Relaciones.Add(typeof(BanBanco),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoBancos),
    //                Nombre = WPF.General.Recursos.UI.Generales.Bancos,
    //                VMEdicion = typeof(VMEdicionBanco),
    //                Vista = Vistas.BanEdicionBanco
    //            });

    //        Relaciones.Add(typeof(BanPuntoBancario),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoPuntosBancarios),
    //                Nombre = WPF.General.Recursos.UI.Bancos.PuntosBancarios,
    //                VMEdicion = typeof(VMEdicionPuntoBancario),
    //                Vista = Vistas.BanEdicionPuntoBancario
    //            });

    //        Relaciones.Add(typeof(BanCuenta),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCuentas),
    //                Nombre = WPF.General.Recursos.UI.Bancos.Cuentas,
    //                VMEdicion = typeof(VMEdicionCuenta),
    //                Vista = Vistas.BanEdicionCuentas
    //            });

    //        Relaciones.Add(typeof(BanFormaPago),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoFormasPagos),
    //                Nombre = WPF.General.Recursos.UI.Bancos.FormasPago,
    //                VMEdicion = typeof(VMEdicionFormaPago),
    //                Vista = Vistas.BanEdicionFormasDePago
    //            });

    //        Relaciones.Add(typeof(BanSucursal),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoSucursales),
    //                Nombre = WPF.General.Recursos.UI.Bancos.Sucursales,
    //                VMEdicion = typeof(VMEdicionSucursal),
    //                Vista = Vistas.BanEdicionSucursal
    //            });

    //        Relaciones.Add(typeof(BanConceptoCC),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoConceptosCC),
    //                Nombre = WPF.General.Recursos.UI.Bancos.ConceptosCajaChica,
    //                VMEdicion = typeof(VMEdicionConceptoCajaChica),
    //                Vista = Vistas.BanEdicionConceptosCajaChica
    //            });

    //        Relaciones.Add(typeof(BanBeneficiario),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoBeneficiariosBancos),
    //                Nombre = WPF.General.Recursos.UI.Bancos.Beneficiarios,
    //                VMEdicion = typeof(VistaModelo.Bancos.VMEdicionBeneficiario),
    //                Vista = Vistas.BanEdicionBeneficiario
    //            });


    //        Relaciones.Add(typeof(BanChequera),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoChequeras),
    //                Nombre = WPF.General.Recursos.UI.Bancos.Chequeras,
    //                VMEdicion = typeof(VMEdicionChequera),
    //                Vista = Vistas.BanEdicionChequera
    //            });
    //        Relaciones.Add(typeof(BanMovimientoCC),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoMovimientosCC),
    //                Nombre = WPF.General.Recursos.UI.Bancos.MovimientosCajaChica,
    //                VMEdicion = typeof(VMEdicionMovimientoCajaChica),
    //                Vista = Vistas.BanEdicionMovimientoCajaChica
    //            });
    //        Relaciones.Add(typeof(BanCajaChica),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCajasChicas),
    //                Nombre = WPF.General.Recursos.UI.Bancos.CajaChica,
    //                VMEdicion = typeof(VMEdicionCajaChica),
    //                Vista = Vistas.BanEdicionCajaChica
    //            });
    //        Relaciones.Add(typeof(BanConceptoBancario),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoConceptosBancarios),
    //                Nombre = WPF.General.Recursos.UI.Bancos.ConceptosBancarios,
    //                VMEdicion = typeof(VMEdicionConceptoBancario),
    //                Vista = Vistas.BanEdicionConceptosBancarios
    //            });
    //        #endregion  //Bancos
    //        #region Nomina
    //        Relaciones.Add(typeof(NomArea),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoAreas),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Areas,
    //                VMEdicion = typeof(VMEdicionArea),
    //                Vista = Vistas.NomEdicionArea
    //            });

    //        Relaciones.Add(typeof(NomCargo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCargos),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Cargos,
    //                VMEdicion = typeof(VMEdicionCargo),
    //                Vista = Vistas.NomEdicionCargo
    //            });

    //        Relaciones.Add(typeof(NomProfesion),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoProfesiones),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Profesiones,
    //                VMEdicion = typeof(VMEdicionProfesion),
    //                Vista = Vistas.NomEdicionProfesion
    //            });

    //        Relaciones.Add(typeof(NomTipoPrestamos),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTiposPrestamo),
    //                Nombre = WPF.General.Recursos.UI.Nomina.TiposPrestamo,
    //                VMEdicion = typeof(VMEdicionTipoPrestamo),
    //                Vista = Vistas.NomEdicionTipoPrestamo
    //            });

    //        Relaciones.Add(typeof(NomTipoAbsentismo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTiposAbsentismo),
    //                Nombre = WPF.General.Recursos.UI.Nomina.TiposAbsentismo,
    //                VMEdicion = typeof(VMEdicionTipoAbsentismo),
    //                Vista = Vistas.NomEdicionTipoAbsentismo
    //            });

    //        Relaciones.Add(typeof(NomTipoNomina),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTiposNomina),
    //                Nombre = WPF.General.Recursos.UI.Nomina.TiposNomina,
    //                VMEdicion = typeof(VMEdicionTipoNomina),
    //                Vista = Vistas.NomEdicionTipoNomina
    //            });

    //        Relaciones.Add(typeof(NomPeriodo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoPeriodos),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Periodos,
    //                VMEdicion = typeof(VMEdicionPeriodo),
    //                Vista = Vistas.NomEdicionPeriodo
    //            });

    //        Relaciones.Add(typeof(NomFamiliar),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoFamiliares),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Familiares,
    //                VMEdicion = typeof(VMEdicionFamiliares),
    //                Vista = Vistas.NomEdicionFamiliar
    //            });

    //        Relaciones.Add(typeof(NomModelo),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoModelos),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Modelos,
    //                VMEdicion = typeof(VMEdicionModelo),
    //                Vista = Vistas.NomEdicionModelo
    //            });

    //        Relaciones.Add(typeof(NomConcepto),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoConceptos),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Conceptos,
    //                VMEdicion = typeof(VMEdicionConcepto),
    //                Vista = Vistas.NomEdicionConceptos
    //            });

    //        Relaciones.Add(typeof(NomCausaLiquidacion),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCausasLiquidacion),
    //                Nombre = WPF.General.Recursos.UI.Nomina.CausasLiquidacion,
    //                VMEdicion = typeof(VMEdicionCausaLiquidacion),
    //                Vista = Vistas.NomEdicionCausaLiquidacion
    //            });

    //        Relaciones.Add(typeof(NomEmpleado),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoEmpleados),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Empleados,
    //                VMEdicion = typeof(VMEdicionEmpleado),
    //                Vista = Vistas.NomEdicionEmpleado
    //            });

    //        Relaciones.Add(typeof(NomTasa),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTasasIntereses),
    //                Nombre = WPF.General.Recursos.UI.Nomina.TasaInteres,
    //                VMEdicion = typeof(VMEdicionTasaInteres),
    //                Vista = Vistas.NomEdicionTasaInteres
    //            });

    //        Relaciones.Add(typeof(NomConstante),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoConstantes),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Constantes,
    //                VMEdicion = typeof(VMEdicionConstante),
    //                Vista = Vistas.NomEdicionConstante
    //            });

    //        Relaciones.Add(typeof(NomTablaEquivalencias),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoTablasEquivalencias),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Equivalencias,
    //                VMEdicion = typeof(VMEdicionEquivalencia),
    //                Vista = Vistas.NomEdicionEquivalencias
    //            });

    //        Relaciones.Add(typeof(NomCalendario),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoCalendarios),
    //                Nombre = WPF.General.Recursos.UI.Nomina.Calendario,
    //                VMEdicion = typeof(VMEdicionCalendario),
    //                Vista = Vistas.NomEdicionCalendario
    //            });
    //        #endregion //Nomina

    //        #region Finanzas
    //        Relaciones.Add(typeof(FinConceptoImpto),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoImpuestos),
    //                Nombre = WPF.General.Recursos.UI.Finanzas.ConceptosImpto,
    //                VMEdicion = typeof(VMEdicionConceptoImpto),
    //                Vista = Vistas.FinEdicionConceptoImpto
    //            });

    //        Relaciones.Add(typeof(FinRamoImpto),
    //            new DatosEntidades()
    //            {
    //                ServicioMaestro = typeof(IServicioManejoRamoImpto),
    //                Nombre = WPF.General.Recursos.UI.Finanzas.RamosImpto,
    //                VMEdicion = typeof(VMEdicionRamoImpto),
    //                Vista = Vistas.FinEdicionRamoImpto
    //            });
    //        #endregion

    //    }
        
    //}

}
