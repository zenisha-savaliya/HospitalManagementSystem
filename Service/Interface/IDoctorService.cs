using Service.DTO;
using System.Net.NetworkInformation;

namespace Service.Interface
{
    public interface IDoctorService
    {

        Task<string> AddDoctor(RegisterDTO registerDTO,string Specialization);
        Task<string> AddNurse(RegisterDTO registerDTO);
        Task<string> AddReceptionist(RegisterDTO registerDTO);
        Task<string> AssignDuty(AssignDutyDTO assignDutyDTO);
        Task<List<DoctorAppointmentViewDTO>> CheckAppointments(string consultDoctor);
        Task<string> ChangeStatus(int id, string status);

    }
}
