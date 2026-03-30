using Microsoft.AspNetCore.Mvc;
using RepositoryInsights.Application.DTOs;
using RepositoryInsights.Application.Services;

namespace RepositoryInsights.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RepositoriesController : ControllerBase
{
    private readonly TrackedRepositoryService _service;

    public RepositoriesController(TrackedRepositoryService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TrackedRepositoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        RegisterTrackedRepositoryRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.RegisterAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrackedRepositoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var results = await _service.GetAllAsync(cancellationToken);
        return Ok(results);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TrackedRepositoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
