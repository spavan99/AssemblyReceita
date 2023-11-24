using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosItensReceitaFull
    {
        [Display(Name = "Id")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Display(Name = "Numero Receita")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int IdReceita { get; set; }

        [Required(ErrorMessage = "O Ingrediente é obrigatório.")]
        //[MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        //[MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Ingrediente da Receita")]
        public string Ingredientes { get; set; }

        [Required(ErrorMessage = "O campo Qtde é obrigatório.")]
        //[MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        //[MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Qtde na Receita Ex: 1Kg / 1/2Xicara")]
        public string QtdeIngredientes { get; set; }


    }
}
