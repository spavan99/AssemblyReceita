using Assembly.Database;
using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _Repository;

        public CategoriaService(ICategoriaRepository pRepository)
        {
            this._Repository = pRepository;

        }
        public Categoria Add(Categoria obj)
        {
            obj = _Repository.Add(obj);
            if (obj is not null)
            {
                return obj;
            }else
            { 
                return null;
            }
        }

        public string AddFull(DtosCategoriaFull obj)
        {
            // checampo estao padrao

            if (obj.NomeCategoria == null)
            {
                return "Campo nulo";
            }
            else if (string.IsNullOrWhiteSpace(obj.NomeCategoria))
            {
                return "Campo inválido, deve conter pelo menos 3 caracteres";
            }
            else
            {
                // conversao full para usuario
                Categoria cadastrar = ParseShared.ParseClassDtos<Categoria, DtosCategoriaFull>(obj);

                var nret = Add(cadastrar);
                if( nret != null) {
                    return "Cadastro com Sucesso";                
            
                };
                return "Nao Cadastrado";
            }
            return "Dados nulo";
        }

        public bool Delete(int id)
        {
            if (true)
            {
                return (_Repository.Delete(id));
            }
            return false;

        }

        public bool Delete(Categoria obj)
        {
            if (true)
            {
                return (_Repository.Delete(obj.Id));
            }
            return false;

        }
        public List<DtosCategoriaFull> GetAll()
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosCategoriaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosCategoriaFull>(nLstAchou);

            return nlista;
            
        }

        public List<DtosCategoriaFull> GetById<Tvr>(Tvr id, string ncampo = null)
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosCategoriaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosCategoriaFull>(nLstAchou);

            return nlista;

        }

        public List<DtosCategoriaFull> GetById(List<SQLDTOSPesquisa> nChave)
        {
            // where multiplos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosCategoriaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosCategoriaFull>(nLstAchou);

            return nlista;

        }

        public bool Update(Categoria obj)
        {
            if (true)
            {
                return (_Repository.Update(obj));
            }
            return false;
        }

        public bool Update(Categoria obj, int id)
        {
            // checar id tem base  
            if (true)
            {
                // equipara o obejto recebido ao id a alterar e efetua a alteracao
                obj.Id = id;
                return (_Repository.Update(obj));
            }
            return false;
        }

        // clasee especifica somente tem iuserservice 
        public bool UpdateFull(DtosCategoriaFull obj)
        {
            // conversao full para usuario
            Categoria alterado = ParseShared.ParseClassDtos<Categoria, DtosCategoriaFull>(obj);
            //Usuario alterado = ParseShared.ParseClassDtos<Usuario>(obj);
            return Update(alterado);

        }

    }
}
