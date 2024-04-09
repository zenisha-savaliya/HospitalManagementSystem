using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface INurseRepository
    {
        Task<int> GetNurseCount();
        Task<bool> AddNurse(Nurse nurse);
        Task<bool> CheckNurseExist(int id);
    }
}
