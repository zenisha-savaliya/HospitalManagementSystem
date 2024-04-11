using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class NurseRepository : INurseRepository
    {
        private readonly AppDbContext _context;
        public NurseRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<bool> AddNurse(Nurse nurse)
        {
            try
            {
                await _context.Nurses.AddAsync(nurse);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a nurse: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CheckNurseExist(int id)
        {
            try
            {
                return await _context.Nurses.AnyAsync(n => n.NurseId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<int> GetNurseCount()
        {
            try
            {
                return await _context.Nurses.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1; 
            }
        }

        public async Task<bool> RemoveNurse(Nurse nurse)
        {
            try
            {
                _context.Nurses.Remove(nurse);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing nurse: {ex.Message}");
                return false;
            }
        }
    }
}
