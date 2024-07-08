using contactplatformweb.Entities;
using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositoryState: IRepositoryState
    {
        private readonly ApplicationDBContext context;

        public RepositoryState(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<State> AddState(State state)
        {
            context.Add(state);
            await context.SaveChangesAsync();
            return state;
        }

        public async Task DeleteState(int id)
        {
            await context.States.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> ExistState(int id)
        {
            return await context.States.AnyAsync(x => x.Id == id);
        }

        public async Task<State> GetState(int id)
        {
            return await context.States.FindAsync(id);
        }

        public async Task<List<State>> GetStates()
        {
            return await context.States.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<State> UpdateState(State state)
        {
            context.Update(state);
            await context.SaveChangesAsync();
            return state;
        }

    }
}
