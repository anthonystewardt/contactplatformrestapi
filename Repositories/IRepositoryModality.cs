namespace contactplatformweb.Repositories
{
    public interface IRepositoryModality
    {
        Task<List<Entities.Modality>> GetModalities();
        Task<Entities.Modality?> GetModality(int id);
        Task<Entities.Modality> AddModality(Entities.Modality modality);
        Task<Entities.Modality> UpdateModality(Entities.Modality modality);
        Task<bool> ExistModality(int id);
        Task DeleteModality(int id);
    }
}
