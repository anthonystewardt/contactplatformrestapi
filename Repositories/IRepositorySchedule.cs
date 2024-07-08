namespace contactplatformweb.Repositories
{
    public interface IRepositorySchedule
    {
        Task<List<Entities.Schedule>> GetSchedules();

        Task<Entities.Schedule?> GetSchedule(Guid id);

        Task<Entities.Schedule> AddSchedule(Entities.Schedule schedule);

        Task<bool> ExistSchedule(Guid id);

        Task<Entities.Schedule> UpdateSchedule(Entities.Schedule schedule);

        Task DeleteSchedule(Guid id);


    }
}
