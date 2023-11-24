using Assembly.Database;
using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages
{
    public class IndexADMModel : PageModel
    {
        public List<DtosReceitaVitrine> lstVitrine { get; set; } = new List<DtosReceitaVitrine>();


        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public void OnGet()
        {
            // obtem dados da vitirne
            ReceitaService receitaVirine = new ReceitaService(new ReceitaRepository());
            lstVitrine = receitaVirine.ReceitaVitrine();


        }
    }
}