using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages
{
    public class IndexOldModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexOldModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}