using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class ErroMensagens
    {
        public List<CodErroMSG> codErroMSGs { get; set; }
        public ErroMensagens() { }
        public bool isTemErro( string rotina = "")
        {
            bool isTemErro = false;
            // for erro geral ou por rotina e ver se tem true ou false
            foreach( var verErro in codErroMSGs)
            {
                if (rotina.Equals(""))
                {
                    if( verErro.erromsg > 0)
                    {
                        isTemErro = true;
                        break;
                    }
                }
                else
                {
                    if(rotina.ToUpper().Equals(verErro.chave.ToUpper()))
                    {
                        if (verErro.erromsg > 0)
                        {
                            isTemErro = true;
                            break;
                        }
                    }

                }
            }
            return (isTemErro);
        }

    }

}


//dtos/classe erro
public class CodErroMSG
{
    public int erromsg { get; set; }   // 0 ok  1 erro  2  mensagem

    public string chave { get; set; }  // progrma que deu erro // chave de acesso
    public string mensagem { get; set; }  // mensagem a exibir

    public string local { get; set; }   // local erro mensagem


}


