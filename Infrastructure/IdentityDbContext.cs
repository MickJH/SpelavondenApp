using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure
{
    public class IdentityDbContext : IdentityDbContext<Person>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
            Persons = Set<Person>(); // Add DbSet for Person
        }

        public DbSet<Person> Persons { get; set; }  // Add DbSet for Person

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
