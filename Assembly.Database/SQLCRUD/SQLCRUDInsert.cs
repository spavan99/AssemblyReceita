using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assembly.Database
{
    public class SQLCRUDInsert : ISQLCRUD
    {
 
        //contrutor
        public SQLCRUDInsert() { }

        public static object SQLInset<TEnty>(string pSQL, List<string> ncampos,
                                     TEnty pOBJ, string[] pExcluir )
        {
            // Montando variáveis
            var elementos = pOBJ.GetType().GetProperties();
            int ntam = ncampos.Count;
            int encontrou = -1;

            using ( SqlConnection nDB = new SqlFactory().startDB())
            using (SqlCommand cmd = new SqlCommand(pSQL + " SELECT SCOPE_IDENTITY()", nDB))
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
                return cmd.ExecuteScalar();

            }
        }





    }
}
