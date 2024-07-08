namespace contactplatformweb.Repositories
{
    public interface IRepositorySubCampaign
    {
        Task<List<Entities.SubCampaign>> GetSubCampaigns();
        Task<Entities.SubCampaign?> GetSubCampaign(int id);
        Task<Entities.SubCampaign> AddSubCampaign(Entities.SubCampaign subCampaign);
        Task<Entities.SubCampaign> UpdateSubCampaign(Entities.SubCampaign subCampaign);
        Task<bool> ExistSubCampaign(int id);
        Task DeleteSubCampaign(int id);
    }
}
