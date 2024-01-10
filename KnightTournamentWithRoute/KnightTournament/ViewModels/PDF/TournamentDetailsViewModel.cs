using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class TournamentDetailsViewModelPDF
    {
        public Dictionary<string, string> Winners { get; set; }

        public string Tournament_Name { get; set; }

        public string Tournament_Description { get; set; }

        public int Tournament_Scope { get; set; }

        public DateTime Tournament_StartDate { get; set; }

        public Status Tournament_Status { get; set; }

        public Location Tournament_Location { get; set; }
    }
}
