using Assembly.Database;
using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class DificuldadeService : IDificuldadeService
    {

        private readonly IDificuldadeRepository _Repository;

        public DificuldadeService(IDificuldadeRepository pRepository)
        {
            this._Repository = pRepository;

        }
        public Dificuldade Add(Dificuldade obj)
        {
            obj = _Repository.Add(obj);
            if (obj is not null)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

        public string AddFull(DtosDificuldadeFull obj)
        {
            // checampo estao padrao

            if (obj.GrauDificuldade == null)
            {
                return "Campo nulo";
            }
            else if (string.IsNullOrWhiteSpace(obj.GrauDificuldade))
            {
                return "Campo inválido, deve conter pelo menos 3 caracteres";
            }
            else
            {
                // conversao full para usuario
                Dificuldade cadastrar = ParseShared.ParseClassDtos<Dificuldade, DtosDificuldadeFull>(obj);

                var nret = Add(cadastrar);
                if (nret != null)
                {
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

        public bool Delete(Dificuldade obj)
        {
            if (true)
            {
                return (_Repository.Delete(obj.Id));
            }
            return false;

        }

        public List<DtosDificuldadeFull> GetAll()
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosDificuldadeFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosDificuldadeFull>(nLstAchou);

            return nlista;

        }

        public List<DtosDificuldadeFull> GetById<Tvr>(Tvr id, string ncampo = null)
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosDificuldadeFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosDificuldadeFull>(nLstAchou);

            return nlista;
        }

        public List<DtosDificuldadeFull> GetById(List<SQLDTOSPesquisa> nChave)
        {
            // where multiplos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosDificuldadeFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosDificuldadeFull>(nLstAchou);

            return nlista;
        }

        public bool Update(Dificuldade obj)
        {
            if (true)
            {
                return (_Repository.Update(obj));
            }
            return false;

        }

        public bool Update(Dificuldade obj, int id)
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

        public bool UpdateFull(DtosDificuldadeFull obj)
        {
            // conversao full para usuario
            Dificuldade alterado = ParseShared.ParseClassDtos<Dificuldade, DtosDificuldadeFull>(obj);
            //Usuario alterado = ParseShared.ParseClassDtos<Usuario>(obj);
            return Update(alterado);

        }
    }
}
