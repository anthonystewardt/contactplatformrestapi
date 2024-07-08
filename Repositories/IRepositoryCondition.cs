namespace contactplatformweb.Repositories
{
    public interface IRepositoryCondition
    {
        Task<List<Entities.Condition>> GetConditions();
        Task<Entities.Condition?> GetCondition(int id);
        Task<Entities.Condition> AddCondition(Entities.Condition condition);
        Task<Entities.Condition> UpdateCondition(Entities.Condition condition);
        Task<bool> ExistCondition(int id);
        Task DeleteCondition(int id);
    }
}
