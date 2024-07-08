namespace contactplatformweb.Entities
{
    public class SubCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; } = null!;
    }
}
