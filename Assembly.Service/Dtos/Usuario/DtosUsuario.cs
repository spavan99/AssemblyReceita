using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DtosUsuario
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string SobreNome { get; set; }
        //public string Email { get; set; }
        //public string UserName { get; set; }
        ////public string Senha { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public AtivoEnum Ativo { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }



    }
}
