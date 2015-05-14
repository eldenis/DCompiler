using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Security;
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;
using WPF.Cliente.Nucleo.MensajesApp;
using System.Windows;

namespace WPF.Cliente.Util
{
    public static class EjecutorAutenticado
    {
        //#region Métodos públicos
        //public static void EjecutarConSeguridad(this Action accion)
        //{
        //    try { accion(); }
        //    catch (SecurityException ex)
        //    {
        //        //if (ex.Demanded is PrincipalPermission)
        //        //{ var permisosNecesarios = ex.Demanded as PrincipalPermission; }                
        //        var principal = MostrarDialogoAutenticacion();
        //        if (principal != null) EjecutarConPrincipal(principal, accion);
        //    }
        //}

        //public static T EjecutarConSeguridad<T>(this Func<T> accion)
        //{
        //    T retval = default(T);

        //    try { retval = accion(); }
        //    catch (SecurityException ex)
        //    {
        //        var principal = MostrarDialogoAutenticacion();
        //        if (principal != null) EjecutarConPrincipal(principal, accion);
        //    }
        //    return retval;
        //}
        //#endregion

        //#region Métodos privados
        //private static void EjecutarConPrincipal(IPrincipal principal, Action accion)
        //{
        //    var principalOriginal = EstablecerPrincipal(principal);

        //    try { accion(); }
        //    catch (SecurityException) { throw; }
        //    finally { ReEstablecerPrincipal(principalOriginal); }
        //}

        //private static T EjecutarConPrincipal<T>(IPrincipal principal, Func<T> accion)
        //{
        //    var principalOriginal = EstablecerPrincipal(principal);

        //    try { return accion(); }
        //    catch (SecurityException) { throw; }
        //    finally { ReEstablecerPrincipal(principalOriginal); }
        //}

        //private static IPrincipal EstablecerPrincipal(IPrincipal principal)
        //{
        //    var principalOriginal = Thread.CurrentPrincipal;
        //    Thread.CurrentPrincipal = principal;

        //    return principalOriginal;
        //}

        //private static void ReEstablecerPrincipal(IPrincipal principal)
        //{
        //    Thread.CurrentPrincipal = principal;
        //}
        //#endregion

        //private static IPrincipal MostrarDialogoAutenticacion()
        //{
        //    var principal = default(IPrincipal);

        //    new MensajeMostrarDialogo<ResultadoDialogo>(
        //       null,
        //       Nucleo.Vistas.GenAutenticacion,
        //       new VMAutenticacionUsuario(),
        //       accion: res =>
        //       {
        //           if (res != null && res.Resultado == MessageBoxResult.OK && res.HayValores<IPrincipal>())
        //               principal = res.ObtenerPrimero<IPrincipal>();
        //       }).Enviar();

        //    return principal;
        //}
    }
}
