using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IAuthService
    {
        Task<bool> Login(DtosUsuarioLogin loginDto);
        Task Logout();
        void Register(DtosUsuarioLogin registerDto);
    }
}
