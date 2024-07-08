namespace contactplatformweb.Entities
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        // belong to many users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
