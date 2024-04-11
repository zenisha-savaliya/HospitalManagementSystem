using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    public class AuthController : BaseController
    {
        #region Properties
        private readonly IAuthService _authService;
        #endregion

        #region Constructor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Authendpoints
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
        #endregion
    }
}
