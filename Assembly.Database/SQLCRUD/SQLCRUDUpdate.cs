using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class SQLCRUDUpdate : ISQLCRUD
    {

        public SQLCRUDUpdate() { }

        public static bool SQLUpdate<TEnty, Tvr>(string pSQL, List<string> ncampos,
                                     TEnty pOBJ, string[] pExcluir,
                                     string nChave, Tvr nValor)
        {
            // Montando variáveis
            var elementos = pOBJ.GetType().GetProperties();
            int ntam = ncampos.Count;
            int encontrou = -1;
            int nret = -1;

            if( nChave is not null)
            {
                using (SqlConnection nDB = new SqlFactory().startDB())
                using (SqlCommand cmd = new SqlCommand(pSQL, nDB))
                {
                    // Parâmetros da consulta SQL
                    SqlParameter param;
                    for (int n = 0; n < elementos.Length; n++)
                    {
                        encontrou = ncampos.IndexOf(elementos[n].Name);
                        if (encontrou > -1)
                        {
                            param = new SqlParameter();
                            param.ParameterName = "@" + ncampos[encontrou];
                            param.Value = elementos[n].GetValue(pOBJ, null);
                            cmd.Parameters.Add(param);
                        }
                    }
                    // Adiciona parâmetro do WHERE
                    param = new SqlParameter();
                    param.ParameterName = "@" + nChave;
                    param.Value = nValor;
                    cmd.Parameters.Add(param);

                    nret = cmd.ExecuteNonQuery();
                    return (nret > 0 ? true : false);
                }
            }
            return false;

        }

    }
}
