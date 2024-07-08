using contactplatformweb.Entities;
using Microsoft.EntityFrameworkCore;


namespace contactplatformweb.Repositories
{
    public class RepositoryTeam: IRepositoryTeam
    {
        private readonly ApplicationDBContext context;

        public RepositoryTeam(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Team>> GetTeams()
        {
            return await context.Teams.Include(u => u.Users).Include(s => s.Supervisor).Include(d => d.ReasonForDeparture).Include(c => c.Campaign).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Team?> GetTeam(int id)
        {
            // return await _context.Capas.Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == id);
            return await context.Teams.Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Team> AddTeam(Team team)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            context.Entry(team).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> ExistTeam(int id)
        {
            return await context.Teams.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteTeam(int id)
        {
            var team = await context.Teams.FindAsync(id);
            if (team != null)
            {
                context.Teams.Remove(team);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(int id)
        {
            return await context.Users.FindAsync(id);
        }
    }
}
