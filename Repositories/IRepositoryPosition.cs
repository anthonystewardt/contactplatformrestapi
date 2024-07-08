namespace contactplatformweb.Repositories
{
    public interface IRepositoryPosition
    {
        Task<List<Entities.Position>> GetPositions();
        Task<Entities.Position?> GetPosition(int id);
        Task<Entities.Position> AddPosition(Entities.Position position);
        Task<Entities.Position> UpdatePosition(Entities.Position position);
        Task<bool> ExistPosition(int id);
        Task DeletePosition(int id);
    }
}
