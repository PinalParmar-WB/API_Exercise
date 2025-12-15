using API_Exercise.Models;

namespace API_Exercise.DTO
{
    public class InvoiceDisplayDTO
    {
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string PartyName { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public double ProductRate { get; set; }
        public decimal Quantity { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
