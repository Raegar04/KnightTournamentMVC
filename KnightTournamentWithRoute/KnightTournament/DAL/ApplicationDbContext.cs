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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies(true).UseSqlServer(GetConString());

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Round>().HasOne(e => e.Round_Trophy).WithOne(e => e.Round);
            builder.Entity<Location>().HasMany(e => e.Location_Tournaments).WithOne(e => e.Tournament_Location).IsRequired(false);
            builder.Entity<Tournament>().HasOne(e => e.Tournament_Location).WithMany(e => e.Location_Tournaments).HasForeignKey(e => e.Tournament_LocationId).IsRequired(false);

            builder.Entity<AppUser>().HasMany(e => e.User_Trophies).WithOne(e => e.Knight).IsRequired(false);
            builder.Entity<Trophy>().HasOne(e => e.Knight).WithMany(e => e.User_Trophies).HasForeignKey(e => e.KnightId).IsRequired(false);

            builder.Entity<Tournament>().HasOne(e => e.Tournament_User).WithMany(e => e.User_HoldedTournaments).HasForeignKey(e => e.Tournament_HolderId);

            builder.Entity<Tournament>()
           .HasMany(t => t.Tournament_Knights)
           .WithMany(u => u.User_Tournaments)
            .UsingEntity<TournamentUsers>(
                j => j.HasOne(tu => tu.Knight)
                    .WithMany()
                    .HasForeignKey(tu => tu.KnightId)
                    .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne(tu => tu.Tournament)
                    .WithMany()
                    .HasForeignKey(tu => tu.TournamentId)
                    .OnDelete(DeleteBehavior.Restrict),
                j =>
                {
                    j.HasKey(tu => tu.Id);
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
            builder.AddJsonFile("appSettings.json");
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
