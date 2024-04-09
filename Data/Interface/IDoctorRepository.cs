using Data.Models;

namespace Data.Interface
{
    public interface IDoctorRepository
    {
        Task<bool> AddDoctor(Doctor doctor);
        Task<int> GetDoctorCount();
        Task<bool> CheckSpecialization(string Specialization);
        Task<int> GetDoctorIdBySpecialization(string specialization);
        Task<bool> CheckDoctorExist(int id);

        Task<bool> RemoveDoctor(Doctor doctor);
    }
}
