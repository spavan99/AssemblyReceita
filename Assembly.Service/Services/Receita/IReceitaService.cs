using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IReceitaService : IService<Receita, DtosReceitaFull>
    {
        bool UpdateFull(DtosReceitaFull obj);

        string AddFull(DtosReceitaFull obj);
        
    }
}
