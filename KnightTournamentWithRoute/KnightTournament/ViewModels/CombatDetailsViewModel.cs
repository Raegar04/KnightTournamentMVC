using KnightTournament.Models.Enums;
using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class ResultsCombatPDF
    {
        public Guid Combat_Id { get; set; }

        public DateTime Combat_StartDate { get; set; }

        public DateTime Combat_EndDate { get; set; }

        public CombatType Combat_Type { get; set; }

        public bool Combat_IsFinished { get; set; }

        public virtual Guid Combat_RoundId { get; set; }

        public string ErrorMessage { get; set; }

        public string PathToImage { get; set; }
    }
}
