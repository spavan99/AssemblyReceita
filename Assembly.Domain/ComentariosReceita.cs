using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Domain
{
    public class ComentariosReceita : Base
    {
        public int IdReceita { get; set; }
        public string Comentario { get; set; }
        
        public int Avaliacao { get; set; }
        
        //public int Aprovado { get; set; }
        public ComentarioReceitaEnum Aprovado { get; set; }

    }
}
