using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.Xml.Linq;

namespace Assembly.Receita.Pages.Receita.Adm
{
    public class CabecalhoADMModel : PageModel
    {
        /*  pensar como fazer depois
        [BindProperty]
        public PesquisaForm _pequisa { get; set; } = new();

        public CabecalhoADMModel()
        {
        }
        */


        public void OnGet()
        {
            /*  pensar como fazer depois
                _pequisa.alfa = "test alga";
                _pequisa.chavePesquisa = "chave de pesquisa";
                */

        }
    }


}

/*  pensar como fazer depois

public class PesquisaForm
{
public string chavePesquisa { get; set; }

public string alfa { get; set; }
}

*/