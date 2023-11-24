
using System.ComponentModel;

namespace Assembly.Domain
{
    public enum TipoPratoEnum {
        [Description("Prato Vegetariano")]
        Venetariano = 1,
        [Description("Prato Vegano")]
        Vegano = 2,
        [Description("Prato apreciadores Carne")]
        Carnivoro = 3 ,
        [Description("Prato sem Glutem")]
        Sem_Glutem = 4,
        [Description("Cozinha Italiana")]
        Italia = 5,
        [Description("Cozinha Chineza")]
        China = 6

    }


}
