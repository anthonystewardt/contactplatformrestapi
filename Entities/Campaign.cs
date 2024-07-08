namespace contactplatformweb.Entities
{
    public class Campaign
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Cco { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
