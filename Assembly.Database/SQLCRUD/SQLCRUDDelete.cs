using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class SQLCRUDDelete : ISQLCRUD
    {
        public SQLCRUDDelete() { }

        public static bool SQLDelete<Tvr>(string pSQL, string ncampo,
                             Tvr nValor)
        {
            int nret;
            if (nValor is not null)
            {
                // efetuar uma pesquisa antes var ver usuario existe caso positivo continuas
                if (true)
                {
                    using (SqlConnection nDB = new SqlFactory().startDB())
                    using (SqlCommand cmd = new SqlCommand(pSQL, nDB))
                    {
                        // Parâmetro da consulta SQL
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@" + ncampo;
                        param.Value = nValor;
                        cmd.Parameters.Add(param);

                        nret = cmd.ExecuteNonQuery();
                    }
                    return (nret > 0 ? true : false) ;
                }else
                {   // usuario nao encontradp
                    return false;
                }

            }
            // paramentro deletar nao passado
            return false;

        }

    }
}
