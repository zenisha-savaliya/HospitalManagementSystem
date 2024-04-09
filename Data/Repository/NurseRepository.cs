using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Nurses.AnyAsync(n => n.NurseId == id);
        }

        public async Task<int> GetNurseCount()
        {
            return await _context.Doctors.CountAsync();
        }
    }
}
