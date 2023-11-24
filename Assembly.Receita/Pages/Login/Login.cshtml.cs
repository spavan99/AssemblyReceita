using Assembly.Database;
using Assembly.Domain;
using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Xml.Linq;

namespace Assembly.Receita.Pages.Login
{
    public class LoginModel : PageModel
    {
 
        private readonly IAuthService _authenticationService;
        private readonly IUserRepository _Repository;
        private readonly IUserService _Service;

        [BindProperty]
        public DtosUsuarioLogin LoginDto { get; set; }

        public LoginModel(IAuthService authenticationService, IUserRepository Repository, IUserService Service)
        {
            this._authenticationService = authenticationService;
            _Repository = Repository;
            _Service = Service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var isSuccess = await _authenticationService.Login(LoginDto);

            if(! isSuccess)
            {
                return Redirect("/Login/ErrorPage");
            }

            return Redirect("/IndexADM");
            
            //if (!isSuccess)
            //{
            //    return Redirect("/Error"); // user page
            //}

            //if (_userResolverService.IsAdmin())
            //{
            //    return Redirect("/Admin");
            //}

            //if (_userResolverService.IsUser())
            //{
            //    return Redirect("/User");
            //}


        }

    }
}
