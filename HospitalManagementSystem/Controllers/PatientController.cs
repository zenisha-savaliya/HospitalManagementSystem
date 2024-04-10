using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("/GetCurrentAppoinmentSchedule/{id}")]
        public async Task<IActionResult> GetCurrentAppoinmentSchedule([FromRoute] int id)
        {
            return Ok(await _patientService.GetAppoinmentDetail(id));
        }
        [HttpGet("/GetAppoinmentHistory/{id}")]
        public async Task<IActionResult> GetAppoinmentHistory([FromRoute] int id)
        {
            return Ok(await _patientService.GetAppoinmentHistory(id));
        }
    }
}
