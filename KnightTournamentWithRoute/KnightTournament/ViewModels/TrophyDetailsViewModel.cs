using KnightTournament.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.ViewModels
{
    public class TrophyDetailsViewModel
    {
        public string Trophy_Name { get; set; }

        public double Trophy_Value { get; set; }

        public Guid Trophy_RoundId { get; set; }
    }
}
