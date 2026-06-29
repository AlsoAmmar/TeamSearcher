using Microsoft.EntityFrameworkCore;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
    public DbSet<Team> Team { get; set; }
    public DbSet<TeamPerson> TeamPersons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamPerson>(entity =>
        {
            entity.HasKey(tp => new { tp.PersonId, tp.TeamId });

            entity.HasOne(tp => tp.Person).WithMany(s => s.TeamPersons).HasForeignKey(tp => tp.PersonId);

            entity.HasOne(tp => tp.Team).WithMany(s => s.TeamPersons).HasForeignKey(tp => tp.TeamId);
        });

        modelBuilder.Entity<Person>()
            .Property(p => p.Id)
            .HasIdentityOptions(startValue: 1000);

        modelBuilder.Entity<Team>()
            .Property(t => t.Id)
            .HasIdentityOptions(startValue: 2000);
    }
}