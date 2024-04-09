using Data.Models;

namespace Data.Interface
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(User user);
        Task<bool> CheckUserExist(string Email, string FirstName);
        Task<bool> RemoveUser(User user);
    }
}
