using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class FotoReceita : Base
    {
        public int IdReceita { get; set; }

        public string ArquivoFoto { get; set; }
       
        //public int Principal { get; set; }
        public FotoEnum Principal { get; set;  }
    }
}
