using Assembly.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IService<TEntity, TDtos>
    {
    
       // interface generica crud
       
        // list geral 
        //  List<dynamic> GetAll();
        List<TDtos> GetAll();

         //retorna um especiico  primeira assinatura
        List<TDtos> GetById<Tvr>(Tvr id, string ncampo = null);

        // retirbn um especifoco com multiplos filtros  segunda assiantura
        List<TDtos> GetById(List<SQLDTOSPesquisa> nChave);

        //adiciona
        TEntity Add(TEntity obj);

        //Update
        bool Update(TEntity obj);
        bool Update(TEntity obj, int id);

        // delete id
        bool Delete(int id);

        // delete elemento todo - sobreposicao
        bool Delete(TEntity obj);

    }
}
