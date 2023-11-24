using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assembly.Service
{
    public class DtosCategoriaFull
    {
        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "Id")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        public string NomeCategoria { get; set; }
    }
}
