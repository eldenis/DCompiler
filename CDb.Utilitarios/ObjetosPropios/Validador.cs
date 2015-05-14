using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace CDb.Transversal.Utilitarios.ObjetosPropios
{
    public class Validador : Validator
    {

        #region Parte estática

        public static Dictionary<Type, Validador> _validadores = new Dictionary<Type, Validador>();

        public static Validador ObtenerValidador(Type tipo, string ruleSet = "", bool soloMetadata = false)
        {
            if (_validadores.ContainsKey(tipo))
            {
                return _validadores[tipo];
            }
            else
            {
                try
                {
                    var nuevo = new Validador(tipo, ruleSet, soloMetadata);
                    _validadores.Add(tipo, nuevo);
                    return nuevo;
                }
                catch (Exception e)
                {
                    return ObtenerValidador(tipo, ruleSet, soloMetadata);
                }
            }
        }
        #endregion

        #region Constructor
        protected Validador(Type tipo, string ruleSet, bool soloMetadata = false)
            : base("", "")
        {
            SoloMetadata = soloMetadata;
            RuleSet = ruleSet;
            Metadata = tipo.GetMetadataType();

            try
            {
                var valFactory = EnterpriseLibraryContainer.Current.GetInstance<ValidatorFactory>();
                ValidadorEntity = valFactory.CreateValidator(tipo);
            }
            catch (Exception e) { }

            GenerarValidaciones(tipo);
        }


        #endregion

        #region Miembros/Métodos privados

        /// <summary>
        /// Tipo Metadata del tipo de datos.
        /// </summary>
        public System.ComponentModel.DataAnnotations.MetadataTypeAttribute Metadata { get; private set; }

        private Dictionary<string, Tuple<PropertyInfo, ContenedorValidadores>> _atributos =
            new Dictionary<string, Tuple<PropertyInfo, ContenedorValidadores>>();


        private void GenerarValidaciones(Type tipo)
        {//IgnoreNullsAttribute
            List<PropertyInfo> propiedadesReales = tipo.GetProperties().ToList<PropertyInfo>();

            List<PropertyInfo> todasProps = new List<PropertyInfo>();

            if (!SoloMetadata)
                todasProps.AddRange(propiedadesReales);

            if (Metadata != null)
                todasProps.AddRange(Metadata.MetadataClassType.GetProperties());

            foreach (var prop in todasProps)
            {
                ContenedorValidadores validadores = ObtenerValidatorAttributes(prop, RuleSet);
                if (validadores.Validadores.Count > 0)
                {
                    var propReal = propiedadesReales.Where(pr => pr.Name == prop.Name).FirstOrDefault();

                    if (propReal == null)
                        throw new Exception("Error en la metadata. La propiedad: " + prop.Name + " no existe en la clase base.");

                    var tupla = new Tuple<PropertyInfo, ContenedorValidadores>(propReal, validadores);


                    if (_atributos.ContainsKey(prop.Name))
                    {
                        _atributos[prop.Name].Item2.Validadores.AddRange(validadores.Validadores);
                        _atributos[prop.Name].Item2.IgnorarNulos = validadores.IgnorarNulos;
                    }
                    else
                        _atributos.Add(prop.Name, tupla);
                }
            }
        }

        /// <summary>
        /// Obtiene el mensaje de error de un <see cref="ValidatorAttribute"/>
        /// y lo pasa por referencia en la variable mensaje
        /// </summary>
        /// <param name="validacion">ValidationAttribute de dónde sacar el mensaje de error</param>
        /// <param name="mensaje">El valor por ref que tendrá el mensaje listo</param>
        private static void ObtenerMensajeError(ValidatorAttribute validacion, ref string mensaje)
        {
            if (validacion.ErrorMessageResourceType != null &&
                !string.IsNullOrWhiteSpace(validacion.ErrorMessageResourceName))
            {
                var resProp = validacion.ErrorMessageResourceType
                    .GetProperty(validacion.ErrorMessageResourceName);

                if (resProp != null)
                    mensaje = (string)resProp.GetValue(null, null);
            }
            else if (!string.IsNullOrEmpty(validacion.ErrorMessage))
            {
                mensaje = validacion.ErrorMessage;
            }
        }

        /// <summary>
        /// Obtiene los <see cref="ValidatorAttribute"/> de las propiedades
        /// Aplicando el RuleSet pasado.
        /// </summary>
        /// <param name="prop">Las propiedad de dónde sacar los atributos</param>
        /// <param name="ruleSet">El RuleSet que se quiere aplicar</param>
        /// <returns></returns>
        private static ContenedorValidadores ObtenerValidatorAttributes(PropertyInfo prop, string ruleSet)
        {
            var validadores = prop.GetCustomAttributes(typeof(ValidatorAttribute), true)
                        .Cast<ValidatorAttribute>()
                        .Where(va => (va as ValidatorAttribute).Ruleset == ruleSet)
                        .Select(va => (va as ValidatorAttribute)).ToList();

            var permiteNulos = prop.GetCustomAttributes(typeof(IgnoreNullsAttribute), true).Length > 0;

            return new ContenedorValidadores(validadores, permiteNulos);
        }
        #endregion

        #region Propiedades
        public Validator ValidadorEntity { get; set; }
        public Validator ValidadorMetadata { get; set; }

        public bool SoloMetadata { get; private set; }
        public string RuleSet { get; private set; }
        #endregion

        #region Overrides
        protected override string DefaultMessageTemplate
        {
            get { return ""; }
        }

        public override void DoValidate(object objectToValidate, object currentTarget,
            string key, ValidationResults validationResults)
        {
            //Valida todas las propiedades.
            //También se podría validar sólo la propiedad necesaria (en parámetro key).
            foreach (var atributo in _atributos)
            {
                var valor = atributo.Value.Item1.GetValue(objectToValidate, null);

                if (valor == null && atributo.Value.Item1.PropertyType == typeof(string))
                    valor = string.Empty;

                foreach (var validacion in atributo.Value.Item2)
                {
                    if ((valor == null) && atributo.Value.Item2.IgnorarNulos) { break; }

                    if (validacion is PropertyComparisonValidatorAttribute) continue;

                    if (!validacion.IsValid(valor))
                    {
                        var mensaje = string.Empty;

                        ObtenerMensajeError(validacion, ref mensaje);

                        if (!string.IsNullOrWhiteSpace(mensaje))
                        {
                            var vr = new ValidationResult(mensaje, null, atributo.Key, "", this);
                            validationResults.AddResult(vr);
                        }
                    }

                }
            }

            validationResults.AddAllResults(ValidadorEntity.Validate(objectToValidate));
        }


        #endregion

    }

    public class ContenedorValidadores : IEnumerable<ValidatorAttribute>
    {
        public List<ValidatorAttribute> Validadores { get; private set; }
        public bool IgnorarNulos { get; set; }

        public ContenedorValidadores(List<ValidatorAttribute> validadores
            , bool permitirNulos)
        {
            IgnorarNulos = permitirNulos;
            Validadores = validadores;
        }

        public ValidatorAttribute this[int index]
        {
            get { return Validadores[index]; }
            set { Validadores[index] = value; }
        }

        public int Count { get { return Validadores.Count; } }

        public IEnumerator<ValidatorAttribute> GetEnumerator()
        { return Validadores.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return Validadores.GetEnumerator(); }
    }
}
