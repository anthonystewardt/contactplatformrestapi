namespace contactplatformweb.Entities
{
    public class Supervisor
    {
        public int Id { get; set; }

        // userId 
        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
