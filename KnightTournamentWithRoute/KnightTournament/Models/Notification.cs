namespace KnightTournament.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string? Message { get; set; }

        public bool IsNoticed { get; set; }

        public Guid requestFromId { get; set; }

        public virtual Guid KnightOwnerId { get; set; }

        public virtual AppUser KnightOwner { get; set; }
    }
}
