using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Database
{
    public class ModelQuery
    {

        public ModelQuery(string pquery, object pparam)
        {
            SqlQuery = pquery;
            SqlParameters = pparam;
        }

        public string SqlQuery { get; set; }
        public object SqlParameters { get; set; }

    }
}
