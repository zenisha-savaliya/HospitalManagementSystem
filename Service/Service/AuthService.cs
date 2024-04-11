using Data.Interface;
using Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.DTO;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Service
{
    public class AuthService : IAuthService
    {
        #region Fields
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly IHashPasswordService _hashPasswordService;
        #endregion

        #region Constructor
        public AuthService(IAuthRepository authRepository,IConfiguration configuration,IHashPasswordService hashPasswordService)
        {
            _config = configuration;
            _authRepository = authRepository;
            _hashPasswordService = hashPasswordService;
        }
        #endregion

        #region Methods
        public async Task<string> LoginByEmail(LoginWithEmail loginWithEmail)
        {
            try
            {
                string HashPassword = _hashPasswordService.HashPassword(loginWithEmail.Password);
                User user = await _authRepository.CheckUserAuthByEmailAsync(loginWithEmail.Email, HashPassword);
                if (user != null)
                {
                    return GenerateToken(user);
                }
                else
                {
                    return "Please enter valid credentials";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while processing your request. Please try again later.";
            }
        }

        public async Task<string> LoginWithMobileNUmber(LoginWithMobileNumber loginWithMobileNumber)
        {
            try
            {
                string HashPassword = _hashPasswordService.HashPassword(loginWithMobileNumber.Password);
                User user = await _authRepository.CheckUserAuthByMobileNumberAsync(loginWithMobileNumber.ContactNumber, HashPassword);
                if (user != null)
                {
                    return GenerateToken(user);
                }
                else
                {
                    return "Please enter valid credentials";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "An error occurred while processing your request. Please try again later.";
            }
        }
        #endregion

        /// <summary>
        /// This method is used for generating JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        #region TokenGenerationMethod
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("Id", user.UserId.ToString()),
            };


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
