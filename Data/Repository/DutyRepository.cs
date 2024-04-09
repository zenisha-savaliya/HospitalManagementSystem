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
            return await _context.Duty
                .Where(d => d.NurseId == id)
                .ToListAsync();
        }
    }
}
