using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    [PrimaryKey("Trophy_Id")]
    public class Trophy
    {
        public Guid Trophy_Id { get; set; }    

        public string Trophy_Name { get; set; }

        public double Trophy_Value { get; set; }

        public virtual Guid Trophy_RoundId { get; set; }

        public virtual Round Trophy_Round { get; set; }

        public virtual Guid? Trophy_KnightId { get; set; }

        public virtual AppUser Trophy_Knight { get; set; }

    }
}
