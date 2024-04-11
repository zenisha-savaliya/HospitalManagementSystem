using Data.Interface;
using Data.Models;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class PatientService : IPatientService
    {
        #region Fields
        private readonly IAppoinmentRepository _appoinmentRepository;
        #endregion

        #region Constructor
        public PatientService(IAppoinmentRepository appoinmentRepository)
        {
            _appoinmentRepository = appoinmentRepository;
        }
        #endregion

        #region Methods
        public async Task<AppoinmentViewDTO> GetAppoinmentDetail(int id)
        {
            Appointment appointment = await _appoinmentRepository.GetAppointment(id);
            return new AppoinmentViewDTO
            {
                ScheduleStartTime = appointment.ScheduleStartTime,
                Status = appointment.Status,
                ConsultDoctor = appointment.ConsultDoctor,
            };
        }

        public async Task<List<AppoinmentViewDTO>> GetAppoinmentHistory(int id)
        {
                List<Appointment> appointmentList = await _appoinmentRepository.GetAppointmentList(id);

                List<AppoinmentViewDTO>  appoinmentViewsList = appointmentList.Select(appoinment => new AppoinmentViewDTO
                {
                    ScheduleStartTime = appoinment.ScheduleStartTime,
                    Status = appoinment.Status,
                    ConsultDoctor= appoinment.ConsultDoctor,
                }).ToList();
                return appoinmentViewsList;
        }
        #endregion
    }
}
