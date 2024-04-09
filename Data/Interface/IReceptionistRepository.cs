using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IReceptionistRepository
    {
        Task<bool> AddReceptionist(Receptionist receptionist);
        Task<int> GetReceptionistCount();
    }
}
