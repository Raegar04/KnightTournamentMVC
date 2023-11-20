using KnightTournament.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.ViewModels
{
    public class TrophyDetailsViewModel
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public Guid RoundId { get; set; }
    }
}
