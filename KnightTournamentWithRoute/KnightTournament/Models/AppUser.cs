using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnightTournament.Models
{
    public class AppUser: IdentityUser<Guid>
    {
        public Rank User_Rank { get; set; }

        [DisallowSimilar]
        public override string? UserName { get; set; }

        public int User_Rating { get; set; }

        public virtual ICollection<Trophy>? User_Trophies { get; set; }

        public virtual ICollection<Combat> User_Combats { get; set; } 

        public virtual ICollection<Tournament> User_Tournaments { get; set; } 

        public virtual ICollection<Tournament> User_HoldedTournaments { get; set; }
    }
}
