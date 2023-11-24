using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace Assembly.Service
{
    public class ReflectionModel
    {
        public PropertyInfo[] _properties { get; set; }
        public object _model { get; set; }

        public ReflectionModel(object model)
        {
            _model = model;
            _properties = model.GetType().GetProperties();
        }

        public bool GetHtmlReadOnAttribute(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<HtmlReadAttribute>();
            return nret != null;
        }


        public bool GetHiddenInputAttribute(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<HiddenInputAttribute>();
            return nret != null;
        }

        // set tem botal sell adicional
        public bool GetHtmlBotaoSel(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<HtmlBotaoSel>();
            return nret != null;
        }

        // retorno tipoo de marcacao
        public string GetDataType(PropertyInfo property)
        {
            var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();

            if (dataTypeAttribute != null)
            {
                return dataTypeAttribute.DataType.ToString();
            }

            return null;
        }
        
        public string GetDisplayAttribute(PropertyInfo property)
        {
            var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            // Se não houver atributo de exibição, retorne o nome da propriedade como padrão.
            return property.Name;
        }

        public bool GetRequiredAttribute(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<RequiredAttribute>();
            return nret != null;
        }

        public bool GetReadOnlyAttribute(PropertyInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<ReadOnlyAttribute>();
            Console.WriteLine(" teste " + readOnlyAttribute);

            return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
        }

        //public bool GetReadOnlyAttribute(PropertyInfo property)
        //{
        //    var nret = property.GetCustomAttribute<ReadOnlyAttribute>();
        //    return nret != null;
        //}


    }


    // criar classe de marcadores especificos
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    //[AttributeUsage(AttributeTargets.Class)]
    public class HtmlReadAttribute : Attribute
    {
        public bool IsConditional { get; }
        public HtmlReadAttribute(bool isConditional)
        {
            IsConditional = isConditional;
        }

    }


    // criar classe de marcadores especificos
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class HtmlBotaoSel : Attribute
    {
        public bool IsConditional { get; }
        public HtmlBotaoSel(bool isConditional)
        {
            IsConditional = isConditional;
        }

    }



}
