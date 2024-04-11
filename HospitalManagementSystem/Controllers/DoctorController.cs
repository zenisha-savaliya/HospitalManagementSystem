using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : BaseController
    {
        #region Properties
        private readonly IDoctorService _doctorService;
        #endregion

        #region Constructor
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        [Route("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody]RegisterDTO registerDTO,[FromQuery]string Specialization)
        {
            return Ok(await _doctorService.AddDoctor(registerDTO,Specialization));
        }
        [HttpPost]
        [Route("AddNurse")]
        public async Task<IActionResult> AddNurse([FromBody] RegisterDTO registerDTO)
        {
            return Ok(await _doctorService.AddNurse(registerDTO));
        }
        [HttpPost]
        [Route("AddReceptionist")]
        public async Task<IActionResult> AddReceptionist([FromBody] RegisterDTO registerDTO)
        {
            return Ok(await _doctorService.AddReceptionist(registerDTO));
        }
        [HttpPost]
        [Route("AssignDutyToNurse")]
        public async Task<IActionResult> AssignDutyToNurse([FromBody] AssignDutyDTO assignDutyDTO)
        {
            return Ok(await _doctorService.AssignDuty(assignDutyDTO));
        }
        [HttpGet("/CheckAllAppointments/{id}")]
        public async Task<IActionResult> CheckAllAppointments(string consultDoctor)
        {
            return Ok(await _doctorService.CheckAppointments(consultDoctor));
        }
        [HttpPatch("ChangeStatusToReschedule")]
        public async Task<IActionResult> ChangeStatusToReschedule(int AppoinmentId,string status)
        {
            return Ok(await _doctorService.ChangeStatus(AppoinmentId, status));
        }
        [HttpPatch("ChangeStatusToCancel")]
        public async Task<IActionResult> ChangeStatusToCancel(int AppoinmentId, string status)
        {
            return Ok(await _doctorService.ChangeStatus(AppoinmentId, status));
        }
        #endregion
    }
}
