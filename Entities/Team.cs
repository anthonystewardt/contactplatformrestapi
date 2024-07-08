namespace contactplatformweb.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? CampaignId { get; set; }

        public Campaign? Campaign { get; set; }

        public int? SupervisorId { get; set; } = null!;

        public User? Supervisor { get; set; } = null!;

        public int? ReasonDepartureId { get; set; } = null!;

        public ReasonForDeparture? ReasonForDeparture { get; set; } = null!;

        public DateOnly? DateCese { get; set; } = null!;

        // create column to write the reason for the departure in text
        public string? Body { get; set; } = null!;

        public List<User> Users { get; set; } = new List<User>();

    }
}
