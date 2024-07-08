using contactplatformweb.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace contactplatformweb.Repositories
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly ApplicationDBContext context;

        public RepositoryUser(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<User> AddUser(User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        // delete multiple users
        public async Task DeleteUsers(List<int> ids)
        {
            var usersToDelete = await context.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
            context.Users.RemoveRange(usersToDelete);
            await context.SaveChangesAsync();
        }

        // deactivate multiple users
        public async Task DeactivateUsers(List<int> ids)
        {
            var usersToDeactivate = await context.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
            usersToDeactivate.ForEach(x => x.Active = false);
            await context.SaveChangesAsync();
        }

        //GetUsersByExpressionRegular
        public async Task<List<User>> GetUsersByExpressionRegular(string expression)
        {
            return await context.Users.Where(x => EF.Functions.Like(x.Name, $"%{expression}%")).ToListAsync();
        }

        // GetUserPostulate 
        public async Task<List<User>> GetUserPostulate()
        {
            return await context.Users.OrderBy(x => x.Name).Where(u => u.IsPostulate == true).ToListAsync();
        }

        // all users
        public async Task<List<User>> GetUsers()
        {
           //return await context.Users.Include(c => c.Campaign).OrderBy(x => x.Name).ToListAsync();
           return await context.Users.OrderBy(x => x.Name).Where(u => u.Active == true).ToListAsync();
        }

        // all users inactive
        public async Task<List<User>> GetUserInactives()
        {
            return await context.Users.OrderBy(x => x.Name).Where(u => u.Active == false).ToListAsync();
        }

        public async Task DeleteUser(int id)
        {
            await context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<User> GetUser(int id)
        {
            //return await context.Users.FindAsync(id);
            return await context.Users.Include(u => u.Calendar) // Incluir la entidad Calendar asociada
                .FirstOrDefaultAsync(u => u.Id == id);

        }


        public async Task<User> UpdateUser(User user)
        {
            //context.Update(user);
            //context.SaveChanges()
            context.Update(user);
            await context.SaveChangesAsync(); ;
            //return Task.FromResult(user);
            return user;
        }

        public async Task<bool> ExistUser(int id)
        {
            return await context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<Calendar> AddCalendar(Calendar calendar)
        {
            context.Calendars.Add(calendar);
            await context.SaveChangesAsync();
            return calendar;
        }

        public async Task<Calendar?> GetCalendar(Guid id)
        {
            return await context.Calendars.FindAsync(id);
        }

        // Login
        public async Task<User?> Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return await context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == hashedPassword);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
