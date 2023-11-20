namespace KnightTournament.Models
{
    public class CombatsKnight
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public double Points { get; set; }

        public virtual Guid CombatId { get; set; }

        public virtual Combat Combat { get; set; }

        public virtual Guid KnightId { get; set; }

        public virtual AppUser Knight { get; set; }
    }
}
