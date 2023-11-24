using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosReceitaVitrine
    {
        public int Id { get; set; }
       // public int IdUser { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Preparo { get; set; }
        public int IdCategoria { get; set; }
        public int IdDificuldade { get; set; }
        
        public int Tempo { get; set; }
        //public TempoPreparoEnum Tempo { get; set; }

        public int ServePessoas { get; set; }

        public int TipoPrato { get; set; }
        //public TipoPratoEnum TipoPrato { get; set; }
        
        public int Status { get; set; }
        //public ReceitaStatusEnum Status { get; set; }
        public string fotoreceita { get; set; }
        public int favorito { get; set; }

    }
}
