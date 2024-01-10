using Microsoft.EntityFrameworkCore;

namespace KnightTournament.Models
{
    [PrimaryKey("Location_Id")]
    public class Location
    {
        public Guid Location_Id { get; set; } = Guid.NewGuid();

        public string Location_Country { get; set; }

        public string Location_City { get; set; }

        public string Location_Place { get; set; }

        public string Location_ImgUri { get; set; }

        public virtual ICollection<Tournament> Location_Tournaments { get; set; }


        public override string ToString()
        {
            return $"{Location_Country}. {Location_City}. {Location_Place}";
        }
    }
}
