using Assembly.Database;
using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assembly.Receita.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authenticationService;
        private readonly IUserRepository _Repository;
        private readonly IUserService _Service;

        [BindProperty]
        public DtosUsuarioRegister RegisterDto { get; set; }

        public RegisterModel(IAuthService authenticationService, IUserRepository Repository, IUserService Service)
        {
            this._authenticationService = authenticationService;
            _Repository = Repository;
            _Service = Service;
        }


        public void OnGet()
        {
            


        }

        public  IActionResult OnPost()
        {
            // verificar senha são iguais
            if (!RegisterDto.Senha.Equals(RegisterDto.SenhaOk)){
                TempData["My9Mensagem"] = "Senhas não sao Iguais";
                return RedirectToAction("/Login/Register");
            }

            var result = _Service.RegisterUser(RegisterDto);
            if( result is not null )
            {
                TempData["My9Mensagem"] = result;
                return RedirectToAction("/Login/Register");
            }
            return RedirectToAction("/Login/Login");


        }
    }
}
