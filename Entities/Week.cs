using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace contactplatformweb.Entities
{
    public class Week
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        public Guid CalendarId { get; set; }



        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Monday { get; set; }

        public Boolean MondayIsActive { get; set; }

        public TimeSpan MondayStartTime { get; set; }

        public TimeSpan MondayEndTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Tuesday { get; set; }

        public Boolean TuesdayIsActive { get; set; }

        public TimeSpan TuesdayStartTime { get; set; }


        public TimeSpan TuesdayEndTime { get; set; }


        [DataType(DataType.Date)]
        public DateTime Wednesday { get; set; }

        public TimeSpan WednesdayStartTime { get; set; }

        public TimeSpan WednesdayEndTime { get; set; }

        public Boolean WednesdayIsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime Thursday { get; set; }

        public Boolean ThursdayIsActive { get; set; }

        public TimeSpan ThursdayStartTime { get; set; }

        public TimeSpan ThursdayEndTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Friday { get; set; }

        public Boolean FridayIsActive { get; set; }


        public TimeSpan FridayStartTime { get; set; }


        public TimeSpan FridayEndTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Saturday { get; set; }

        public Boolean SaturdayIsActive { get; set; }

        public TimeSpan SaturdayStartTime { get; set; }

        public TimeSpan SaturdayEndTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Sunday { get; set; }


        public Boolean SundayIsActive { get; set; }


        public TimeSpan SundayStartTime { get; set; }

        public TimeSpan SundayEndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
