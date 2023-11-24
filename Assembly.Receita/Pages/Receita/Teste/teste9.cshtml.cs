using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages.Receita.Teste
{
    public class teste9Model : PageModel
    {
        public void OnGet()
        {
        }
    }


    public class MenuItem
    {
        public string Nome { get; set; }
        public string Rota { get; set; }
    }


}
