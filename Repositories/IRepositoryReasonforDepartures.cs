

namespace contactplatformweb.Repositories
{
    public interface IRepositoryReasonForDepartures
    {
        Task<List<Entities.ReasonForDeparture>> GetReasonForDepartures();

        Task<Entities.ReasonForDeparture?> GetReasonForDeparture(int id);

        Task<Entities.ReasonForDeparture> AddReasonForDeparture(Entities.ReasonForDeparture ReasonForDeparture);

        Task<bool> ExistReasonForDeparture(int id);

        Task<Entities.ReasonForDeparture> UpdateReasonForDeparture(Entities.ReasonForDeparture ReasonForDeparture);

        Task DeleteReasonForDeparture(int id);
    }
}
