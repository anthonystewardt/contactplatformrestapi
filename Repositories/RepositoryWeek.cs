using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositoryWeek: IRepositoryWeek
    {
        private readonly ApplicationDBContext context;

        public RepositoryWeek(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Entities.Week>> GetWeeks()
        {
            return await context.Weeks.OrderBy(x => x.DateStart).ToListAsync();
        }

        public async Task<Entities.Week?> GetWeek(int id)
        {
            return await context.Weeks.FindAsync(id);
        }

        public async Task<Entities.Week> AddWeek(Entities.Week week)
        {
            context.Weeks.Add(week);
            await context.SaveChangesAsync();
            return week;
        }

        public async Task<Entities.Week> UpdateWeek(Entities.Week week)
        {
            context.Entry(week).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return week;
        }

        public async Task<List<Entities.Week>> GetWeeksByUserId(int userId)
        {
            return await context.Weeks.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> ExistWeek(int id)
        {
            return await context.Weeks.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteWeek(int id)
        {
            var week = await context.Weeks.FindAsync(id);
            if (week != null)
            {
                context.Weeks.Remove(week);
                await context.SaveChangesAsync();
            }
        }
    }
}
