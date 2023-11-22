using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class TournamentDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Scope { get; set; }

        public DateTime StartDate { get; set; }

        public Status Status { get; set; }

        public ICollection<Round> Rounds { get; set; }

        public string ErrorMessage { get; set; }

        public Guid LocationId { get; set; }

        public Guid HolderId { get; set; }
    }
}
