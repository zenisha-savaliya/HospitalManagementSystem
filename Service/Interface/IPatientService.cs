using Service.DTO;

namespace Service.Interface
{
    public interface IPatientService
    {
        Task<AppoinmentViewDTO> GetAppoinmentDetail(int id);
        Task<List<AppoinmentViewDTO>> GetAppoinmentHistory(int id);
    }
}
