using Data.Models;

namespace Data.Interface
{
    public interface IAuthRepository
    {
        Task<bool> AddUserAsync(User user);

        Task<User> CheckUserAuthByEmailAsync(string email, string password);

        Task<User> CheckUserAuthByMobileNumberAsync(string mobilenumber, string password);
    }
}
