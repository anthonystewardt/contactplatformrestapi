namespace contactplatformweb.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        public DateTime StartHour { get; set; }

        public DateTime EndHour { get; set; }

        public DateTime HourOfDay { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


    }
}
