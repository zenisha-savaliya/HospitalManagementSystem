using Data.Models;

namespace Data.Interface
{
    public interface INurseRepository
    {
        Task<int> GetNurseCount();
        Task<bool> AddNurse(Nurse nurse);

        Task<bool> RemoveNurse(Nurse nurse);
        Task<bool> CheckNurseExist(int id);
    }
}
