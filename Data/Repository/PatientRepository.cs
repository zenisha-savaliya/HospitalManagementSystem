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
            try
            {
                bool exists = await _context.Patients
                    .AnyAsync(u => u.Email == Email && u.FirstName == FirstName);

                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking patient existence: {ex.Message}");
                return false; 
            }
        }

        public async Task<bool> CheckPatientExistById(int id)
        {
            try
            {
                bool exists = await _context.Patients.AnyAsync(p => p.PatientId == id);
                return exists;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking patient existence by ID: {ex.Message}");
                return false; 
            }
        }

        public async Task<int> GetPatientId(string Email, string FirstName)
        {
            try
            {
                Patient patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Email == Email && p.FirstName == FirstName);

                return patient?.PatientId ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching patient ID: {ex.Message}");
                return -1; 
            }
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
