using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryInsights.Domain.Entities;

namespace RepositoryInsights.Infrastructure.Persistence.Configurations;

public class TrackedRepositoryConfiguration : IEntityTypeConfiguration<TrackedRepository>
{
    public void Configure(EntityTypeBuilder<TrackedRepository> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Owner)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.DisplayName)
            .IsRequired()
            .HasMaxLength(401);

        builder.HasIndex(r => new { r.Owner, r.Name }).IsUnique();
    }
}
