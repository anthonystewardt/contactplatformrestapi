using contactplatformweb.Entities;

namespace contactplatformweb.DTOs
{
    public class SubCampaignDTO
    {
        public string Name { get; set; }
        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; }
    }
}
