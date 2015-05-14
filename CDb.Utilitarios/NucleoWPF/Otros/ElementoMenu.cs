using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CDb.Transversal.Utilitarios;
using WPF.Cliente.Nucleo.MensajesApp;
using WPF.Cliente.VistaModelo.Nucleo;
using GalaSoft.MvvmLight.Command;
using WPF.Cliente.VistaModelo.Generales;
using System.Reflection;

namespace WPF.Cliente.Nucleo
{
    public class ElementoMenu : ComandoBase
    {
        //#region Constructores
        //public ElementoMenu() : this(null) { }

        //public ElementoMenu(KeyGesture acceso)
        //    : base(acceso)
        //{

        //}
        //#endregion

        ////TODO: Ubicar el comando por teclado
        ////TODO: IsChecked()
        ////TODO: IsEnabled()
        ////TODO: Visibility

        //public ElementoMenu Padre { get; set; }
        //ObservableCollection<ElementoMenu> _hijos;
        //public ObservableCollection<ElementoMenu> Hijos
        //{
        //    get
        //    {
        //        return _hijos;
        //    }
        //    set
        //    {
        //        _hijos = value;
        //        LevantarCambioPropiedad(() => Hijos);
        //        //Cada vez que se agregan hijos a la colección, se coloca
        //        //que esta instancia es el padre.
        //        Hijos.CollectionChanged += (s, e) =>
        //        {
        //            if (e.NewItems != null)
        //                e.NewItems.OfType<ElementoMenu>().ForEach(el =>
        //                {
        //                    el.Padre = this;
        //                });
        //        };
        //    }
        //}

        //public Type Modelo { get; set; }
        //public Vistas Vista { get; set; }
        //public NivelMenu Nivel { get; set; }
        //public int Posicion { get; set; }
        ////public ObservableCollection<ERolesSeguridad> RolesSeguridad { get; set; }

        //#region  Miembros estáticos

        //public static Dictionary<NivelMenu, ElementoMenu> Menu { get; private set; }
        ////public static ColeccionComando<ComandoBase> Comandos { get; private set; }

        //static ElementoMenu()
        //{
        //    Menu = new Dictionary<NivelMenu, ElementoMenu>();
        //    //Comandos = new ColeccionComando<ComandoBase>();
        //    ConstruirMenus();
        //}

        //private static void ConstruirMenus()
        //{
        //    List<ElementoMenu> _menusCargados = new List<ElementoMenu>();

        //    //var ensamblados = AppDomain.CurrentDomain.GetAssemblies();
        //    var asm = Assembly.GetExecutingAssembly();

        //    var tipos = asm.GetTypes().Where(tipo => tipo.Name.StartsWith("VM") && !tipo.ContainsGenericParameters);
        //    foreach (var tipo in tipos)
        //    {
        //        PropertyInfo propMenus = tipo.GetProperty("Menus");
        //        if (propMenus != null)
        //        {
        //            var menus = propMenus.GetValue(null, null) as ObservableCollection<ElementoMenu>;

        //            if (menus != null) { _menusCargados.AddRange(menus); }
        //        }
        //    }

        //    //Ya están los menús cargados en la lista

        //    foreach (var nivel in Enum.GetValues(typeof(NivelMenu)).AsListOf<NivelMenu>())
        //    {

        //        //var elNivel = nivel;

        //        var nuevoElemento = new ElementoMenu()
        //        {
        //            Titulo = nivel.ToString(),
        //            Hijos = new ObservableCollection<ElementoMenu>(
        //                _menusCargados
        //                .Where(m => m.Nivel == nivel)
        //                .OrderBy(m => m.Posicion)
        //            )
        //        };

        //        nuevoElemento.Hijos.ForEach(hijo =>
        //        {
        //            if (hijo.Comando == null)
        //            {
        //                hijo.Comando = new RelayCommand(delegate
        //                {
        //                    MostrarVista(hijo.Vista, (VMBase)Activator.CreateInstance(hijo.Modelo));
        //                });
        //            }
        //        });

        //        if (nivel.ToString().Contains("_"))
        //        {//Es un subnivel
        //            var valEnum = Enum.Parse(typeof(NivelMenu), nivel.ToString().Split('_')[0]);

        //            if (valEnum != null)
        //                Menu[(NivelMenu)valEnum].Hijos.Add(nuevoElemento);
        //        }
        //        else
        //            Menu.Add(nivel, nuevoElemento);

        //        //Menu.Add(nivel,);

        //    }

        //    /*CargarMenusInventario();
        //    CargarMenusCompras();*/
        //}



        //private static void CargarMenusInventario()
        //{
        //    //Menús de Inventario
        //    Menu.Add(NivelMenu.Inventario, new ElementoMenu()
        //    {
        //        Titulo = "Inventario",
        //        Hijos = new ObservableCollection<ElementoMenu> { 
        //            //Datos Maestros
        //            new ElementoMenu()
        //            {
        //                Titulo = "Datos Maestros",
        //                Hijos = new ObservableCollection<ElementoMenu>{                            
        //                    //Artículos
        //                    new ElementoMenu(new KeyGesture(Key.A, ModifierKeys.Control)){
        //                        Titulo = WPF.General.Recursos.UI.Inventario.Articulos,
        //                        Comando = new RelayCommand(delegate{
        //                            MostrarVista(
        //                            vista: Vistas.ListaArticulos,
        //                            modelo: new VMListaGenerica<InvArticulo>());
        //                        }, delegate{return PuedeEjecutar();})
        //                    },
        //                    //Categorías
        //                    new ElementoMenu(new KeyGesture(Key.C, ModifierKeys.Control)){
        //                        Titulo = WPF.General.Recursos.UI.Inventario.Categorias,
        //                        Comando = new RelayCommand(delegate{
        //                            MostrarVista(
        //                            vista: Vistas.ListaCategorias,
        //                            modelo: new VMListaGenerica<InvCategoria>());
        //                        }, delegate{return PuedeEjecutar();})
        //                    }
        //                }
        //            }
        //        }
        //    });
        //}

        //private static void CargarMenusCompras()
        //{
        //    //Menús de Compras
        //    Menu.Add(NivelMenu.Compras, new ElementoMenu()
        //    {
        //        Titulo = "Compras",
        //        Hijos = new ObservableCollection<ElementoMenu> { 
        //            //Datos Maestros
        //            new ElementoMenu()
        //            {
        //                Titulo = "Datos Maestros",
        //                Hijos = new ObservableCollection<ElementoMenu>{                            
        //                    //Artículos
        //                    new ElementoMenu(new KeyGesture(Key.P, ModifierKeys.Control)){
        //                        Titulo = WPF.General.Recursos.UI.Compras.Proveedores,
        //                        Comando = new RelayCommand(delegate{
        //                            MostrarVista(
        //                            vista: Vistas.ListaProveedores,
        //                            modelo: new VMListaGenerica<ComProveedor>());
        //                        }, delegate{return PuedeEjecutar();})
        //                    }
        //                }
        //            }
        //        }
        //    });
        //}

        //private static bool PuedeEjecutar()
        //{
        //    return true;
        //}
        //#endregion

        //#region Métodos de mensajes
        //private static void MostrarDialogo<T>(
        //   Vistas vista,
        //   VMBase modelo,
        //   Action<T> accion,
        //   bool modal = true,
        //   object receptor = null)
        //{
        //    MensajeMostrarDialogo<T> msj =
        //        new MensajeMostrarDialogo<T>(null, vista, modelo, accion, modal, receptor);

        //    msj.Enviar();
        //}


        //private static void MostrarVista(
        //    Vistas vista,
        //    VMBase modelo,
        //    bool modal = true,
        //    object receptor = null)
        //{
        //    MensajeMostrarVista msj =
        //        new MensajeMostrarVista(null, vista, modelo, modal, receptor);

        //    msj.Enviar();
        //}
        //#endregion

    }
}
