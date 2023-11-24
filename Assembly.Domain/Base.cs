using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class Base
    {
       public int Id { get; set; }
       public DateTime DataCadastro => DateTime.Now;

    }
}
