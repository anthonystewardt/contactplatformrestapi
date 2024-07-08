using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryCondition : IRepositoryCondition
    {
        private readonly ApplicationDBContext _context;

        public RepositoryCondition(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Condition>> GetConditions()
        {
            return await _context.Conditions.ToListAsync();
        }


        public async Task<Entities.Condition?> GetCondition(int id)
        {
            return await _context.Conditions.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Entities.Condition> AddCondition(Entities.Condition condition)
        {
            _context.Conditions.Add(condition);
            await _context.SaveChangesAsync();
            return condition;
        }


        public async Task<Entities.Condition> UpdateCondition(Entities.Condition condition)
        {
            _context.Conditions.Update(condition);
            await _context.SaveChangesAsync();
            return condition;
        }


        public async Task<bool> ExistCondition(int id)
        {
            return await _context.Conditions.AnyAsync(x => x.Id == id);
        }


        public async Task DeleteCondition(int id)
        {
            var condition = await GetCondition(id);
            if (condition != null)
            {
                _context.Conditions.Remove(condition);
                await _context.SaveChangesAsync();
            }
        }

    }
}
