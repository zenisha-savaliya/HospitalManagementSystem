using Service.DTO;

namespace Service.Interface
{
    public interface IReceptionistService
    {
        Task<string> ScheduleAppoinment(AppointmentDTO appointmentDTO);
    }
}
