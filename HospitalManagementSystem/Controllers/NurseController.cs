using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    public class NurseController : BaseController
    {
        private readonly INurseService _nurseService;
        public NurseController(INurseService nurseService)
        {
            _nurseService = nurseService;
        }
        [HttpGet("/DashBoard/{id}")]
        public async Task<IActionResult> DashBoard([FromRoute] int id)
        {
            return Ok(await _nurseService.SeeDuties(id));
        }

    }
}
