namespace RepositoryInsights.Application.DTOs;

public record TrackedRepositoryResponse(
    Guid Id,
    string Owner,
    string Name,
    string DisplayName,
    DateTime CreatedAtUtc);
