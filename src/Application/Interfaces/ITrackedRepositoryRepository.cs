using RepositoryInsights.Domain.Entities;

namespace RepositoryInsights.Application.Interfaces;

public interface ITrackedRepositoryRepository
{
    Task<TrackedRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TrackedRepository>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByOwnerAndNameAsync(string owner, string name, CancellationToken cancellationToken = default);
    Task AddAsync(TrackedRepository repository, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
