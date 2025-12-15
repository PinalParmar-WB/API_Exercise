using API_Exercise.Data;
using API_Exercise.Models;
using API_Exercise.ServiceContract;
using Microsoft.EntityFrameworkCore;

namespace API_Exercise.Services
{
    public class PartyService : IPartyService
    {
        private readonly AppDbContext _context;
        public PartyService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Party> CreateAsync(Party entity)
        {
            _context.Parties.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
            {
                return false;
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Party>> GetAllAsync()
        {
            return await _context.Parties.ToListAsync();
        }

        public async Task<Party?> GetByIdAsync(int id)
        {
            return await _context.Parties.FirstOrDefaultAsync(p => p.PartyId == id);
        }

        public async Task<bool> UpdateAsync(int id, Party entity)
        {
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
            {
                return false;
            }

            party.PartyName = entity.PartyName;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
