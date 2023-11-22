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

        public Status Status { get; set; } = Status.Planned;

        public virtual ICollection<Round> Rounds { get; set; }

        public virtual Guid? LocationId { get; set; }  = new Guid("c8df80c9-a8c6-4c07-919c-37fcfc0c731d");

        public virtual Location Location { get; set; }

        public virtual ICollection<AppUser> Knights { get; set; } 

        public virtual Guid HolderId { get; set; }  

        public virtual AppUser Holder { get; set; }
    }
}
