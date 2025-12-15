using API_Exercise.Models;

namespace API_Exercise.DTO
{
    public class InvoiceInputDTO
    {
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PartyId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
