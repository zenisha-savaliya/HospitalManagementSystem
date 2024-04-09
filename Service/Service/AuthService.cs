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
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository authRepository,IConfiguration configuration)
        {
            _config = configuration;
            _authRepository = authRepository;
        }
        public async Task<string> LoginByEmail(LoginWithEmail loginWithEmail)
        {
            User user = await _authRepository.CheckUserAuthByEmailAsync(loginWithEmail.Email, loginWithEmail.Password);
            if (user != null)
            {
                return GenerateToken(user);
            }
            else
            {
                return "Please enter valid credentials";
            }
        }

        public async Task<string> LoginWithMobileNUmber(LoginWithMobileNumber loginWithMobileNumber)
        {
            User user = await _authRepository.CheckUserAuthByMobileNumberAsync(loginWithMobileNumber.ContactNumber, loginWithMobileNumber.Password);
            if (user != null)
            {
                return GenerateToken(user);
            }
            else
            {
                return "Please enter valid credentials";
            }
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.Role),
            };


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
