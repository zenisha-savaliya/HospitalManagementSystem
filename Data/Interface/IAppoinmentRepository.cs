using Data.Models;

namespace Data.Interface
{
    public interface IAppoinmentRepository
    {
        Task<bool> AddAppointment(Appointment appointment);

        Task<bool> RemoveAppointment(Appointment appointment);
        Task<bool> CheckDoctorAvailability(string consultdoctor, DateTime starttime);
        Task<Appointment> GetAppointment(int id);
        Task<List<Appointment>> GetAppointmentList(int id);
        Task<List<Appointment>> CheckAppointments(string consultDoctor);
        Task<bool> ChangeStatus(int id, string status);
    }
}
