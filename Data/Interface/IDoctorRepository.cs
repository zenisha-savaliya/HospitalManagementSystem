using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IDoctorRepository
    {
        Task<bool> AddDoctor(Doctor doctor);
        Task<int> GetDoctorCount();
        Task<bool> CheckSpecialization(string Specialization);
        Task<int> GetDoctorIdBySpecialization(string specialization);
        Task<bool> CheckDoctorExist(int id);
    }
}
