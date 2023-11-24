using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public interface IRepository<TEntity>
    {

        // interface generica crud

        // list geral 

        // select com varia condiçoes Whwre
        List<dynamic> GetAll(List<SQLDTOSPesquisa> nChave, string _sqlComplexa = null);

        // somente select com uma condiçao where
        List<dynamic> GetAll<Tvr>(string nChave, Tvr nValor, string _sqlComplexa = null);
    
          //adiciona
        TEntity Add(TEntity obj);

        //Update
        bool Update(TEntity obj);

        // delete id somente base
        bool Delete(int id);


    }
}
