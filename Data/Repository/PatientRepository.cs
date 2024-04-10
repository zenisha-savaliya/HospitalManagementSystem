using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Data.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<bool> CheckPatientExist(string Email, string FirstName)
        {
            bool exists = await _context.Patients
                .AnyAsync(u => u.Email == Email && u.FirstName == FirstName);

            return exists;
        }

        public async Task<bool> CheckPatientExistById(int id)
        {
            return await _context.Patients.AnyAsync(p => p.PatientId == id);
        }

        public async Task<int> GetPatientId(string Email, string FirstName)
        {
            Patient patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == Email && p.FirstName == FirstName);

            if (patient != null)
            {
                return patient.PatientId;
            }
            return 0;
        }

        public async Task<bool> RegisterPatient(Patient patient)
        {
            try
            {
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a patient: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemovePatient(Patient patient)
        {
            try
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing patient: {ex.Message}");
                return false;
            }
        }
    }
}
