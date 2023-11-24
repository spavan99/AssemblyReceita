using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;
//using System.ComponentModel;




namespace Assembly.Receita.Pages.Receita.Teste
{
    public class teste3Model : PageModel
    {

        public MeuDTO dados { get; set; }

        public void OnGet()
        {
           dados= new MeuDTO();
        
        }
    }



    public enum Status
    {
        [Display(Name = "Ativo")]
        Ativo,
        [Display(Name = "Inativo")]
        Inativo
    }

    public class MeuDTO
    {
        [Display(Name = "Nome do Usuário")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Idade")]
        [Range(18, 99, ErrorMessage = "A idade deve estar entre 18 e 99 anos.")]
        public int Idade { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        [Display(Name = "Campo Somente Leitura")]
        //[ReadOnly(true)]
        public string CampoSomenteLeitura { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CampoHidden { get; set; }
    }

}
