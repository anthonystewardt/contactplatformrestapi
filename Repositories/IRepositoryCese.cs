namespace contactplatformweb.Repositories
{
    public interface IRepositoryCese
    {
        Task<List<Entities.Cese>> GetCeses();
        Task<Entities.Cese?> GetCese(int id);
        Task<Entities.Cese> AddCese(Entities.Cese cese);
        Task<Entities.Cese> UpdateCese(Entities.Cese cese);
        Task<bool> ExistCese(int id);

        Task<Entities.User> GetUser(int id);

        // GetCesesByUser
        Task<List<Entities.Cese>> GetCesesByUser(int id);
        Task DeleteCese(int id);
    }
}
