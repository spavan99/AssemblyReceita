using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class SQLCRUDSelect : ISQLCRUD
    {
        public SQLCRUDSelect() { }

        public static List<dynamic> SQLSelectPar_1<Tvr>(string pSQL, string ncampo, Tvr nValor )
        {
            using (SqlConnection nDB = new SqlFactory().startDB())
            using (SqlCommand cmd = new SqlCommand(pSQL, nDB))
            {
                // Parâmetro da consulta SQL
                if (ncampo is not null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@" + ncampo;
                    param.Value = nValor;
                    cmd.Parameters.Add(param);
                }

                //tirar depois 
                Console.WriteLine(pSQL);

                // Executa a consulta SQL
                using (SqlDataReader Resultado = cmd.ExecuteReader())
                {
                    List<dynamic> retPesquisa = LerDadosSQLReader(Resultado);
                    return retPesquisa;
                }
            }

        }
      

        // receve um array
        public static List<dynamic> SQLSelectPar_Arry(string pSQL, List<SQLDTOSPesquisa> ncampo)
        {
            using (SqlConnection nDB = new SqlFactory().startDB())
            using (SqlCommand cmd = new SqlCommand(pSQL, nDB))
            {
                // Parâmetro da consulta SQL
                if (  ncampo.Count > 0)
                {
                    for( int i = 0; i < ncampo.Count; i++)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@" + ncampo[i].nCampo;

                        if (ncampo[i].nType == SQLtypeEnum.type_int)
                        { param.Value = int.Parse(ncampo[i].nValor); }

                        if (ncampo[i].nType == SQLtypeEnum.type_string)
                        { param.Value = ncampo[i].nValor; }

                        if (ncampo[i].nType == SQLtypeEnum.type_bool)
                        { param.Value = bool.Parse(ncampo[i].nValor.ToLower()); }
                        cmd.Parameters.Add(param);
                    }
                }

                //tirar depois 
                Console.WriteLine(pSQL);

                // Executa a consulta SQL
                using (SqlDataReader Resultado = cmd.ExecuteReader())
                {
                    List<dynamic> retPesquisa = LerDadosSQLReader(Resultado);
                    return retPesquisa;
                }
            }

        }


        // funciona usando dynamic
        private static List<dynamic> LerDadosSQLReader(SqlDataReader reader)
        {
            List<dynamic> lista = new List<dynamic>();
            while (reader.Read())
            {
                var data = new ExpandoObject() as IDictionary<string, object>;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    data[reader.GetName(i)] = reader[i] == DBNull.Value ? null : reader[i];
                }
                lista.Add(data);
            }
            return lista;
        }

    }
}


