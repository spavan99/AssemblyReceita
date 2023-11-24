using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages.Receita.Adm
{
    public class MenuADMModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserResolverService _userResolverService;

        public MenuADMModel(ILogger<IndexModel> logger, IHttpContextAccessor accessor, IUserResolverService userResolverService)
        {
            _logger = logger;
            this._accessor = accessor;
            this._userResolverService = userResolverService;
        }

        public string NameLogin { get; set; }
        public int IdLogin { get; set; }

        public void OnGet()
        {
 
            NameLogin = _userResolverService.GetName();
            IdLogin = _userResolverService.GetUserId();
        }
    }
}
