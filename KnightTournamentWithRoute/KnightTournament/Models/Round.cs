

namespace KnightTournament.Models
{
    [Serializable]
    public class Round
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Guid TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Combat> Combats { get; set; }

        public virtual Trophy Trophy { get; set; }
    }
}
