using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositoryReasonForDeparture: IRepositoryReasonForDepartures
    {

        private readonly ApplicationDBContext _context;

        public RepositoryReasonForDeparture(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.ReasonForDeparture>> GetReasonForDepartures()
        {
            return await _context.ReasonForDepartures.ToListAsync();
        }

        public async Task<Entities.ReasonForDeparture?> GetReasonForDeparture(int id)
        {
            return await _context.ReasonForDepartures.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Entities.ReasonForDeparture> AddReasonForDeparture(Entities.ReasonForDeparture reasonForDeparture)
        {
            _context.ReasonForDepartures.Add(reasonForDeparture);
            await _context.SaveChangesAsync();
            return reasonForDeparture;
        }

        public async Task<bool> ExistReasonForDeparture(int id)
        {
            return await _context.ReasonForDepartures.AnyAsync(x => x.Id == id);
        }

        public async Task<Entities.ReasonForDeparture> UpdateReasonForDeparture(Entities.ReasonForDeparture reasonForDeparture)
        {
            _context.ReasonForDepartures.Update(reasonForDeparture);
            await _context.SaveChangesAsync();
            return reasonForDeparture;
        }

        public async Task DeleteReasonForDeparture(int id)
        {
            var reasonForDeparture = await _context.ReasonForDepartures.FindAsync(id);
            if (reasonForDeparture != null)
            {
                _context.ReasonForDepartures.Remove(reasonForDeparture);
                await _context.SaveChangesAsync();
            }
        }



        
    }
}
