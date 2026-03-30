using Microsoft.EntityFrameworkCore;
using RepositoryInsights.Application.Interfaces;
using RepositoryInsights.Domain.Entities;
using RepositoryInsights.Infrastructure.Persistence;

namespace RepositoryInsights.Infrastructure.Repositories;

public class TrackedRepositoryRepository : ITrackedRepositoryRepository
{
    private readonly AppDbContext _context;

    public TrackedRepositoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TrackedRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TrackedRepositories.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<TrackedRepository>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TrackedRepositories
            .OrderByDescending(r => r.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByOwnerAndNameAsync(
        string owner, string name, CancellationToken cancellationToken = default)
    {
        return await _context.TrackedRepositories
            .AnyAsync(r => r.Owner == owner && r.Name == name, cancellationToken);
    }

    public async Task AddAsync(TrackedRepository repository, CancellationToken cancellationToken = default)
    {
        await _context.TrackedRepositories.AddAsync(repository, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
