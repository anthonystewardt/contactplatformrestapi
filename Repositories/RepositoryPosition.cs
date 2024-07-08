using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryPosition: IRepositoryPosition
    {
        private readonly ApplicationDBContext _context;

        public RepositoryPosition(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Position>> GetPositions()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Entities.Position?> GetPosition(int id)
        {
            return await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Entities.Position> AddPosition(Entities.Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }


        public async Task<Entities.Position> UpdatePosition(Entities.Position position)
        {
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
            return position;
        }


        public async Task<bool> ExistPosition(int id)
        {
            return await _context.Positions.AnyAsync(x => x.Id == id);
        }


        public async Task DeletePosition(int id)
        {
            //var position = await GetPosition(id);
            //if (position != null)
            //{
            //    _context.Positions.Remove(position);
            //    await _context.SaveChangesAsync();
            //}
            var position = await GetPosition(id);
            if (position == null)
            {
                throw new KeyNotFoundException("Position not found.");
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }

    }
}
