using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    [Authorize(Roles = "Nurse")]
    public class NurseController : BaseController
    {
        #region Properties
        private readonly INurseService _nurseService;
        #endregion

        #region Constructor
        public NurseController(INurseService nurseService)
        {
            _nurseService = nurseService;
        }
        #endregion

        #region Endpoints
        [HttpGet("/DashBoard/{id}")]
        public async Task<IActionResult> DashBoard([FromRoute] int id)
        {
            
            return Ok(await _nurseService.SeeDuties(id));
        }
        #endregion

    }
}
