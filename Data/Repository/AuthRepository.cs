using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                _context.Users.AddAsync(user);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<User> CheckUserAuthByEmailAsync(string email, string password)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<User> CheckUserAuthByMobileNumberAsync(string mobilenumber, string password)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.ContactNumber.Equals(mobilenumber) && u.Password.Equals(password));
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
