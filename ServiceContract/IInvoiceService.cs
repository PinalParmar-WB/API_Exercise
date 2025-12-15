using API_Exercise.DTO;

namespace API_Exercise.ServiceContract
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDisplayDTO>> GetAll();
        Task<InvoiceDisplayDTO?> GetById(int id);
        Task<InvoiceDisplayDTO> CreateAsync(InvoiceInputDTO model);
        Task<bool> UpdateAsync(int id, InvoiceInputDTO model);
        Task<IEnumerable<InvoiceDisplayDTO>?> GetAllByProductAndParty(int? productId, int? partyId);
        Task<bool> DeleteAsync(int id);
    }
}
