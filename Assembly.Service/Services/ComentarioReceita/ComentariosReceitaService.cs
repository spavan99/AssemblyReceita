using Assembly.Database;
using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class ComentariosReceitaService : IComentariosReceitaService
    {


        private readonly IComentariosReceitaRepository _Repository;

        public ComentariosReceitaService(IComentariosReceitaRepository pRepository)
        {
            this._Repository = pRepository;

        }
        
        public ComentariosReceita Add(ComentariosReceita obj)
        {
            obj = _Repository.Add(obj);
            if( obj is not null) {
                return obj;
            }else{
                return null;
            }

        }

        public bool Delete(int id)
        {
            if (true)
            {
                return (_Repository.Delete(id));
            }
            return false;

        }

        public bool Delete(ComentariosReceita obj)
        {
            // fazer a checagem se existe
            // pegar objeto e passa o id para deletar
            if (true)
            {
                return (_Repository.Delete(obj.Id));
            }
            return false;
        }

        
        public List<DtosComentarioReceita> GetAll()
        {
            // antes de whrw multipos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosComentarioReceita> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosComentarioReceita>(nLstAchou);

            return nlista;
            //return (_Repository.GetAll<int>(null, 0));

        }

        public List<DtosComentarioReceita> GetById<Tvr>(Tvr id, string? ncampo = null)
        {

            // utiliza GetAll assinatura where unico
 
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosComentarioReceita> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosComentarioReceita>(nLstAchou);
            return nlista;
  
        }

        //  criar GetById comente para lista vario whwre
        // passa para o contrutor outra assiantura
        public List<DtosComentarioReceita> GetById(List<SQLDTOSPesquisa> nChave)
        {
            // utiliza GetAll assinatura where multiplos

            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosComentarioReceita> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosComentarioReceita>(nLstAchou);

            return nlista;

        }

        public bool Update(ComentariosReceita obj)
        {
            if (true)
            {
                return (_Repository.Update(obj));
            }
            return false;

        }

        public bool Update(ComentariosReceita obj, int id)
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
    }
}
