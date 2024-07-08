using Microsoft.EntityFrameworkCore;

namespace contactplatformweb.Repositories
{
    public class RepositorySubCampaign: IRepositorySubCampaign
    {
        private readonly ApplicationDBContext context;

        public RepositorySubCampaign(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Entities.SubCampaign>> GetSubCampaigns()
        {
            return await context.SubCampaigns.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Entities.SubCampaign?> GetSubCampaign(int id)
        {
            return await context.SubCampaigns.FindAsync(id);
        }

        public async Task<Entities.SubCampaign> AddSubCampaign(Entities.SubCampaign subCampaign)
        {
            context.SubCampaigns.Add(subCampaign);
            await context.SaveChangesAsync();
            return subCampaign;
        }

        public async Task<Entities.SubCampaign> UpdateSubCampaign(Entities.SubCampaign subCampaign)
        {
            context.Entry(subCampaign).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return subCampaign;
        }

        public async Task<bool> ExistSubCampaign(int id)
        {
            return await context.SubCampaigns.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteSubCampaign(int id)
        {
            var subCampaign = await context.SubCampaigns.FindAsync(id);
            if (subCampaign != null)
            {
                context.SubCampaigns.Remove(subCampaign);
                await context.SaveChangesAsync();
            }
        }
    }
}
