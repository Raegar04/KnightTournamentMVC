using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class RoundDetailsViewModel
    { 
        public Guid Round_Id { get; set; }

        public string Round_Name { get; set; }

        public string Round_Description { get; set; }

        public DateTime Round_StartDate { get; set; }

        public DateTime Round_EndDate { get; set; }

        public Guid Round_TournamentId { get; set; }

        public ICollection<Combat> Round_Combats { get; set; }

        public Guid Round_TrophyId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
