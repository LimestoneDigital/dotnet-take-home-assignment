using FluentAssertions;
using NSubstitute;
using RepositoryInsights.Application.DTOs;
using RepositoryInsights.Application.Interfaces;
using RepositoryInsights.Application.Services;
using RepositoryInsights.Domain.Entities;
using Xunit;

namespace RepositoryInsights.UnitTests.Services;

public class TrackedRepositoryServiceTests
{
    private readonly ITrackedRepositoryRepository _repository;
    private readonly TrackedRepositoryService _service;

    public TrackedRepositoryServiceTests()
    {
        _repository = Substitute.For<ITrackedRepositoryRepository>();
        _service = new TrackedRepositoryService(_repository);
    }

    [Fact]
    public async Task RegisterAsync_WithValidInput_CreatesRepositoryWithDerivedDisplayName()
    {
        _repository
            .ExistsByOwnerAndNameAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var request = new RegisterTrackedRepositoryRequest("dotnet", "runtime");

        var result = await _service.RegisterAsync(request);

        result.Owner.Should().Be("dotnet");
        result.Name.Should().Be("runtime");
        result.DisplayName.Should().Be("dotnet/runtime");
        result.Id.Should().NotBeEmpty();
        result.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

        await _repository.Received(1).AddAsync(
            Arg.Is<TrackedRepository>(r => r.DisplayName == "dotnet/runtime"),
            Arg.Any<CancellationToken>());

        await _repository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RegisterAsync_WithDuplicateRepository_ThrowsInvalidOperationException()
    {
        _repository
            .ExistsByOwnerAndNameAsync("dotnet", "runtime", Arg.Any<CancellationToken>())
            .Returns(true);

        var request = new RegisterTrackedRepositoryRequest("dotnet", "runtime");

        var act = () => _service.RegisterAsync(request);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*already being tracked*");
    }

    [Theory]
    [InlineData("", "runtime")]
    [InlineData("  ", "runtime")]
    [InlineData("dotnet", "")]
    [InlineData("dotnet", "  ")]
    public async Task RegisterAsync_WithMissingInput_ThrowsArgumentException(
        string owner, string name)
    {
        var request = new RegisterTrackedRepositoryRequest(owner, name);

        var act = () => _service.RegisterAsync(request);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
