using Service.DTO;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<string> LoginByEmail(LoginWithEmail loginWithEmail);

        Task<string> LoginWithMobileNUmber(LoginWithMobileNumber loginWithMobileNumber);

    }
}
