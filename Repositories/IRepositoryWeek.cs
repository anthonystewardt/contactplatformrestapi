namespace contactplatformweb.Repositories
{
    public interface IRepositoryWeek
    {
        Task<List<Entities.Week>> GetWeeks();
        Task<Entities.Week?> GetWeek(int id);
        Task<Entities.Week> AddWeek(Entities.Week week);
        Task<Entities.Week> UpdateWeek(Entities.Week week);

        Task<List<Entities.Week>> GetWeeksByUserId(int userId);
        Task<bool> ExistWeek(int id);
        Task DeleteWeek(int id);
    }
}
