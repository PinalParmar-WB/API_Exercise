using API_Exercise.Models;
using API_Exercise.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Exercise.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IPartyService _partyService;
        public PartyController(IPartyService party)
        {
            _partyService = party;
        }

        // GET: api/<PartyController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.Write(userId);
            var parties = await _partyService.GetAllAsync();
            return Ok(parties);
        }

        // GET api/<PartyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var party = await _partyService.GetByIdAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            return Ok(party);
        }

        // POST api/<PartyController>
        [HttpPost]
        public async Task<IActionResult> Create(Party model)
        {
            var created = await _partyService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new {id = created.PartyId}, created);
        }

        // PUT api/<PartyController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Party model)
        {
            if(id != model.PartyId)
            {
                return BadRequest("PartyId mismatch");
            }

            var updated = await _partyService.UpdateAsync(id, model);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE api/<PartyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _partyService.DeleteAsync(id);  
            if(!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
