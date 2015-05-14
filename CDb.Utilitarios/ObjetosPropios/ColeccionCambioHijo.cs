using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CDb.Transversal.Utilitarios.ObjetosPropios
{

    /// <summary>
    /// Implementa una colección <see cref="ObservableCollection<T>"/> genérica
    /// que expone un evento <see cref="CambioPropiedadHijoHandler"/> que contiene
    /// información del cambio de propiedad de un hijo de la colección
    /// </summary>
    /// <typeparam name="T">El tipo de itemes que tendrá la colección</typeparam>
    public class ColeccionCambioHijo<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged
    {
        public delegate void CambioPropiedadHijoHandler(T sender, PropertyChangedEventArgs e);

        public event CambioPropiedadHijoHandler CambioPropiedadHijo;

        public ColeccionCambioHijo() { }
        public ColeccionCambioHijo(IEnumerable<T> itemes)
            : base(itemes)
        {
            VincularTodosHijosAEventoPropertyChanged();
        }
        public ColeccionCambioHijo(List<T> itemes)
            : base(itemes)
        {
            VincularTodosHijosAEventoPropertyChanged();
        }

        private void VincularTodosHijosAEventoPropertyChanged()
        {
            foreach (var val in this)
            {
                VincularEventoPropertyChanged(val);
            }
        }

        private void VincularEventoPropertyChanged(T item)
        {
            item.PropertyChanged += (sender, e) =>
            {
                var handler = CambioPropiedadHijo;
                if (handler != null)
                    CambioPropiedadHijo((T)sender, e);
            };
        }

        public new void Add(T item)
        {
            VincularEventoPropertyChanged(item);

        //TODO: Averiguar qué está pasando con esto.
        intentarDeNuevo:
            try { base.Add(item); }
            catch (Exception)
            {
                goto intentarDeNuevo;
            }

        }
    }
}
