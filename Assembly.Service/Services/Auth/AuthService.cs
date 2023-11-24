using Assembly.Database;
using Assembly.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assembly.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserRepository _Repository;
        private readonly IUserService _Service;
        //private readonly IPasswordHasher<User> _hasher;
        //private readonly IData _data;

        //public AuthService(IHttpContextAccessor accessor, IPasswordHasher<User> hasher, IData data)
        public AuthService(IHttpContextAccessor accessor, IUserRepository Repository, IUserService Service)
        {
            _accessor = accessor;
            _Repository = Repository;
            _Service = Service;
            //this._hasher = hasher;
            //this._data = data;
        }

        public async Task<bool> Login(DtosUsuarioLogin LoginDto)
        {

            Usuario foundUser = new Usuario();
            // achar usuario na base 
            var achou = _Service.GetById<string>(LoginDto.UserName, "UserName");
            if (achou.Count == 1)
            {
                if (achou[0].UserName.ToLower().Equals(LoginDto.UserName.ToLower()))
                {
                    var propriedadesOrigem = achou[0].GetType().GetProperties();
                    var propriedadesDestino = foundUser.GetType().GetProperties();
                    foreach (var propriedadeDestino in propriedadesDestino)
                    {
                        var propriedadeOrigem = propriedadesOrigem.FirstOrDefault(p => p.Name == propriedadeDestino.Name);
                        if (propriedadeOrigem != null && propriedadeOrigem.PropertyType == propriedadeDestino.PropertyType)
                        {
                            propriedadeDestino.SetValue(foundUser, propriedadeOrigem.GetValue(achou[0]));
                        }
                    }
                }
            } else { return false; }


            // verifica usuario esta inativo
            if( foundUser.Ativo == AtivoEnum.Inativo ) {
                return false;
            }

            // verificar senha com hashed
            //PasswordVerificationResult senhaOK = _hasher.VerifyHashedPassword(foundUser, foundUser.Password, loginDto.Password);
            //if (senhaOK == PasswordVerificationResult.Failed)
            //{
            //    return false;
            //}

            //verificar senha sem hasck
            if (! foundUser.Senha.ToLower().Equals(LoginDto.Senha.ToUpper()))
            {
                return false;
            }

            var claims = new List<Claim>()
            {
                new("sub", foundUser.Id.ToString()),
                new("id", foundUser.Id.ToString()),
                new("name", foundUser.Name),
                new("email", foundUser.Email),
                new("role", foundUser.TipoUsuario.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await _accessor.HttpContext.SignInAsync(claimsPrincipal);

            return true;
        }

        public async Task Logout()
        {
             await _accessor.HttpContext.SignOutAsync();
        }

        public void Register(DtosUsuarioLogin registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
