using System;
using MessageBox = Microsoft.Windows.Controls.MessageBox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dragonz.actb.provider;

namespace WPF.Cliente.Nucleo
{
    //public class DataProviderDeServicio<TServicio, TEntidad> :
    //    IAutoCompleteDataProvider
    //    where TServicio : IServicioEntidadBase<TEntidad>
    //    where TEntidad : EntidadBase
    //{

    //    TServicio _servicio;
    //    List<string> _ultimosResultados = null;

    //    public DataProviderDeServicio()
    //    {
    //        _servicio = IoCFactory.Instance.CurrentContainer.Resolve<TServicio>();
    //    }

    //    public IEnumerable<string> GetItems(string filtro)
    //    {
    //        if (_servicio != null)
    //        {
    //            _ultimosResultados = _servicio.ObtenerRegistrosPorDescripcion<TEntidad>(filtro)
    //                .Select(i => i.CampoID + " -- " + i.CampoFiltradoPredeterminado).ToList();
    //        }

    //        return _ultimosResultados;
    //    }


    //}
}
