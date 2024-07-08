using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositoryTrainer: IRepositoryTrainer
    {
        private readonly ApplicationDBContext _context;

        public RepositoryTrainer(ApplicationDBContext context)
        {
            this._context = context;
        }

        public async Task<List<Entities.Trainer>> GetTrainers()
        {
            //return await _context.Trainers.ToListAsync();
            return await _context.Trainers.Include(t => t.User).ToListAsync();
        }

        public async Task<Entities.Trainer?> GetTrainer(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<Entities.Trainer> AddTrainer(Entities.Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Entities.Trainer> UpdateTrainer(Entities.Trainer trainer)
        {
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<bool> ExistTrainer(int id)
        {
            return await _context.Trainers.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
        }
    }
}
