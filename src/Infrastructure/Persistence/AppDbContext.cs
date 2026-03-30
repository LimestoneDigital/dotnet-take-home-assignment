using Microsoft.EntityFrameworkCore;
using RepositoryInsights.Domain.Entities;

namespace RepositoryInsights.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<TrackedRepository> TrackedRepositories => Set<TrackedRepository>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
