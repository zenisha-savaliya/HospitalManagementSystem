using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<int> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await GetUserId(user);
        }

        public async Task<int> GetUserId(User user)
        {
            return user.UserId;
        }

        public async Task<bool> CheckUserExist(string Email, string FirstName)
        {
            bool exists = await _context.Users
                .AnyAsync(u => u.Email == Email || u.FirstName == FirstName);

            return exists;
        }
    }
}
