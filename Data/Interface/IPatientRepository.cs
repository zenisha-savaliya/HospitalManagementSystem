using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IPatientRepository
    {
        Task<bool> RegisterPatient(Patient patient);
        Task<bool> CheckPatientExist(string Email, string FirstName);
        Task<int> GetPatientId(string Email, string FirstName);
        Task<bool> CheckPatientExistById(int id);
    }
}
