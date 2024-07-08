using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryModality: IRepositoryModality
    {
        private readonly ApplicationDBContext _context;
        public RepositoryModality(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Entities.Modality>> GetModalities()
        {
            return await _context.Modalities.ToListAsync();
        }
        public async Task<Entities.Modality?> GetModality(int id)
        {
            return await _context.Modalities.FindAsync(id);
        }
        public async Task<Entities.Modality> AddModality(Entities.Modality modality)
        {
            _context.Modalities.Add(modality);
            await _context.SaveChangesAsync();
            return modality;
        }
        public async Task<Entities.Modality> UpdateModality(Entities.Modality modality)
        {
            _context.Modalities.Update(modality);
            await _context.SaveChangesAsync();
            return modality;
        }
        public async Task<bool> ExistModality(int id)
        {
            return await _context.Modalities.AnyAsync(x => x.Id == id);
        }
        public async Task DeleteModality(int id)
        {
            var modality = await _context.Modalities.FindAsync(id);
            _context.Modalities.Remove(modality);
            await _context.SaveChangesAsync();
        }
    }
}
