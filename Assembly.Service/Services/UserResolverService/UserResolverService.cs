using Assembly.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class UserResolverService : IUserResolverService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }
        public string GetEmail()
        {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                if (claim.Type == "email")
                {
                    return claim.Value;
                }
            }

            return null;
        }

        public string GetName()
        {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                if (claim.Type == "name")
                {
                    return claim.Value;
                }
            }

            return null; ;
        }

        public int GetUserId()
        {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                if (claim.Type == "sub")
                {
                    return int.Parse(claim.Value);
                }
            }

            return -1;
        }

        public bool isMaster()
        {
            var isMaster = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Master.ToString());
            return isMaster;
        }

        public bool isAdmin()
        {
            var isMaster = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Master.ToString());
            var isAdmin = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Admin.ToString());
            return ( isMaster  || isAdmin );
        }

        public bool isGerente()
        {
            var isMaster = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Master.ToString());
            var isAdmin = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Admin.ToString());
            var isGerente = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Gerente.ToString());
            return (isMaster || isAdmin || isGerente);
        }


        public bool isUsuario()
        {
            var isMaster = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Master.ToString());
            var isAdmin = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Admin.ToString());
            var isGerente = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Gerente.ToString());
            var isUsuario = _httpContextAccessor.HttpContext.User.HasClaim("role", TipoUsuarioEnum.Usuario.ToString());

            return (isMaster || isAdmin || isGerente || isUsuario);
        }

        public string isFuncao()
        {
            throw new NotImplementedException();
        }
    }
}
