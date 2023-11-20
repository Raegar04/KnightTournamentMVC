using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    public class Trophy
    {
        public Guid Id { get; set; }    

        public string Name { get; set; }

        public double Value { get; set; }

        [ForeignKey("Round")]
        public virtual Guid RoundId { get; set; }

        public virtual Round Round { get; set; }

        public virtual Guid? KnightId { get; set; }

        public virtual AppUser Knight { get; set; }

    }
}
