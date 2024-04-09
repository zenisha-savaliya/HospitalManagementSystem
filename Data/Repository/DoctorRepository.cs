using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<bool> AddDoctor(Doctor doctor)
        {
            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a doctor: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CheckDoctorExist(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.DoctorId == id);
        }

        public async Task<bool> CheckSpecialization(string Specialization)
        {
            bool exists = await _context.Doctors.AnyAsync(d => d.Specialist == Specialization);
            return exists;
        }

        public async Task<int> GetDoctorCount()
        {
           return await _context.Doctors.CountAsync();
        }

        public async Task<int> GetDoctorIdBySpecialization(string specialization)
        {
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Specialist == specialization);
            return doctor?.DoctorId ?? 0;
        }
    }
}
