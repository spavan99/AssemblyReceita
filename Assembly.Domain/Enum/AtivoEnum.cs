using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assembly.Domain
{
    public enum AtivoEnum {
        [Display(Name = "Dados Inativo")]
        Ativo = 1,
        [Display(Name = "Dados Ativo")]
        Inativo = 2,
        [Display(Name = "Dados Cancelado")]
        Cancelado = 3

    }
}
