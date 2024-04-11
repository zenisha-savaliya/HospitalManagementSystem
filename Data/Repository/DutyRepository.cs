using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class DutyRepository : IDutyRepository
    {
        private readonly AppDbContext _context;
        public DutyRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<bool> AddDuty(Duty duty)
        {
            try
            {
                await _context.Duty.AddAsync(duty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a duty: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Duty>> GetDutyList(int id)
        {
            try
            {
                return await _context.Duty
                    .Where(d => d.NurseId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Duty>(); 
            }
        }
    }
}
