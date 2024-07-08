namespace contactplatformweb.Repositories
{
    public interface IRepositoryCampaign
    {
        Task<List<Entities.Campaign>> GetCampaigns();
        Task<Entities.Campaign?> GetCampaign(int id);
        Task<Entities.Campaign> AddCampaign(Entities.Campaign campaign);
        Task<Entities.Campaign> UpdateCampaign(Entities.Campaign campaign);

        // get the subcampaigns of a campaign
        Task<List<Entities.SubCampaign>> GetSubCampaigns(int id);

        //bool ExistCampaign(int id);
        //Task<bool> ExistCalendar(Guid id);
        Task<bool> ExistCampaign(int id);

        Task DeleteCampaign(int id);
    }
}
