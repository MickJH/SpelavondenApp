using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            BoardGameNights = Set<BoardGameNight>();
            BoardGames = Set<BoardGame>();
            Player = Set<Player>();
        }

        public DbSet<BoardGameNight> BoardGameNights { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Player> Player { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>()
                .OwnsOne(b => b.GameType);

            base.OnModelCreating(modelBuilder);
        }
    }
}
