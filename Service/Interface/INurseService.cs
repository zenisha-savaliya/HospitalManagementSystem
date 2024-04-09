using Service.DTO;

namespace Service.Interface
{
    public interface INurseService
    {
        Task<List<SeeDutyDTO>> SeeDuties(int id);
    }
}
