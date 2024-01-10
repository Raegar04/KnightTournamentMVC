using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class TournamentDetailsViewModel
    {
        public Guid Tournament_Id { get; set; }

        public string Tournament_Name { get; set; }

        public string Tournament_Description { get; set; }

        public int Tournament_Scope { get; set; }

        public DateTime Tournament_StartDate { get; set; }

        public Status Tournament_Status { get; set; }

        public ICollection<Round> Tournament_Rounds { get; set; }

        public string ErrorMessage { get; set; }

        public Guid Tournament_LocationId { get; set; }

        public Guid Tournament_HolderId { get; set; }
    }
}
