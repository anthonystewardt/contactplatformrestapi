namespace contactplatformweb.Repositories
{
    public interface IRepositoryTeam
    {
        Task<List<Entities.Team>> GetTeams();
        Task<Entities.Team?> GetTeam(int id);
        Task<Entities.Team> AddTeam(Entities.Team team);
        Task<bool> ExistTeam(int id);
        Task<Entities.Team> UpdateTeam(Entities.Team team);

        // check if existe user by UserId
        Task<Entities.User> GetUser(int id);

        Task DeleteTeam(int id);
    }
}
