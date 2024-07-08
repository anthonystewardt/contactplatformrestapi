namespace contactplatformweb.Repositories
{
    public interface IRepositoryCapa
    {
        Task<List<Entities.Capa>> GetCapas();
        Task<Entities.Capa?> GetCapa(int id);
        Task<Entities.Capa> AddCapa(Entities.Capa capa);
        Task<Entities.Capa> UpdateCapa(Entities.Capa capa);
        Task<bool> ExistCapa(int id);
        Task<Entities.User> GetUser(int id);
        Task RemoveUserFromCapa(int id, int idUser);
        Task DeleteCapa(int id);
    }
}
