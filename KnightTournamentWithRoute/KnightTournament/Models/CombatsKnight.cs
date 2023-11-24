using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    public class CombatsKnight
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public double Points { get; set; } = 0;

        public virtual Guid? CombatId { get; set; }

        public virtual Combat Combat { get; set; }

        public virtual Guid? AppUserId { get; set; }

        public virtual AppUser Knight { get; set; }
    }
}
