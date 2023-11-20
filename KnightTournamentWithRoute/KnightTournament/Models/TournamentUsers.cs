namespace KnightTournament.Models
{
    public class TournamentUsers
    {
        public Guid Id { get; set; }

        public virtual Guid TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual Guid KnightId { get; set; }

        public virtual AppUser Knight { get; set; }
    }
}
