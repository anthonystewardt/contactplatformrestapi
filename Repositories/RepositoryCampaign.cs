using Microsoft.EntityFrameworkCore;
using contactplatformweb.Entities;

namespace contactplatformweb.Repositories
{
    public class RepositoryCampaign: IRepositoryCampaign
    {

        private readonly ApplicationDBContext _context;

        public RepositoryCampaign(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Campaign>> GetCampaigns()
        {
            return await _context.Campaigns.ToListAsync();
        }

        // create GetSubCampaigns method 
        public async Task<List<SubCampaign>> GetSubCampaigns(int id)
        {
            return await _context.SubCampaigns.Where(x => x.CampaignId == id).ToListAsync();
        }



        public async Task<Campaign?> GetCampaign(int id)
        {
            return await _context.Campaigns.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campaign> AddCampaign(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();
            return campaign;
        }

        public async Task<Campaign> UpdateCampaign(Campaign campaign)
        {
            _context.Campaigns.Update(campaign);
            await _context.SaveChangesAsync();
            return campaign;
        }

        public async Task DeleteCampaign(int id)
        {
            var campaign = await GetCampaign(id);
            if (campaign != null)
            {
                _context.Campaigns.Remove(campaign);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistCampaign(int id)
        {
            return await _context.Campaigns.AnyAsync(x => x.Id == id);
        }

        



    }
}
