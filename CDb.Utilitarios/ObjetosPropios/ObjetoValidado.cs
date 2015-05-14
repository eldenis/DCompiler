using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Linq.Expressions;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using CDb.Transversal.Utilitarios.ObjetosPropios;


namespace CDb.Transversal.Utilitarios
{
    /// <summary>
    /// Una clase que sirve para validar los campos de las clases que 
    /// la hereden usando el validation block del entity library
    /// o las anotaciones del DataAnnotations
    /// </summary>
    public abstract class ObjetoValidado : System.ComponentModel.IDataErrorInfo
    {
        Validator _validador = null;
        ValidationResults ultimosResultados = null;
        bool _validaAutomaticamente = true;

        public ObjetoValidado(bool validaAutomaticamente = true)
        {
            _validaAutomaticamente = validaAutomaticamente;

            var tipo = this.GetType();

            try
            {
                _validador = Validador.ObtenerValidador(tipo);
                //var valFactory = EnterpriseLibraryContainer.Current.GetInstance<ValidatorFactory>();
                //_validador = valFactory.CreateValidator(tipo);
            }
            catch (Exception) { }

            if (!validaAutomaticamente)
                RealizarValidaciones("");
        }

        protected void RealizarValidaciones(string nombrePropiedad)
        {
            ultimosResultados = new ValidationResults();
            //Esta linea es para cuando trabajo con el validador propio
            _validador.DoValidate(this, null, nombrePropiedad, ultimosResultados);

            //Esta linea es para solo el validator de entlib
            //_validador.Validate(this, ultimosResultados);
        }

        public virtual string this[string nombrePropiedad]
        {
            get
            {
                if (_validador != null)
                {
                    var resFiltrados = new ValidationResults();

                    if (_validaAutomaticamente)
                        RealizarValidaciones(nombrePropiedad);

                    if (ultimosResultados != null)
                    {
                        resFiltrados.AddAllResults(from resultado in ultimosResultados
                                                   where resultado.Key == nombrePropiedad
                                                   select resultado);

                        return !resFiltrados.IsValid ?
                            resFiltrados.First().Message : string.Empty;
                    }
                }
                return string.Empty;
            }
        }

        public virtual string Error
        {
            get
            {
                return ultimosResultados != null && !ultimosResultados.IsValid ?
                  ultimosResultados.First().Message : string.Empty;
            }
        }

        public virtual bool EsValido { get { return string.IsNullOrEmpty(Error); } }
    }
}
