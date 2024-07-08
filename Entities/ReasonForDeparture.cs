namespace contactplatformweb.Entities
{
    public class ReasonForDeparture
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool Active { get; set; } = true;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
