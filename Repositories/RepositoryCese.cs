using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryCese: IRepositoryCese
    {
        private readonly ApplicationDBContext _context;

        public RepositoryCese(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Cese>> GetCeses()
        {
            return await _context.Ceses.ToListAsync();
        }

        public async Task<Entities.Cese?> GetCese(int id)
        {
            return await _context.Ceses.Include(x => x.ReasonForDeparture).FirstOrDefaultAsync(x => x.Id == id);
        }

        // GetCesesByUser
        public async Task<List<Entities.Cese>> GetCesesByUser(int id)
        {
            return await _context.Ceses.Where(x => x.UserId == id).Include(x => x.ReasonForDeparture).ToListAsync();
        }

        public async Task<Entities.Cese> AddCese(Entities.Cese cese)
        {
            _context.Ceses.Add(cese);
            await _context.SaveChangesAsync();
            return cese;
        }

        public async Task<Entities.Cese> UpdateCese(Entities.Cese cese)
        {
            _context.Ceses.Update(cese);
            await _context.SaveChangesAsync();
            return cese;
        }

        public async Task<bool> ExistCese(int id)
        {
            return await _context.Ceses.AnyAsync(x => x.Id == id);
        }

        public async Task DeleteCese(int id)
        {
            var cese = await GetCese(id);
            if (cese == null)
            {
                throw new KeyNotFoundException("Cese not found.");
            }
            _context.Ceses.Remove(cese);
            await _context.SaveChangesAsync();
        }

        public async Task<Entities.User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        
    }
}
