using Assembly.Database;
using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class ItensReceitaService : IItensReceitaService
    {

        private readonly IItensReceitaRepository _Repository;

        public ItensReceitaService(IItensReceitaRepository pRepository)
        {
            this._Repository = pRepository;

        }

        public ItensReceita Add(ItensReceita obj)
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

        public string AddFull(DtosItensReceitaFull obj)
        {
            // checampo estao padrao

            if (obj.Ingredientes == null)
            {
                return "Campo nulo";
            }
            else if ((string.IsNullOrWhiteSpace(obj.Ingredientes) && string.IsNullOrWhiteSpace(obj.QtdeIngredientes)) )
            {
                return "Inclusao NAO REALIZADA - Campo inválido, deve conter pelo menos 3 caracteres, codigo receita etc etc";
            }
            else
            {
                // conversao full para usuario
                ItensReceita cadastrar = ParseShared.ParseClassDtos<ItensReceita, DtosItensReceitaFull>(obj);
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

        public bool Delete(ItensReceita obj)
        {
            if (true)
            {
                return (_Repository.Delete(obj.Id));
            }
            return false;
        }

        public List<DtosItensReceitaFull> GetAll()
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosItensReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosItensReceitaFull>(nLstAchou);

            return nlista;
        }

        public List<DtosItensReceitaFull> GetById<Tvr>(Tvr id, string ncampo = null)
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosItensReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosItensReceitaFull>(nLstAchou);

            return nlista;

        }

        public List<DtosItensReceitaFull> GetById(List<SQLDTOSPesquisa> nChave)
        {
            // where multiplos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosItensReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosItensReceitaFull>(nLstAchou);

            return nlista;
        }

        public bool Update(ItensReceita obj)
        {
            if (true)
            {
                return (_Repository.Update(obj));
            }
            return false;
        }

        public bool Update(ItensReceita obj, int id)
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

        public bool UpdateFull(DtosItensReceitaFull obj)
        {
            // conversao full para usuario
            ItensReceita alterado = ParseShared.ParseClassDtos<ItensReceita, DtosItensReceitaFull>(obj);
            return Update(alterado);

        }
    }
}
