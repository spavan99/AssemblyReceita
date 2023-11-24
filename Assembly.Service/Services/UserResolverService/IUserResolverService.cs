using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IUserResolverService
    {
        int GetUserId();

        string GetName();

        string GetEmail();

        string isFuncao();

        bool isMaster();
        bool isAdmin();
        bool isGerente();
        bool isUsuario();

    }
}
