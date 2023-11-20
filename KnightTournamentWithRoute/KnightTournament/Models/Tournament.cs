using KnightTournament.Attributes;
using KnightTournament.Models.Enums;

namespace KnightTournament.Models
{
    [Serializable]
    public class Tournament
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Description { get; set; }

        public int Scope { get; set; }

        public DateTime StartDate { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }

        public virtual Guid? LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<AppUser> Knights { get; set; } 

        public virtual Guid HolderId { get; set; }  

        public virtual AppUser Holder { get; set; }
    }
}
