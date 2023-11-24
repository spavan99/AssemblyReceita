using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assembly.Service
{ 
    // nao necessario
    public interface IUserService : IService<Usuario, DtosUsuario>
    {
        public bool Login(string username, string password);

        bool UpdateFull(DtosUsuarioFull obj);

        Usuario AddFull(DtosUsuarioFull obj);

        string RegisterUser(DtosUsuarioRegister user);

    }
}
