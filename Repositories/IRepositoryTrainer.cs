namespace contactplatformweb.Repositories
{
    public interface IRepositoryTrainer
    {
        Task<List<Entities.Trainer>> GetTrainers();
        Task<Entities.Trainer?> GetTrainer(int id);
        Task<Entities.Trainer> AddTrainer(Entities.Trainer trainer);
        Task<Entities.Trainer> UpdateTrainer(Entities.Trainer trainer);
        Task<bool> ExistTrainer(int id);
        Task DeleteTrainer(int id);
    }
}
