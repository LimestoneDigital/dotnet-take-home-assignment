using RepositoryInsights.Application.DTOs;
using RepositoryInsights.Application.Interfaces;
using RepositoryInsights.Domain.Entities;

namespace RepositoryInsights.Application.Services;

public class TrackedRepositoryService
{
    private readonly ITrackedRepositoryRepository _repository;

    public TrackedRepositoryService(ITrackedRepositoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrackedRepositoryResponse> RegisterAsync(
        RegisterTrackedRepositoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Owner))
            throw new ArgumentException("Owner is required.", nameof(request));

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name is required.", nameof(request));

        var owner = request.Owner.Trim();
        var name = request.Name.Trim();

        var exists = await _repository.ExistsByOwnerAndNameAsync(owner, name, cancellationToken);
        if (exists)
            throw new InvalidOperationException(
                $"Repository '{owner}/{name}' is already being tracked.");

        var trackedRepo = new TrackedRepository(owner, name);

        await _repository.AddAsync(trackedRepo, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return MapToResponse(trackedRepo);
    }

    public async Task<IReadOnlyList<TrackedRepositoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var repositories = await _repository.GetAllAsync(cancellationToken);
        return repositories.Select(MapToResponse).ToList();
    }

    public async Task<TrackedRepositoryResponse?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var repository = await _repository.GetByIdAsync(id, cancellationToken);
        return repository is null ? null : MapToResponse(repository);
    }

    private static TrackedRepositoryResponse MapToResponse(TrackedRepository repo) =>
        new(repo.Id, repo.Owner, repo.Name, repo.DisplayName, repo.CreatedAtUtc);
}
