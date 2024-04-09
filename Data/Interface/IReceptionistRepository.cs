using Data.Models;

namespace Data.Interface
{
    public interface IReceptionistRepository
    {
        Task<bool> AddReceptionist(Receptionist receptionist);
        Task<int> GetReceptionistCount();
    }
}
