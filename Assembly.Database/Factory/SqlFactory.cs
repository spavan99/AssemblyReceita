﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class SqlFactory
    {
        /*
        private readonly string _strConexao = "Data Source=DESKTOP-AJ2A8P4;" +
                                              "Initial Catalog=assembly_receitas;" +
                                              "Integrated Security = True;Encrypt=False";
        */


        private readonly string _strConexao = "Server=5.161.72.81;Database=assembly_receitas;User Id=sa;Password=Minhasenha4321#;Encrypt=False;TrustServerCertificate=True";


        // contutor pegar string no json
        public SqlFactory() { }

        //retornar acesso ao banco de dados
        public IDbConnection GetSqlConnection()
        {
            return new SqlConnection(_strConexao);
        }

        public SqlConnection startDB()
        {

            SqlConnection Conexao_BD = new SqlConnection(_strConexao);
            Conexao_BD.Open();

            return Conexao_BD;
        }

    }
}
