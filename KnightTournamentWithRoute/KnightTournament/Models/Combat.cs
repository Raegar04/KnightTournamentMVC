using KnightTournament.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    [Serializable]
    public class Combat
    {
        public Guid Combat_Id { get; set; } = Guid.NewGuid();

        public DateTime Combat_StartDate { get; set; }

        public DateTime Combat_EndDate { get; set; }

        public CombatType Combat_Type { get; set; }

        public bool Combat_IsFinished { get; set; } = false;

        public virtual Guid Combat_RoundId { get; set; }

        public virtual Round Combat_Round { get; set; }

        public virtual ICollection<AppUser> Combat_AppUsers { get; set; }
    }
}
