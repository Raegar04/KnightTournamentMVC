using KnightTournament.Attributes;
using KnightTournament.Models.Enums;

namespace KnightTournament.Models
{
    [Serializable]
    public class Tournament
    {
        public Guid Tournament_Id { get; set; } = Guid.NewGuid();

        public string Tournament_Name { get; set; }

        public string Tournament_Description { get; set; }

        public int Tournament_Scope { get; set; }

        public DateTime Tournament_StartDate { get; set; }

        public bool Tournament_IsFinished { get; set; } = false;

        public Status Tournament_Status { get; set; } = Status.Planned;

        public virtual ICollection<Round> Tournament_Rounds { get; set; }

        public virtual Guid? Tournament_LocationId { get; set; }  = new Guid("c8df80c9-a8c6-4c07-919c-37fcfc0c731d");

        public virtual Location Tournament_Location { get; set; }

        public virtual ICollection<AppUser> Tournament_Knights { get; set; } 

        public virtual Guid Tournament_HolderId { get; set; }  

        public virtual AppUser Tournament_User { get; set; }
    }
}
