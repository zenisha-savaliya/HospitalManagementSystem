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
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return await GetUserId(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while registering the user: {ex.Message}");
                return -1; 
            }
        }

        public Task<int> GetUserId(User user)
        {
            return Task.FromResult(user.UserId);
        }

        public async Task<bool> CheckUserExist(string Email, string FirstName)
        {
            try
            {
                bool exists = await _context.Users
                    .AnyAsync(u => u.Email == Email || u.FirstName == FirstName);

                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking user existence: {ex.Message}");
                return false; 
            }
        }

        public async Task<bool> RemoveUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing user: {ex.Message}");
                return false;
            }
        }
    }
}
