using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class ItensReceita : Base
    {
        public int IdReceita { get; set; }
        public string Ingredientes { get; set; }
        public string QtdeIngredientes { get; set; }
    }
}
