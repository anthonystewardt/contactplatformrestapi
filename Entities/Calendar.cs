namespace contactplatformweb.Entities
{
    public class Calendar
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        public TimeSpan StartHour { get; set; }

        public TimeSpan EndHour { get; set; }

        // One Calendar has many Users
        public ICollection<User> Users { get; set; } = new List<User>();


    }
}
