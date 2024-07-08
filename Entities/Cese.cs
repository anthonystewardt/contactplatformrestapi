namespace contactplatformweb.Entities
{
    public class Cese
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ReasonForDepartureId { get; set; }

        public ReasonForDeparture ReasonForDeparture { get; set; }

        // create a column to save a text (resumen) of the cese

        public string Resumen { get; set; }
    }
}
