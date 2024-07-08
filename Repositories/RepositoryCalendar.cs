using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryCalendar : IRepositoryCalendar
    {
        private readonly ApplicationDBContext _context;

        public RepositoryCalendar(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Calendar>> GetCalendars()
        {
            //return await _context.Calendars.Include(c => c.Users).ToListAsync();
            return await _context.Calendars.ToListAsync();
        }

        public async Task<Calendar?> GetCalendar(Guid id)
        {
            return await _context.Calendars
                       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Calendar> AddCalendar(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
            return calendar;
        }

        public async Task<bool> ExistCalendar(Guid id)
        {
            return await _context.Calendars.AnyAsync(c => c.Id == id);
        }

        public async Task<Calendar> UpdateCalendar(Calendar calendar)
        {
            var existingCalendar = await _context.Calendars.FindAsync(calendar.Id);

            if (existingCalendar == null)
            {
                throw new ArgumentException("Calendar not found.");
            }

            _context.Entry(existingCalendar).CurrentValues.SetValues(calendar);
            await _context.SaveChangesAsync();

            return existingCalendar;
        }

        public async Task DeleteCalendar(Guid id)
        {
            var calendar = await GetCalendar(id);
            if (calendar != null)
            {
                _context.Calendars.Remove(calendar);
                await _context.SaveChangesAsync();
            }
        }
    }
}
