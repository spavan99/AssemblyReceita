using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosDificuldadeFull
    {


        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "Id")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo 50 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Grau de Dificuldade de Realizacao Receita")]
        public string GrauDificuldade { get; set; }
    }
}
