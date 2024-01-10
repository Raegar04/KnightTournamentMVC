using KnightTournament.Models.Enums;

namespace KnightTournament.ViewModels
{
    public class CombatPDF
    {
        public Dictionary<string, int> Knights { get; set; }

        public DateTime Combat_StartDate { get; set; }

        public DateTime Combat_EndDate { get; set; }

        public CombatType Combat_Type { get; set; }

        public string PathToImage { get; set; }
    }
}
