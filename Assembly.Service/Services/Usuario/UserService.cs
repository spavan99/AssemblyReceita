using Assembly.Database;
using Assembly.Domain;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _Repository;

        public UserService(IUserRepository pRepository) {
            this._Repository = pRepository;

        }
        public Usuario Add(Usuario obj)
        {
            obj = _Repository.Add(obj);
            if( obj is not null) {
                return obj;
            }else{
                return null;
            }
        }

        public Usuario AddFull(DtosUsuarioFull obj)
        {
             
            // validçoes - userme nao existe / email nao exite / outor

            // conversao full para usuario
            Usuario cadastrar = ParseShared.ParseClassDtos<Usuario, DtosUsuarioFull>(obj);

            // sempre cadastra o usuario como inativo e usuario
            cadastrar.TipoUsuario = TipoUsuarioEnum.Usuario;
            cadastrar.Ativo = AtivoEnum.Inativo;
            
            return Add(cadastrar);
        }

        public bool Delete(int id)
        {
            // fazer cehcagem exsiste somente quiser faze algo antes pois
            // se nao encontrar as classe ja retorna false 
            // vale para todos os itens... por exemplo na alteracao pode pegar
            //os dados e mostrar antes de alterar
            if(true) {
              return ( _Repository.Delete(id));
            }
            return false;
        }

        public bool Delete(Usuario obj)
        {
            // fazer a checagem se existe
            // pegar objeto e passa o id para deletar
            if (true){
               return (_Repository.Delete(obj.Id));
            }
            return false;
        }

        public List<DtosUsuario> GetAll()
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosUsuario> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosUsuario>(nLstAchou);

            return nlista;

           // return (_Repository.GetAll<int>(null, 0));
        }

        public List<DtosUsuario> GetById<Tvr>(Tvr id, string? ncampo = null)
        {
            //  mudar futuro para devolver somente um elemento e nao uma lista
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosUsuario> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosUsuario>(nLstAchou);

            return nlista;
        }

        public List<DtosUsuario> GetById(List<SQLDTOSPesquisa> nChave)
        {

            // where multiplos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosUsuario> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosUsuario>(nLstAchou);

            return nlista;
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario obj)
        {
            if (true){
                return (_Repository.Update(obj));
            }
            return false;

        }

        public bool Update(Usuario obj, int id)
        {
            // checar id tem base  
            if (true)
            {
                // equipara o obejto recebido ao id a alterar e efetua a alteracao
                obj.Id= id;
                return (_Repository.Update(obj));
            }
            return false;
        }

        // clasee especifica somente tem iuserservice 
        public bool UpdateFull(DtosUsuarioFull obj)
        {
            // conversao full para usuario
            Usuario alterado = ParseShared.ParseClassDtos<Usuario, DtosUsuarioFull>(obj);
            //Usuario alterado = ParseShared.ParseClassDtos<Usuario>(obj);
            return Update(alterado);

        }


        public string RegisterUser( DtosUsuarioRegister user)
        {
            // verificar se o usurname nao existe
            var resultUserName = GetById<string>(user.UserName, "UserName");
            if(resultUserName.Count > 0)
            {
                return "Registro não realizado - Username já existente";
            }

            // verificar se o EMAIL nao existe
            var resulteMAIL = GetById<string>(user.Email, "Email");
            if (resultUserName.Count > 0)
            {
                return "Registro não Realizado - Username já existente";
            }

            // verificar se as senha sao iguais  // fazer a conversao para hasck
            if ( ! user.Senha.Equals(user.SenhaOk))
            {
                return "Registro não Realizado - Senhas diferentes";
            }
            // parse usuario e efetiva cadastro
            Usuario novoUsuario = new Usuario();

            novoUsuario.Name= user.Name;
            novoUsuario.SobreNome= user.SobreNome;
            novoUsuario.UserName= user.UserName;
            novoUsuario.Email= user.Email;
            novoUsuario.Senha= user.Senha;
            novoUsuario.Ativo = AtivoEnum.Ativo;
            novoUsuario.TipoUsuario = TipoUsuarioEnum.Usuario;

            var resultAdd = Add(novoUsuario);
            if( resultAdd is null)
            {
                return "Registro nao Realizado - erro ao cadatra Base";
            }
            return null;
        }
    }
}
