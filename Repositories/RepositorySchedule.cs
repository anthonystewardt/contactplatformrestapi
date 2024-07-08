using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositorySchedule: IRepositorySchedule
    {
        private readonly ApplicationDBContext _context;

        public RepositorySchedule(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Schedule>> GetSchedules()
        {
            //return await context.Users.OrderBy(x => x.Name).ToListAsync();

            return await _context.Schedules.Include(s => s.User).ToListAsync();
        }

        public async Task<Entities.Schedule?> GetSchedule(Guid id)
        {
            //return await _context.Schedules.FindAsync(id);
            return await _context.Schedules
                       .Include(s => s.User) // Incluir la entidad de usuario asociada
                       .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Entities.Schedule> AddSchedule(Entities.Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> ExistSchedule(Guid id)
        {
            return await _context.Schedules.AnyAsync(x => x.Id == id);
        }

        public async Task<Entities.Schedule> UpdateSchedule(Entities.Schedule schedule)
        {
            var existingSchedule = await _context.Schedules.FindAsync(schedule.Id);

            if (existingSchedule == null)
            {
                throw new ArgumentException("Schedule not found.");
            }

            // Actualizar solo los campos proporcionados en el DTO
            _context.Entry(existingSchedule).CurrentValues.SetValues(schedule);

            // Excluir la actualización del UserId y otros campos que no deben actualizarse
            _context.Entry(existingSchedule).Property(x => x.UserId).IsModified = false;
            _context.Entry(existingSchedule).Property(x => x.User).IsModified = false;

            await _context.SaveChangesAsync();

            return existingSchedule;
        }

        public async Task DeleteSchedule(Guid id)
        {
            var schedule = await GetSchedule(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
