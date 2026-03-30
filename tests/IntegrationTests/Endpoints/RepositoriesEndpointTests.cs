using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using RepositoryInsights.Application.DTOs;
using Xunit;

namespace RepositoryInsights.IntegrationTests.Endpoints;

public class RepositoriesEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RepositoriesEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsSeededRepositories()
    {
        var response = await _client.GetAsync("/repositories");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var repositories = await response.Content
            .ReadFromJsonAsync<List<TrackedRepositoryResponse>>();

        repositories.Should().NotBeNull();
        repositories.Should().Contain(r => r.DisplayName == "dotnet/runtime");
        repositories.Should().Contain(r => r.DisplayName == "microsoft/vscode");
    }

    [Fact]
    public async Task Register_WithValidInput_ReturnsCreatedWithLocation()
    {
        var request = new RegisterTrackedRepositoryRequest("facebook", "react");

        var response = await _client.PostAsJsonAsync("/repositories", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var result = await response.Content
            .ReadFromJsonAsync<TrackedRepositoryResponse>();

        result.Should().NotBeNull();
        result!.Owner.Should().Be("facebook");
        result.Name.Should().Be("react");
        result.DisplayName.Should().Be("facebook/react");
    }

    [Fact]
    public async Task Register_DuplicateRepository_ReturnsConflict()
    {
        var request = new RegisterTrackedRepositoryRequest("dotnet", "runtime");

        var response = await _client.PostAsJsonAsync("/repositories", request);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GetById_WithUnknownId_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/repositories/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
