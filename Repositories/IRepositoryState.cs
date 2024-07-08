namespace contactplatformweb.Repositories
{
    public interface IRepositoryState
    {
        Task<List<Entities.State>> GetStates();

        Task<Entities.State?> GetState(int id);

        Task<Entities.State> AddState(Entities.State state);

        Task<bool> ExistState(int id);

        Task<Entities.State> UpdateState(Entities.State state);

        Task DeleteState(int id);
    }
}
