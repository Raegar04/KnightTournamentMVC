using KnightTournament.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace KnightTournament.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
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
            builder.Entity<Round>().HasOne(e => e.Trophy).WithOne(e => e.Round);
            builder.Entity<Location>().HasMany(e => e.Tournaments).WithOne(e => e.Location).IsRequired(false);
            builder.Entity<Tournament>().HasOne(e => e.Location).WithMany(e => e.Tournaments).HasForeignKey(e => e.LocationId).IsRequired(false);

            builder.Entity<AppUser>().HasMany(e => e.Trophies).WithOne(e => e.Knight).IsRequired(false);
            builder.Entity<Trophy>().HasOne(e => e.Knight).WithMany(e => e.Trophies).HasForeignKey(e => e.KnightId).IsRequired(false);

            builder.Entity<Tournament>().HasOne(e => e.Holder).WithMany(e => e.HoldedTournaments).HasForeignKey(e => e.HolderId);

            builder.Entity<Tournament>()
           .HasMany(t => t.Knights)
           .WithMany(u => u.Tournaments)
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
           .HasMany(t => t.Combats)
           .WithMany(u => u.AssignedKnights)
           .UsingEntity<CombatsKnight>(
               j => j.HasOne(tu => tu.Combat)
                   .WithMany()
                   .HasForeignKey(tu => tu.CombatId)
                   .OnDelete(DeleteBehavior.Restrict),
               j => j.HasOne(tu => tu.Knight)
                   .WithMany()
                   .HasForeignKey(tu => tu.KnightId)
                   .OnDelete(DeleteBehavior.Restrict),
               j =>
               {
                   j.HasKey(tu => tu.Id);
                   j.ToTable("CombatsKnights");
               }
           );

            builder.Entity<AppUser>().HasMany(e => e.Notifications).WithOne(e => e.KnightOwner).HasForeignKey(e => e.KnightOwnerId);

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
        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<Combat> Combats { get; set; }

        public virtual DbSet<Round> Rounds { get; set; }

        public virtual DbSet<Trophy> Trophies { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<CombatsKnight> CombatsKnights { get; set; }

        public virtual DbSet<TournamentUsers> TournamentUsers { get; set; }
    }
}
