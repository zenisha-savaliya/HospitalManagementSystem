using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return await _context.Doctors.AnyAsync(d => d.DoctorId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> CheckSpecialization(string Specialization)
        {
            try
            {
                bool exists = await _context.Doctors.AnyAsync(d => d.Specialist == Specialization);
                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<int> GetDoctorCount()
        {
            try
            {
                return await _context.Doctors.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1; 
            }
        }

        public async Task<int> GetDoctorIdBySpecialization(string specialization)
        {
            try
            {
                Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Specialist == specialization);
                return doctor?.DoctorId ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public async Task<bool> RemoveDoctor(Doctor doctor)
        {
            try
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing doctor: {ex.Message}");
                return false;
            }
        }
    }
}
