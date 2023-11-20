namespace KnightTournament.Models
{
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Country { get; set; }

        public string City { get; set; }

        public string Place { get; set; }

        public string ImgUri { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
