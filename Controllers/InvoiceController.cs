using API_Exercise.DTO;
using API_Exercise.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Exercise.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var invoices = await _invoiceService.GetAll();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if(invoice == null)
                return NotFound();

            return Ok(invoice); 
        }

        [HttpGet()]
        public async Task<IActionResult> GetByFilter(int? productId, int? partyId) { 
            var invoices = await _invoiceService.GetAllByProductAndParty(productId, partyId);

            if(invoices == null) return NotFound();

            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InvoiceInputDTO model)
        {
            var created = await _invoiceService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.InvoiceId}, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InvoiceInputDTO model)
        {
            if (id != model.InvoiceId)
                return BadRequest("InvoiceId mismatch");

            var updated = await _invoiceService.UpdateAsync(id, model);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _invoiceService.DeleteAsync(id);
            if(!deleted) return NotFound();

            return NoContent();
        }
    }
}
