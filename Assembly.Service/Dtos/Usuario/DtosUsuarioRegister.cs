using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosUsuarioRegister
    {
        public string Name { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public string SenhaOk { get; set; }

    }
}
