using KnightTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace KnightTournament.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public async Task<IEnumerable<Guid>> GetTournamentsWithMostValuableTrophies(int? limit = 10) 
        {
            var tournaments = Database.SqlQueryRaw<Guid>($"EXEC GetTournamentsWithMostValuableTrophies @limit = {limit}");
            return await tournaments.ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Combat>().HasOne(e => e.Combat_Round).WithMany(e => e.Round_Combats).HasForeignKey(e => e.Combat_RoundId);

            builder.Entity<Tournament>().HasOne(e => e.Tournament_Location).WithMany(e => e.Location_Tournaments).HasForeignKey(e => e.Tournament_LocationId);

            builder.Entity<Round>().HasOne(e => e.Round_Tournament).WithMany(e => e.Tournament_Rounds).HasForeignKey(e => e.Round_TournamentId);
            builder.Entity<Tournament>().HasOne(e => e.Tournament_Location).WithMany(e => e.Location_Tournaments).HasForeignKey(e => e.Tournament_LocationId).IsRequired(false);

            builder.Entity<Tournament>().HasOne(e => e.Tournament_User).WithMany(e => e.User_HoldedTournaments).HasForeignKey(e => e.Tournament_HolderId);

            builder.Entity<Trophy>().HasOne(e => e.Trophy_Round).WithOne(e => e.Round_Trophy).HasForeignKey<Trophy>(e => e.Trophy_RoundId);

            builder.Entity<Trophy>().HasOne(e => e.Trophy_Knight).WithMany(e => e.User_Trophies).HasForeignKey(e => e.Trophy_KnightId).IsRequired(false);

            builder.Entity<Tournament>().HasOne(e => e.Tournament_User).WithMany(e => e.User_HoldedTournaments).HasForeignKey(e => e.Tournament_HolderId);

            builder.Entity<Tournament>()
           .HasMany(t => t.Tournament_Knights)
           .WithMany(u => u.User_Tournaments)
            .UsingEntity<TournamentUsers>(
                j => j.HasOne(tu => tu.TournamentUsers_Knight)
                    .WithMany()
                    .HasForeignKey(tu => tu.TournamentUsers_KnightId)
                    .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne(tu => tu.TournamentUsers_Tournament)
                    .WithMany()
                    .HasForeignKey(tu => tu.TournamentUsers_TournamentId)
                    .OnDelete(DeleteBehavior.Restrict),
                j =>
                {
                    j.HasKey(tu => tu.TournamentUsers_Id);
                    j.ToTable("TournamentUsers");
                }
            );

            builder.Entity<AppUser>()
           .HasMany(t => t.User_Combats)
           .WithMany(u => u.Combat_AppUsers)
           .UsingEntity<CombatsKnight>(
               j => j.HasOne(tu => tu.CombatsKnight_Combat)
                   .WithMany()
                   .HasForeignKey(tu => tu.CombatsKnight_CombatId)
                   .OnDelete(DeleteBehavior.Restrict),
               j => j.HasOne(tu => tu.CombatsKnight_Knight)
                   .WithMany()
                   .HasForeignKey(tu => tu.CombatsKnight_AppUserId)
                   .OnDelete(DeleteBehavior.Restrict),
               j =>
               {
                   j.HasKey(tu => tu.CombatsKnight_Id);
                   j.ToTable("CombatsKnights");
               }
           );

            base.OnModelCreating(builder);
        }

        private string GetConString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appSettings.Development.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<Combat> Combats { get; set; }

        public virtual DbSet<Round> Rounds { get; set; }

        public virtual DbSet<Trophy> Trophies { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<CombatsKnight> CombatsKnights { get; set; }

        public virtual DbSet<TournamentUsers> TournamentUsers { get; set; }
    }
}
