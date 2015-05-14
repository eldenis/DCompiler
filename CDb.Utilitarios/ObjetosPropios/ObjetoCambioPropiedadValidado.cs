using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CDb.Transversal.Utilitarios
{
    /// <summary>
    /// Una clase que sirve para combinar los comportamientos
    /// de <see cref="ObjetoCambioPropiedad"/> y <see cref="ObjetoValidado"/> en una sola.
    /// </summary>
    public abstract class ObjetoCambioPropiedadValidado : ObjetoValidado, IPropiedadCambioConEvento
    {
        #region Implementación de IPropiedadCambioConEvento
        public event PropertyChangedEventHandler PropertyChanged;

        void IPropiedadCambio.LevantarCambioPropiedad(string nombre)
        {
            VerificarNombrePropiedad(nombre);

            var handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(nombre));

            DespuesPropiedadCambiada(nombre);
        }

        public void LevantarCambioPropiedad(Expression<Func<object>> propiedad)
        {
            (this as IPropiedadCambio).LevantarCambioPropiedad(propiedad.GetMemberName());
            DespuesPropiedadCambiada(propiedad);
        }

        protected void LevantarCambioPropiedad(string nombre)
        {
            (this as IPropiedadCambio).LevantarCambioPropiedad(nombre);
            DespuesPropiedadCambiada(nombre);
        }

        public virtual void DespuesPropiedadCambiada(Expression<Func<object>> propiedad) { }
        public virtual void DespuesPropiedadCambiada(string propiedad) { }
        #endregion

        #region Ayudas DEBUG

        /// <summary>
        /// Le advierte al desarrollador si este objeto no
        /// tiene una propiedad pública con el nombre especificado.
        /// Este metodo no existe en el ensamblado final.        
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerificarNombrePropiedad(string nombrePropiedad)
        {
            /**
             *  Verifica que haya una propiedad con un el nombre
             *  indicado y que sea una propiedad pública de la instancia
             *  en este objeto.
             */
            if (TypeDescriptor.GetProperties(this)[nombrePropiedad] == null)
            {
                //No importa que este mensaje esté literal porque es solo para DEBUG
                string msg = "Nombre de propiedad inválida: " + nombrePropiedad;

                if (this.LanzarExcepcionPropiedadInvalida)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Retorna si se lanza una excepción, o si se debe usar un Debug.Fail()
        /// cuando se pasa un nombre de propiedad invalido al método VerificarNombrePropiedad
        /// El valor predeterminado es false, pero las subclases usadas en los 
        /// unit tests pueden hacer override del get de la propiedad para cambiarlo a true        
        /// </summary>
        protected virtual bool LanzarExcepcionPropiedadInvalida { get; private set; }

        #endregion // Ayudas DEBUG


    }
}
