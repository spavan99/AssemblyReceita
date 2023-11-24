using Assembly.Domain;
using Assembly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IItensReceitaService : IService<ItensReceita, DtosItensReceitaFull>
    {

        bool UpdateFull(DtosItensReceitaFull obj);
        string AddFull(DtosItensReceitaFull obj);
    }
}
