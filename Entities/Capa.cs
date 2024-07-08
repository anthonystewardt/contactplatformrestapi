namespace contactplatformweb.Entities
{
    public class Capa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CampaignId { get; set; }

        public int SubcampaignId { get; set; }

        public int supervisorId { get; set; }

        public int trainerId { get; set; }

        public DateOnly DateStart { get; set; }

        public DateOnly DateEnd { get; set;}

        public bool Active { get; set; } = true;

        // has many users
        public List<User> Users { get; set; } = new List<User>();
    }
}
