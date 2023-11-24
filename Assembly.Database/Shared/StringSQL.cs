using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class StringSQL
    {

        //contrutor
        public StringSQL() { }


        public static List<DtosCadastroForm> SharedDadosCadastro<T>(T tOBJ, string[] pExluir)
        {
            List<DtosCadastroForm> nret = new List<DtosCadastroForm>();

            var elementos = tOBJ.GetType().GetProperties();

            for (int i = 0; i < elementos.Length; i++)
            {
                if (retElementoNaoConsiderar(elementos[i].Name, pExluir))
                {
                    DtosCadastroForm novo = new DtosCadastroForm();
                    novo.nomeLabel = elementos[i].Name;   // nome campo aparecer tela
                    novo.campoCadastro = elementos[i].Name;  // nome do campo arquivo dtos
                    novo.requeridoSN = notNullEnum.p_Nao;
                    novo.txtType = elementos[i].PropertyType;
                    string naux = "";
                    if(elementos[i].PropertyType == typeof(string))
                    {  naux = "text";
                    }else if(elementos[i].PropertyType == typeof(int))
                    {  naux = "number";
                    }
                    novo.tipoCampo = naux;
                    nret.Add(novo);

                    //nret.Add(elementos[i].Name);
                }
            }
            return nret;
        }


        //monta campos para insert a partir classe banco e tira de uma lista
        public static List<string> SharedCampos<T>(T tOBJ, string[] pExluir)
        {
            List<string> nret = new List<string>();

            var elementos = tOBJ.GetType().GetProperties();

            for (int i = 0; i < elementos.Length; i++)
            {
                if (retElementoNaoConsiderar(elementos[i].Name, pExluir))
                {
                    nret.Add(elementos[i].Name);
                }
            }
            return nret;
        }
        private static bool retElementoNaoConsiderar(string pElementoAtual, string[] pExcluir)
        {
            bool nret = true;

            for (int n = 0; n < pExcluir.Length; n++)
            {
                if (pElementoAtual.ToUpper().Equals(pExcluir[n].ToUpper()))
                {
                    nret = false;
                    break;
                }
            }

            return nret;
        }

        public static string SQLInsetRetorno(List<string> ncampos, string Table)
        {
            string _sql = "INSERT INTO " + Table + "(";
            int nTam = ncampos.Count;

            //monta primeira parte
            for (int i = 0; i < nTam; i++)
            {
                _sql = _sql + ncampos[i] + (i == nTam - 1 ? " )" : ", ");

            }
            //monta segunda parte
            _sql = _sql + " VALUES (";
            for (int i = 0; i < nTam; i++)
            {
                _sql = _sql + "@" + ncampos[i] + (i == nTam - 1 ? " )" : ", ");

            }
            return _sql;
        }

        public static string SQLUpdateRetorno(List<string> ncampos, string Table, string nChave)
        {
            //string _sql = "UPDATE cliente SET nomeCliente=@nomeCliente, enderecoCliente=@enderecoCliente, ";
            //_sql = _sql + "  WHERE idCliente=  @Id";

            int nTam = ncampos.Count;
            string _sql = "UPDATE  " + Table + " SET ";

            //parte campos a alterar
            for (int i = 0; i < nTam; i++)
            {
                _sql = _sql + ncampos[i] + "=@" + ncampos[i] + (i == nTam - 1 ? "  " : ", ");

            }
            // monta final where
            _sql = _sql + " WHERE " + nChave + "=@" + nChave;
            return _sql;
        }

        public static string SQLSelectBasicRetorno(List<string> vetCampos, List<string> vetAlias, string nTabela, string nChave = null)
        {

            // select sem whre toda tabela
            string _sql = SQLSelectParteUm(vetCampos, vetAlias, nTabela);

            //incluir where campo nao for nulo
            if (nChave is not null)
            {
                _sql = _sql + " WHERE " + nChave + "=@" + nChave;
            }
            return _sql;

        }

        // segund assinatura
        // monta sql select com varia instruçoes do campo ultimo  list varias campos para selecao
        public static string SQLSelectBasicRetorno(List<string> vetCampos, List<string> vetAlias, string nTabela, List<SQLDTOSPesquisa> nChave = null)
        {

            // select sem whre toda tabela
            string _sql = SQLSelectParteUm(vetCampos, vetAlias, nTabela);

            //whrere  list filtros
            if( nChave is not null)
            {
                if( nChave.Count > 0 )
                {
                    _sql = _sql + " WHERE ";

                    for ( int n =0; n < nChave.Count; n++)
                    {
                        _sql = _sql + nChave[n].nCampo + "=@" + nChave[n].nCampo;

                        if(nChave[n].operacao == SQLoperEnum.p_and)
                        {  _sql = _sql + " AND ";
                        }else if (nChave[n].operacao == SQLoperEnum.p_or)
                        { _sql = _sql + " AND ";
                        }else
                        { break; }
                    }
                }

            }

            return _sql;

        }

        // monta selete toda tabela parte um
        public static string SQLSelectParteUm(List<string> vetCampos, List<string> vetAlias, string nTabela)
        {
            int nTam = vetCampos.Count;
            string _sql = "SELECT  ";

            // incluir campos com as alias
            for (int i = 0; i < vetCampos.Count; i++)
            {
                _sql = _sql + vetCampos[i] + " AS " + vetAlias[i] + (i == nTam - 1 ? "  " : ", ");
            }
            // incluir a trabe
            _sql = _sql + " FROM " + nTabela;

            return _sql;
        }


        public static string SQLDeleteRetorno(string nCampo, string Table)
        {
            string _sql = "DELETE FROM " + Table + " WHERE " + nCampo + " = @" + nCampo;
            return _sql;
        }

    }
}
