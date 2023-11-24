
using System.ComponentModel;

namespace Assembly.Domain
{
    public enum TempoPreparoEnum {

        [Description("Muito Rapido")]
        Muito_Baixo = 1,
        [Description("Rapido")]
        Baixo = 2,
        [Description("Tempo Médio")]
        Medio = 3,
        [Description("Demorado")]
        Alto = 4,
        [Description("Muito Demorado")]
        Muito_Alto = 5 
    }
}
