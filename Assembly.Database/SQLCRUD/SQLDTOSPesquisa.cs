using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class SQLDTOSPesquisa
    {
        public string nCampo { get; set; }   // nome do campo select
        public SQLtypeEnum nType { get; set; }   // tipo campo int/string/bool

        public string nValor { get; set; }  // vlor do campo para buscar

        public SQLoperEnum operacao { get; set; } // 

        public string descPesquisa { get; set; }
    }


    public enum SQLoperEnum
    {
        p_and = 1,
        p_or = 2,
        p_fim = 3,
        p_nulo = 4
    }

    public enum SQLtypeEnum
    {
        type_int = 1,
        type_string = 2,
        type_bool = 3
    }


}
