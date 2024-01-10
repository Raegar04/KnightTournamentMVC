using Microsoft.EntityFrameworkCore;

namespace KnightTournament.Models
{
    [PrimaryKey("TournamentUsers_Id")]
    public class TournamentUsers
    {
        public Guid TournamentUsers_Id { get; set; } = Guid.NewGuid();

        public virtual Guid TournamentUsers_TournamentId { get; set; }

        public virtual Tournament TournamentUsers_Tournament { get; set; }

        public virtual Guid TournamentUsers_KnightId { get; set; }

        public virtual AppUser TournamentUsers_Knight { get; set; }
    }
}
