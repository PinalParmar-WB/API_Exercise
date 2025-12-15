using API_Exercise.Data;
using API_Exercise.DTO;
using API_Exercise.Models;
using API_Exercise.ServiceContract;
using Microsoft.EntityFrameworkCore;

namespace API_Exercise.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;
        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<InvoiceDisplayDTO> CreateAsync(InvoiceInputDTO model)
        {
            var invoice = new Invoice
            {
                InvoiceDate = model.InvoiceDate,
                PartyId = model.PartyId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalAmount = model.TotalAmount,
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            var createdInvoice = await _context.Invoices
                .Include(i => i.Product)
                .Include(i => i.Party)
                .FirstAsync(i => i.InvoiceId == invoice.InvoiceId);

            var displayInvoice = new InvoiceDisplayDTO
            {
                InvoiceId = createdInvoice.InvoiceId,
                InvoiceDate = createdInvoice.InvoiceDate,
                ProductName = createdInvoice.Product.ProductName,
                ProductRate = Convert.ToDouble(createdInvoice.Product.ProductRate),
                PartyName = createdInvoice.Party.PartyName,
                Quantity = createdInvoice.Quantity,
                TotalAmount = createdInvoice.TotalAmount,
            };
            return displayInvoice;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if(invoice == null)
                return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<IEnumerable<InvoiceDisplayDTO>> GetAll()
        {
            return await _context.Invoices
                                    .Include(s => s.Product)
                                    .Include(s => s.Party)
                                    .Select(s => new InvoiceDisplayDTO
            {
                InvoiceId = s.InvoiceId,
                InvoiceDate = s.InvoiceDate,
                ProductName = s.Product.ProductName,
                PartyName = s.Party.PartyName,
                ProductRate = Convert.ToDouble(s.Product.ProductRate),
                Quantity = s.Quantity,
                TotalAmount = s.TotalAmount,
            }).ToListAsync();
        }

        public async Task<IEnumerable<InvoiceDisplayDTO>?> GetAllByProductAndParty(int? productId, int? partyId)
        {
            var invoices = await _context.Invoices
                                    .Include(s => s.Product)
                                    .Include(s => s.Party).ToListAsync();

            if (productId.HasValue)
                invoices = invoices.Where(i => i.ProductId == productId).ToList();

            if(partyId.HasValue)
                invoices = invoices.Where(i => i.PartyId == partyId).ToList();

            var filteredInvoices = invoices.Select(s => new InvoiceDisplayDTO
            {
                InvoiceId = s.InvoiceId,
                InvoiceDate = s.InvoiceDate,
                ProductName = s.Product.ProductName,
                PartyName = s.Party.PartyName,
                ProductRate = Convert.ToDouble(s.Product.ProductRate),
                Quantity = s.Quantity,
                TotalAmount = s.TotalAmount,
            });

            return filteredInvoices;
        }

        public async Task<InvoiceDisplayDTO?> GetById(int id)
        {
            var invoice = await _context.Invoices
                                            .Include(i => i.Product)
                                            .Include(i => i.Party)
                                            .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if(invoice == null)
                return null;

            var displayInvoice = new InvoiceDisplayDTO()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                ProductName = invoice.Product.ProductName,
                ProductRate = Convert.ToDouble(invoice.Product.ProductRate),
                PartyName = invoice.Party.PartyName,
                Quantity = invoice.Quantity,
                TotalAmount = invoice.TotalAmount,
            };
            return displayInvoice;
        }

        public async Task<bool> UpdateAsync(int id, InvoiceInputDTO model)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if(invoice == null) return false;

            invoice.InvoiceDate = model.InvoiceDate;
            invoice.PartyId = model.PartyId;
            invoice.ProductId = model.ProductId;
            invoice.TotalAmount = model.TotalAmount;
            invoice.Quantity = model.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
