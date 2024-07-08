namespace contactplatformweb.Repositories
{
    public interface IRepositoryCalendar
    {
        Task<List<Entities.Calendar>> GetCalendars();
        Task<Entities.Calendar?> GetCalendar(Guid id);
        Task<Entities.Calendar> AddCalendar(Entities.Calendar calendar);
        Task<bool> ExistCalendar(Guid id);
        Task<Entities.Calendar> UpdateCalendar(Entities.Calendar calendar);
        Task DeleteCalendar(Guid id);
    }
}