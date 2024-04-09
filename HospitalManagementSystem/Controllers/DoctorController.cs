using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        [Route("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody]RegisterDTO registerDTO,[FromQuery]string Specialization)
        {
            return Ok(await _doctorService.AddDoctor(registerDTO,Specialization));
        }
        [HttpPost]
        [Route("AddNurse")]
        public async Task<IActionResult> AddNurse(RegisterDTO registerDTO)
        {
            return Ok(await _doctorService.AddNurse(registerDTO));
        }
        [HttpPost]
        [Route("AddReceptionist")]
        public async Task<IActionResult> AddReceptionist(RegisterDTO registerDTO)
        {
            return Ok(await _doctorService.AddReceptionist(registerDTO));
        }
        [HttpPost]
        [Route("AssignDutyToNurse")]
        public async Task<IActionResult> AssignDutyToNurse(AssignDutyDTO assignDutyDTO)
        {
            return Ok(await _doctorService.AssignDuty(assignDutyDTO));
        }
        [HttpGet("/CheckAllAppointments/{id}")]
        public async Task<IActionResult> CheckAllAppointments(string consultDoctor)
        {
            return Ok(await _doctorService.CheckAppointments(consultDoctor));
        }
        [HttpPatch("ChangeStatusToReschedule")]
        public async Task<IActionResult> ChangeStatusToReschedule(int id,string status)
        {
            return Ok(await _doctorService.ChangeStatus(id,status));
        }
        [HttpPatch("ChangeStatusToCancel")]
        public async Task<IActionResult> ChangeStatusToCancel(int id, string status)
        {
            return Ok(await _doctorService.ChangeStatus(id, status));
        }
    }
}
