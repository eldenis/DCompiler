using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace CDb.Transversal.Utilitarios
{
    public static class MiembroAtributo
    {
        public static List<MiembroAtributo<TAtributo>> ObtenerPropiedadesDeObjeto<TAtributo>(object obj, bool metadata = true)
                where TAtributo : Attribute
        {
            if (obj == null) return null;

            Type tipo = metadata ? ObtenerTipoMetadata(obj) : obj.GetType();

            return ObtenerPropiedadesDeObjeto<TAtributo>(tipo);
        }

        public static List<MiembroAtributo<TAtributo>> ObtenerPropiedadesDeObjeto<TAtributo>(Type tipo, bool metadata)
           where TAtributo : Attribute
        {
            if (metadata) tipo = ObtenerTipoMetadata(tipo);

            return ObtenerPropiedadesDeObjeto<TAtributo>(tipo);
        }

        public static List<MiembroAtributo<TAtributo>> ObtenerPropiedadesDeObjeto<TAtributo>(Type tipo)
           where TAtributo : Attribute
        {
            if (tipo != null)
            {
                var q = (from propiedad in tipo.GetMembers()
                         where propiedad.GetCustomAttributes(typeof(TAtributo), true).Count() > 0
                         select new MiembroAtributo<TAtributo>
                                 (propiedad,
                                 propiedad.GetCustomAttributes(typeof(TAtributo), true)
                                    .AsListOf<TAtributo>()));

                return q.ToList();
            }

            return null;
        }

        public static Type ObtenerTipoMetadata(object obj)
        {
            if (obj != null) return ObtenerTipoMetadata(obj.GetType());
            return null;
        }

        public static Type ObtenerTipoMetadata(Type tipo)
        {
            if (tipo != null)
            {
                var atributoMetadata = tipo.GetCustomAttributes(typeof(MetadataTypeAttribute), true)
                .OfType<MetadataTypeAttribute>().FirstOrDefault();

                if (atributoMetadata != null) return atributoMetadata.MetadataClassType;
            }
            return null;
        }
    }

    public class MiembroAtributo<TAttribute> where TAttribute : Attribute
    {
        public MiembroAtributo(MemberInfo mi, List<TAttribute> atr)
        {
            MemberInfo = mi;
            Atributo = atr;
        }

        public MemberInfo MemberInfo { get; private set; }
        public List<TAttribute> Atributo { get; private set; }

    }

  
}
