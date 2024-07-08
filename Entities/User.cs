namespace contactplatformweb.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? DNI { get; set; } = null!;
        public string? Username { get; set; } = null!;
        public string? UserCampaign { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Country { get; set; } = null!;
        public string? Ip { get; set; } = null!;
        public string? Profession { get; set; } = null!;
        public string? ProfessionState { get; set; } = null!;
        public string? JobPosition { get; set; } = null!;
        public string? JobPositionState { get; set; } = null!;
        public string? Company { get; set; } = null!;
        public string? UrlImage { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Cese { get; set; } = null!;

        public bool? HasCese { get; set; } = null!;

        public bool? Active { get; set; } = null!;

        public bool? IsSupervisor { get; set; } = null!;

        public bool? IsTrainer { get; set; } = null!;

        public DateOnly? DateStart { get; set; } = null!;

        public DateOnly? DateEnd { get; set; } = null!;

        public DateOnly? DateCese { get; set; } = null!;

        public string? userVpn { get; set; } = null!;

        public string? emailCompany { get; set; } = null!;
        public int? ConditionId { get; set; }
        public Condition? Condition { get; set; }
        public int? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        public int? StateId { get; set; }
        public State? State { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
        public int? ReasonForDepartureId { get; set; }
        public ReasonForDeparture? ReasonForDeparture { get; set; }
        public Guid? CalendarId { get; set; }
        public Calendar? Calendar { get; set; }
        // an user has a capa
        public int? CapaId { get; set; } = null!;

        public int? ModalityId { get; set; } = null!;

        public Modality? Modality { get; set; }

        public int? SubCampaignId { get; set; } = null!;

        public bool? IsPostulate { get; set; } = null!;
      
    }
}