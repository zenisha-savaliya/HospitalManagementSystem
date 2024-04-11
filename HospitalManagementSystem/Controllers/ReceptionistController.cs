using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalManagementSystem.Controllers
{
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistController : BaseController
    {
        #region Properties
        private readonly IReceptionistService _receptionistService;
        #endregion

        #region Constructor
        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        [Route("ScheduleAppointment")]
        public async Task<IActionResult> ScheduleAppointment(AppointmentDTO appointmentDTO)
        {
            return Ok(await _receptionistService.ScheduleAppoinment(appointmentDTO));
        }
        #endregion
    }
}
