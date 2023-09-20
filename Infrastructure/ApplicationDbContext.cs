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
        }

        public DbSet<BoardGameNight> BoardGameNights { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>()
                .OwnsOne(b => b.GameType);

            modelBuilder.Entity<BoardGame>()
                .OwnsOne(b => b.Genre);

            base.OnModelCreating(modelBuilder);
        }
    }

}
