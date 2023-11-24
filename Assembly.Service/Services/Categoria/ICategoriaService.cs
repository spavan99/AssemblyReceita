using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface ICategoriaService : IService<Categoria, DtosCategoriaFull>
    {
        bool UpdateFull(DtosCategoriaFull obj);

        string AddFull(DtosCategoriaFull obj);

        
    }
}
