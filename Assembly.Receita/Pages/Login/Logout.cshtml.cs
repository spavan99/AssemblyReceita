using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages.Login
{
    [Authorize()] // Authenticated
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;

        public LogoutModel(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<IActionResult> OnGet()
        {
            await _authService.Logout();
            // envia para o index
            return Redirect("/");
        }
    }
}
