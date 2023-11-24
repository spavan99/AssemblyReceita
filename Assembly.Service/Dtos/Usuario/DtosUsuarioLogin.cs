using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosUsuarioLogin
    {
           
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public AtivoEnum Ativo { get; set; }
        

    }
}
