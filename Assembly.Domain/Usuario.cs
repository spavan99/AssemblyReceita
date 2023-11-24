using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class Usuario : Base
    {
       
        //[maxr]  validaçoes do banco
        public string Name { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }

        // public int Ativo { get; set; }
        public AtivoEnum Ativo { get; set; }
        
        //public int TipoUsuario { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }
       
    }
}
