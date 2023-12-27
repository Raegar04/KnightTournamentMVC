

namespace KnightTournament.Models
{
    [Serializable]
    public class Round
    {
        public Guid Round_Id { get; set; } = Guid.NewGuid();

        public string Round_Name { get; set; }

        public string Round_Description { get; set; }

        public DateTime Round_StartDate { get; set; }

        public DateTime Round_EndDate { get; set; }

        public virtual Guid Round_TournamentId { get; set; }

        public virtual Tournament Round_Tournament { get; set; }

        public virtual ICollection<Combat> Round_Combats { get; set; }

        public virtual Trophy Round_Trophy { get; set; }
    }
}
