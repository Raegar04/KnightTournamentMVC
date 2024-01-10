using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.Models
{
    [PrimaryKey("CombatsKnight_Id")]
    public class CombatsKnight
    {
        public Guid CombatsKnight_Id { get; set; } = Guid.NewGuid();

        public int CombatsKnight_Points { get; set; } = 0;

        public virtual Guid? CombatsKnight_CombatId { get; set; }

        public virtual Combat CombatsKnight_Combat { get; set; }

        public virtual Guid? CombatsKnight_AppUserId { get; set; }

        public virtual AppUser CombatsKnight_Knight { get; set; }
    }
}
