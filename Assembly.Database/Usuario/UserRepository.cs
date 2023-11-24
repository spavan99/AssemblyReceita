using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;



namespace Assembly.Database
{
    public class UserRepository : IUserRepository
    {

     //  private readonly IDbConnection _conexao;

     //   public UserRepository(IDbConnection conexao)
     //   {
     //       _conexao = new SqlFactory().GetSqlConnection();
     //   }

        public Usuario Add(Usuario obj)
        {
            // campo excluir para insert geralmente Id (gerado automatico)
            string[] campoexcluir = { "Id" };

            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(obj, campoexcluir);

            // monta a string sql
            string _sql = StringSQL.SQLInsetRetorno(campos, MapDataBase.GetUsuarioTable());

            //inserir registro
            var ret = SQLCRUDInsert.SQLInset<Usuario>(_sql, campos, obj, campoexcluir);
            
            // verifica objeto null ou tem numero
            if( ret is not null) {
                obj.Id = Convert.ToInt32(ret);
                return obj;
            }else {
                return null;
            }

        }

        // delete somente pelo id no repositiry
        public bool Delete(int id)
        {
            // define campo
            string nCampo = "Id";

            // string sql
            string _sql = StringSQL.SQLDeleteRetorno(nCampo, MapDataBase.GetUsuarioTable());

            //inserir registro
            bool deletado = SQLCRUDDelete.SQLDelete<int>(_sql, nCampo, id);
            return deletado;

        }

        public List<dynamic> GetAll<Tvr>(string nChave, Tvr nValor, string _sqlComplexa = null)
        {

            // cria matriz para pegar campos//varia conform o repository
            Usuario nbranco= new Usuario();

            // campo excluir // deixa todos dominio // dtos e feito no servico
            string[] campoexcluir = { };


            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(nbranco, campoexcluir);

            // string sql // monta atumatico ou coloca na mao
            string _sql;
            if ( _sqlComplexa is not null)
            {
                _sql = _sqlComplexa;
            }else {
               _sql = StringSQL.SQLSelectBasicRetorno(campos, campos, MapDataBase.GetUsuarioTable(), nChave);
            }

            //buscar consulta dados e receber lista dymaic
            List<dynamic> lista = SQLCRUDSelect.SQLSelectPar_1<Tvr>(_sql, nChave, nValor);

            return lista;
        }

        // select where multimo 
        public List<dynamic> GetAll(List<SQLDTOSPesquisa> nChave, string _sqlComplexa = null)
        {
            // cria matriz para pegar campos//varia conform o repository
            Usuario nbranco = new Usuario();

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
                _sql = StringSQL.SQLSelectBasicRetorno(campos, campos, MapDataBase.GetUsuarioTable(), nChave);
            }

            //buscar consulta dados e receber lista dymaic
            List<dynamic> lista = SQLCRUDSelect.SQLSelectPar_Arry(_sql, nChave);
            return lista;
        }

        public bool Update(Usuario obj)
        {
            // campo excluir para insert geralmente Id (gerado automatico)
            string[] campoexcluir = { "Id" };

            // VETOR STRING CAMPOS objeto recebido
            List<string> campos = StringSQL.SharedCampos(obj, campoexcluir);

            // monta a string sql
            string _sql = StringSQL.SQLUpdateRetorno(campos, MapDataBase.GetUsuarioTable(), "Id");

            //alterar o regritro
            var ret = SQLCRUDUpdate.SQLUpdate<Usuario, int>(_sql, campos, obj, campoexcluir, "Id", obj.Id);

            return ret;
        }
    }
}
