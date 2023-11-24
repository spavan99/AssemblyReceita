using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Service
{
    public class DtosUsuarioFull
    {

        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "Id")]
        [HtmlRead(true)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Primeiro Nome")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "Ultimno Nome/Sobrenome")]
        public string SobreNome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        [Display(Name = "Endereço de Email")]
        ////[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo UserName é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo 50 caracteres.")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password, ErrorMessage = "A senha deve ser válida.")]
        [Display(Name = "Senha de Acesso")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirma a senha ")]
        [DataType(DataType.Password, ErrorMessage = "A senha deve igual a anterior.")]
        [Display(Name = "Digite Novamente Senha")]
        public string SenhaConfirmacao { get; set; }

        //[ReadOnly(true)]
        [Display(Name = "Usuario Ativo")]
        //public int Ativo { get; set; }
        public AtivoEnum Ativo { get; set; }

        //[ReadOnly(true)]
        [Display(Name = "Tipo Usuario")]
        //public int TipoUsuario { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }


    }
}
