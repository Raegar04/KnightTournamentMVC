using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnightTournament.Models
{
    public class AppUser: IdentityUser<Guid>
    {
        public Rank Rank { get; set; }

        [DisallowSimilar]
        public override string? UserName { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<Trophy>? Trophies { get; set; }

        public virtual ICollection<Combat>? Combats { get; set; } 

        public virtual ICollection<Tournament> Tournaments { get; set; } 

        public virtual ICollection<Tournament> HoldedTournaments { get; set; }
    }
}
