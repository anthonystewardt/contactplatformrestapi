using contactplatformweb.Entities;

namespace contactplatformweb.DTOs
{
    public class CapaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CampaignId { get; set; }

        public int SubcampaignId { get; set; }


        public int SupervisorId { get; set; }

        public int TrainerId { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public bool Active { get; set; } = true;

        // Relación muchos a muchos con User
        public List<User> Users { get; set; } = new List<User>();
    }
}
