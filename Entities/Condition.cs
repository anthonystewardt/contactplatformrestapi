namespace contactplatformweb.Entities
{
    public class Condition
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
