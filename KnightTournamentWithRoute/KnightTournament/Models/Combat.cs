using KnightTournament.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    [Serializable]
    public class Combat
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CombatType Type { get; set; }

        public bool IsFinished { get; set; } = false;

        public virtual Guid RoundId { get; set; }

        public virtual Round Round { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
