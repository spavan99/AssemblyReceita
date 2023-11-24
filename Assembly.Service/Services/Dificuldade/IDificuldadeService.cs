using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public interface IDificuldadeService : IService<Dificuldade, DtosDificuldadeFull>
    {
        bool UpdateFull(DtosDificuldadeFull obj);
        string AddFull(DtosDificuldadeFull obj);
    }
}
