using Data.Interface;
using Data.Models;
using Data.Repository;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PatientService : IPatientService
    {
        private readonly IAppoinmentRepository _appoinmentRepository;
        public PatientService(IAppoinmentRepository appoinmentRepository)
        {
            _appoinmentRepository = appoinmentRepository;
        }
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
    }
}
