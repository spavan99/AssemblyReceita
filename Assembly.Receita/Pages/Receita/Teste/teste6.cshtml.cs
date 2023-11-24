using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assembly.Receita.Pages.Receita.Teste
{
    public class teste6Model : PageModel
    {
        public ReflectionViewModel DadosViewModel { get; set; }
        public void OnGet()
        {
            var meuModelo = new MeuDTO2();  // Substitua pelo seu modelo real
            DadosViewModel = new ReflectionViewModel(meuModelo);
        }
    }



    public enum Status2
    {
        [Display(Name = "Ativo 44")]
        Ativo,
        [Display(Name = "Inativo")]
        Inativo,
        [Display(Name = "Alfa teste teste")]
        Alfa_Inativo

    }

    public class MeuDTO2
    {
       
        [HtmlReadOn(true)]
        [ReadOnly(true)]
        [Display(Name = "Nome do Usuário")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Idade kkkkkkk")]
        [Range(18, 99, ErrorMessage = "A idade deve estar entre 18 e 99 anos.")]
        public int Idade { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "alfa Status")]
        public Status2 Status { get; set; }

        [Display(Name = "test Campo Somente Leitura")]
        [HtmlReadOn(true)]    // somente leitura marcador personalizado
        public string CampoSomenteLeitura { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CampoHidden { get; set; }
    }



    // cricao atributo especifico para controlar campo somente leitura
    //[AttributeUsage(AttributeTargets.Property)]
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    //[AttributeUsage(AttributeTargets.Class)]
    public class HtmlReadOnAttribute : Attribute
    {
        public bool IsConditional { get; }
        public HtmlReadOnAttribute(bool isConditional)
        {
            IsConditional = isConditional;
        }

    }

    public class ReflectionViewModel
    {
        public PropertyInfo[] _properties { get; set; }
        public object _model { get; set; }

        public ReflectionViewModel(object model)
        {
            _model = model;
            _properties = model.GetType().GetProperties();
        }

        public bool GetHtmlReadOnAttribute(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<HtmlReadOnAttribute>();
            return nret != null;
        }


        public bool GetHiddenInputAttribute(PropertyInfo property)
        {
            var nret = property.GetCustomAttribute<HiddenInputAttribute>();
            return nret != null;
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



}



