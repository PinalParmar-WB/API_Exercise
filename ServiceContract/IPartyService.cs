using API_Exercise.Models;

namespace API_Exercise.ServiceContract
{
    public interface IPartyService
    {
        Task<IEnumerable<Party>> GetAllAsync();
        Task<Party> CreateAsync(Party entity);
        Task<Party?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Party entity);
        Task<bool> DeleteAsync(int id);
    }
}
