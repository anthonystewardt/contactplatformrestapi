using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;



namespace contactplatformweb.Repositories
{
    public class RepositoryCapa: IRepositoryCapa
    {
        private readonly ApplicationDBContext _context;

        public RepositoryCapa(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public async Task<List<Entities.Capa>> GetCapas()
        {
            // .Include(u => u.User) 
           // return await _context.Capas.ToListAsync();
           return await _context.Capas.Include(u => u.Users).ToListAsync();
        }

        public async Task<Entities.Capa?> GetCapa(int id)
        {
            // include users
            //return await _context.Capas.FindAsync(id);
            return await _context.Capas.Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Entities.Capa> AddCapa(Entities.Capa capa)
        {
            _context.Capas.Add(capa);
            await _context.SaveChangesAsync();
            return capa;
        }


        public async Task<Entities.Capa> UpdateCapa(Entities.Capa capa)
        {
            _context.Capas.Update(capa);
            await _context.SaveChangesAsync();
            return capa;
        }

        public async Task<bool> ExistCapa(int id)
        {
            return await _context.Capas.AnyAsync(x => x.Id == id);
        }


        public async Task DeleteCapa(int id)
        {
            var capa = await _context.Capas.FindAsync(id);
            _context.Capas.Remove(capa);
            await _context.SaveChangesAsync();
        }

        public async Task<Entities.User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task RemoveUserFromCapa(int id, int idUser)
        {
            var capa = await _context.Capas.Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == id);
            var user = await _context.Users.FindAsync(idUser);
            if (capa != null && user != null)
            {
                capa.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


    }
}
