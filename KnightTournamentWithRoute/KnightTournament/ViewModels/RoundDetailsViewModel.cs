using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class RoundDetailsViewModel
    { 
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid TournamentId { get; set; }

        public ICollection<Combat> Combats { get; set; }

        public Guid TrophyId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
