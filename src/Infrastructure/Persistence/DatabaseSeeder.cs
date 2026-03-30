using Microsoft.EntityFrameworkCore;
using RepositoryInsights.Domain.Entities;

namespace RepositoryInsights.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.TrackedRepositories.AnyAsync())
            return;

        context.TrackedRepositories.AddRange(
            new TrackedRepository("dotnet", "runtime"),
            new TrackedRepository("microsoft", "vscode"));

        await context.SaveChangesAsync();
    }
}
