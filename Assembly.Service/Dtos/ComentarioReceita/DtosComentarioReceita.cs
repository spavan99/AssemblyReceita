using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
     public class DtosComentarioReceita
    {

        public int Id { get; set; }
        public int IdReceita { get; set; }
        public string Comentario { get; set; }

        public int Avaliacao { get; set; }
   
        public int Aprovado { get; set; }
        //public ComentariosReceita Aprovado { get; set; }

    }
}
