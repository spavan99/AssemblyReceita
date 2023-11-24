using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosReceitaFull
    {

        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "Id Receita")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int Id { get; set; }

        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "User Id")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Titulo da Receita")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Descriçao é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O campo deve ter no máximo 500 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Descriçao Receita")]
        public string Descricao { get; set; }

        // parei aqui contunua colocando as validaçoes
        [Display(Name = "Modo Preparo Receita")]
        public string Preparo { get; set; }

        [Display(Name = "Id Catgoria da Receita")]
        [HtmlBotaoSel(true)]
        public int IdCategoria { get; set; }

        [Display(Name = "Id Grau de Dificuldade da Receita")]
        [HtmlBotaoSel(true)]
        public int IdDificuldade { get; set; }

        //public int Tempo { get; set; }
        [Display(Name = "Tempo de Preparo")]
        public TempoPreparoEnum Tempo { get; set; }

        [Display(Name = "Nr pesssoas serve a receita")]
        public int ServePessoas { get; set; }

        [Display(Name = "Tipo Prato")]
        public TipoPratoEnum TipoPrato { get; set; }

        [HtmlRead(true)]
        [Display(Name = "Id foto receita")]
        public string fotoreceita { get; set; }

        [HtmlRead(true)]
        [Display(Name = "Status da Receita")]
        public ReceitaStatusEnum Status { get; set; }
    }
}
