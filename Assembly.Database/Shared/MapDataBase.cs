using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class MapDataBase
    {
        //especificar tabela banco de dados pode alterar no banco mas
        //nao altera nas query
        // colocar todas tabelas do banco de dados

        public static string GetUsuarioTable()
        {   
            return "[usuarios]";
        }

        public static string GetCategoriaTable()
        {
            return "[categorias]";
        }


        public static string GetReceitaTable()
        {
            return "[receitas]";
        }

        public static string GetItensReceitaTable()
        {
            return "[itensreceita]";
        }

        public static string GetComentariosReceitaTable()
        {
            return "[comentariosreceita]";
        }

        public static string GetDificuldadeTable()
        {
            return "[dificuldade]";
        }


    }
}
