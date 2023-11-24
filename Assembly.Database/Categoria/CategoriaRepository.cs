using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public string _tabela = MapDataBase.GetCategoriaTable();
        
        //private readonly ICategoriaRepository _Repository;
        //public CategoriaRepository(ICategoriaRepository Repository)
        //{
        //    _Repository = Repository;
        //}

        public Categoria Add(Categoria obj)
        {
            // campo excluir para insert geralmente Id (gerado automatico)
            string[] campoexcluir = { "Id" };

            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(obj, campoexcluir);

            // monta a string sql
            string _sql = StringSQL.SQLInsetRetorno(campos, _tabela );

            //inserir registro
            var ret = SQLCRUDInsert.SQLInset<Categoria>(_sql, campos, obj, campoexcluir);

            // verifica objeto null ou tem numero
            if (ret is not null)
            {
                obj.Id = Convert.ToInt32(ret);
                return obj;
            }
            else
            {
                return null;
            }

        }

        public bool Delete(int id)
        {
            // define campo
            string nCampo = "Id";

            // string sql
            string _sql = StringSQL.SQLDeleteRetorno(nCampo, _tabela);

            //inserir registro
            bool deletado = SQLCRUDDelete.SQLDelete<int>(_sql, nCampo, id);
            return deletado;

        }

        public List<dynamic> GetAll(List<SQLDTOSPesquisa> nChave, string _sqlComplexa = null)
        {
            // cria matriz para pegar campos//varia conform o repository
            Categoria nbranco = new Categoria();

            // campo excluir // deixa todos dominio // dtos e feito no servico
            string[] campoexcluir = { };

            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(nbranco, campoexcluir);

            // string sql // monta atumatico ou coloca na mao
            string _sql;
            if (_sqlComplexa is not null)
            {
                _sql = _sqlComplexa;
            }
            else
            {
                _sql = StringSQL.SQLSelectBasicRetorno(campos, campos, _tabela, nChave);
            }

            //buscar consulta dados e receber lista dymaic
            List<dynamic> lista = SQLCRUDSelect.SQLSelectPar_Arry(_sql, nChave);
            return lista;
        }

        public List<dynamic> GetAll<Tvr>(string nChave, Tvr nValor, string _sqlComplexa = null)
        {
            // cria matriz para pegar campos//varia conform o repository
            Categoria nbranco = new Categoria();

            // campo excluir // deixa todos dominio // dtos e feito no servico
            string[] campoexcluir = { };


            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(nbranco, campoexcluir);

            // string sql // monta atumatico ou coloca na mao
            string _sql;
            if (_sqlComplexa is not null)
            {
                _sql = _sqlComplexa;
            }
            else
            {
                _sql = StringSQL.SQLSelectBasicRetorno(campos, campos, _tabela, nChave);
            }

            //buscar consulta dados e receber lista dymaic
            List<dynamic> lista = SQLCRUDSelect.SQLSelectPar_1<Tvr>(_sql, nChave, nValor);

            return lista;
        }

        public bool Update(Categoria obj)
        {
            // campo excluir para insert geralmente Id (gerado automatico)
            string[] campoexcluir = { "Id" };

            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(obj, campoexcluir);

            // monta a string sql
            string _sql = StringSQL.SQLUpdateRetorno(campos, _tabela, "Id");

            //alterar o regritro
            var ret = SQLCRUDUpdate.SQLUpdate<Categoria, int>(_sql, campos, obj, campoexcluir, "Id", obj.Id);

            return ret;

        }
    }
}
