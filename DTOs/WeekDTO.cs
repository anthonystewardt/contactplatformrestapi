namespace contactplatformweb.DTOs
{
    public class WeekDTO
    {public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid CalendarId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Monday { get; set; }
        public bool MondayIsActive { get; set; }

        public TimeSpan MondayStartTime { get; set; }

        public TimeSpan MondayEndTime { get; set; }

        public DateTime Tuesday { get; set; }

        public TimeSpan TuesdayStartTime { get; set; }

        public TimeSpan TuesdayEndTime { get; set; }
        public bool TuesdayIsActive { get; set; }

        public TimeSpan WednesdayStartTime { get; set; }

        public TimeSpan WednesdayEndTime { get; set; }
        public DateTime Wednesday { get; set; }
        public bool WednesdayIsActive { get; set; }
        public DateTime Thursday { get; set; }
        public bool ThursdayIsActive { get; set; }

        public TimeSpan ThursdayStartTime { get; set; }

        public TimeSpan ThursdayEndTime { get; set; }

        public DateTime Friday { get; set; }
        public bool FridayIsActive { get; set; }
        
        public TimeSpan FridayStartTime { get; set; }

        public TimeSpan FridayEndTime { get; set; }

        public DateTime Saturday { get; set; }
        public bool SaturdayIsActive { get; set; }

        public TimeSpan SaturdayStartTime { get; set; }

        public TimeSpan SaturdayEndTime { get; set; }

        public DateTime Sunday { get; set; }

        public TimeSpan SundayStartTime { get; set; }

        public TimeSpan SundayEndTime { get; set; }

        public bool SundayIsActive { get; set; }
        public int UserId { get; set; }
    }
}
