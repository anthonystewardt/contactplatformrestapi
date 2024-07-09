namespace contactplatformweb.Repositories
{
    public interface IRepositoryUser
    {
        Task<List<Entities.User>> GetUsers();
        Task<Entities.User?> GetUser(int id);

        Task<List<Entities.User>> GetUserInactives();

        // delete multiple users by list ids
        Task DeleteUsers(List<int> ids);

        // desactiva usuarios, no los elimina, agarra una lista de ids y pone la columna active en false
        Task DeactivateUsers(List<int> ids);

        //GetUsersByExpressionRegular
        Task<List<Entities.User>> GetUsersByExpressionRegular(string expression);

        // GetUserPostulate
        Task<List<Entities.User>> GetUserPostulate();

        // GetUsersSupervisor
        Task<List<Entities.User>> GetUsersSupervisor();

        Task<Entities.User> AddUser(Entities.User user);
        // check if exist user
        Task<bool> ExistUser(int id);
        Task<Entities.User> UpdateUser(Entities.User user);
        Task DeleteUser(int id);

        // addCalendar
        Task<Entities.Calendar> AddCalendar(Entities.Calendar calendar);  // Nuevo método para agregar un calendario

        // Login
        Task<Entities.User?> Login(string username, string password);

        Task<Entities.Calendar?> GetCalendar(Guid id);
    }
}
