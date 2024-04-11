using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly AppDbContext _context;
        public ReceptionistRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<bool> AddReceptionist(Receptionist receptionist)
        {
            try
            {
                await _context.Receptionists.AddAsync(receptionist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a receptionist: {ex.Message}");
                return false;
            }
        }

        public async Task<int> GetReceptionistCount()
        {
            try
            {
                return await _context.Receptionists.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching receptionist count: {ex.Message}");
                return -1; 
            }
        }
    }
}
