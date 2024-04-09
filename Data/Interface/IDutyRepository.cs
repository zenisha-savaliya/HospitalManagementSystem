using Data.Models;

namespace Data.Interface
{
    public interface IDutyRepository
    {
        Task<bool> AddDuty(Duty duty);
        Task<List<Duty>> GetDutyList(int id);
    }
}
