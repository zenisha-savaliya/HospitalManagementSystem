using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("/LoginByEmail")]
        public async Task<IActionResult> LoginByEmail([FromBody] LoginWithEmail loginWithEmail)
        {
            return Ok(await _authService.LoginByEmail(loginWithEmail));
        }

        [HttpPost]
        [Route("/LoginByMobileNumber")]
        public async Task<IActionResult> LoginByMobileNumber([FromBody] LoginWithMobileNumber loginWithMobileNumber)
        {
            return Ok(await _authService.LoginWithMobileNUmber(loginWithMobileNumber));
        }
    }
}
