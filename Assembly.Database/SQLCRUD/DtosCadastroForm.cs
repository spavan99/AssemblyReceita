using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class DtosCadastroForm
    {
        public string nomeLabel { get; set; }
        public string campoCadastro { get; set; }
        public Type txtType { get; set; }
    
        public string tipoCampo { get; set; }
        public notNullEnum requeridoSN { get; set; } 
    
    }
}

public enum notNullEnum
{
    p_Sim = 0,
    p_Nao = 1
}

public enum txtTypeEnum
{
    p_text = 1,
    p_pasword = 2,
    p_checbox =3,
    p_radio = 4,
    p_number = 5,
    p_date = 6,
    p_email = 7,
    p_url = 8,
    p_file = 9,
    p_hidden = 10,
    p_submit = 11,
    p_button = 12
     
}