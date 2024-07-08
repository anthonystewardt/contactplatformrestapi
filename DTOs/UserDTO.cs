namespace contactplatformweb.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? DNI { get; set; }
        public string? Username { get; set; }
        public string? UserCampaign { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Ip { get; set; }
        public string? Profession { get; set; }
        public string? ProfessionState { get; set; }
        public string? JobPosition { get; set; }
        public string? JobPositionState { get; set; }
        public string? Company { get; set; }

        public bool? Active { get; set; }

        public bool? HasCese { get; set; }
        public string? UrlImage { get; set; }
        public string Role { get; set; } = null!;
        public string? Cese { get; set; }
        public int? ConditionId { get; set; }
        public int? CampaignId { get; set; }
        public int? StateId { get; set; }
        public int? PositionId { get; set; }

        public DateOnly? DateStart { get; set; }

        public DateOnly? DateEnd { get; set; }

        public DateOnly? DateCese { get; set; }

        public string? userVpn { get; set; }


        public string? emailCompany { get; set; }

        public bool? IsSupervisor { get; set; } = null!;
        public bool? IsTrainer { get; set; } = null!;
        public int? ReasonForDepartureId { get; set; }
        public Guid? CalendarId { get; set; }

        public int? CapaId { get; set; } = null!;

        public int? ModalityId { get; set; } = null!;

        public int? SubCampaignId { get; set; } = null!;

        public bool? IsPostulate { get; set; } = null!;

    }
}
