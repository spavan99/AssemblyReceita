using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class FavoritosReceita : Base
    {
        public int IdUsuario { get; set; }
        public int IdReceita { get; set; }
    }
}
