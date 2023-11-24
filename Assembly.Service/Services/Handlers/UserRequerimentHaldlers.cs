using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    //public class UserRequerimentHaldlers
    //{
    //}
    public class UserRequirement : IAuthorizationRequirement
    {

    }

    public class UserRequirementHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly IUserResolverService _userResolverService;

        public UserRequirementHandler(IUserResolverService userResolverService)
        {
            this._userResolverService = userResolverService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var isUser = _userResolverService.isUsuario();

            var isMaster = _userResolverService.isMaster();
            var isAdmin = _userResolverService.isAdmin();
            var isGerente = _userResolverService.isGerente();
            var isUsuario = _userResolverService.isUsuario();
 
            if (isMaster)
            {
                context.Succeed(requirement);
            }
            else if (isAdmin)
            {
                context.Succeed(requirement);
            }
            else if (isGerente)
            {
                context.Succeed(requirement);
            }
            else if (isUsuario)
            {
                context.Succeed(requirement);
            }
            else
            {
                // Se nenhum dos papéis se aplica, falha na autorização.
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
