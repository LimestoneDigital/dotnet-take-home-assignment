namespace RepositoryInsights.Domain.Entities;

public class TrackedRepository
{
    public Guid Id { get; private set; }
    public string Owner { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }

    private TrackedRepository() { }

    public TrackedRepository(string owner, string name)
    {
        Id = Guid.NewGuid();
        Owner = owner.Trim();
        Name = name.Trim();
        DisplayName = $"{Owner}/{Name}";
        CreatedAtUtc = DateTime.UtcNow;
    }
}
