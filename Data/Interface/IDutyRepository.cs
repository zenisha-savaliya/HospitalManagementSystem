using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IDutyRepository
    {
        Task<bool> AddDuty(Duty duty);
        Task<List<Duty>> GetDutyList(int id);
    }
}
