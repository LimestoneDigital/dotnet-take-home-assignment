# Repository Insights API

A backend starter project for a technical take-home assignment. This API lets users register GitHub repositories they want to track locally.

> This is an intentionally incomplete starter project used as part of an interview exercise.
> See [TAKE_HOME.md](TAKE_HOME.md) for the assignment and [CANDIDATE_NOTES.md](CANDIDATE_NOTES.md) for the response template to fill in.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core with SQLite
- xUnit, FluentAssertions, NSubstitute (testing)

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

```bash
# Clone the repository
git clone <repo-url>
cd dotnet-take-home-assignment

# Run the API
dotnet run --project src/Api
```

The API starts on `http://localhost:5062` by default. A Swagger UI is available at `/swagger` in development mode.

The SQLite database (`repositories.db`) is created automatically on first run and seeded with two sample repositories.

## Running Tests

```bash
dotnet test
```

## API Endpoints

| Method | Path                  | Description                          |
|--------|-----------------------|--------------------------------------|
| POST   | `/repositories`       | Register a repository to track       |
| GET    | `/repositories`       | List all tracked repositories        |
| GET    | `/repositories/{id}`  | Get a single tracked repository      |

### Register a Repository

```bash
curl -X POST http://localhost:5062/repositories \
  -H "Content-Type: application/json" \
  -d '{"owner": "dotnet", "name": "aspnetcore"}'
```

### List All Repositories

```bash
curl http://localhost:5062/repositories
```

## Project Structure

```
src/
  Api/              Entry point, controllers, configuration
  Application/      Use cases, DTOs, service interfaces
  Domain/           Core entities
  Infrastructure/   EF Core persistence, repository implementations

tests/
  UnitTests/        Unit tests for application logic
  IntegrationTests/ Integration tests for API endpoints
```

## Architecture

The project uses a pragmatic layered architecture:

- **Domain** contains the core `TrackedRepository` entity with no external dependencies.
- **Application** holds the service layer, DTOs, and the repository interface. Business logic and validation live here.
- **Infrastructure** implements persistence using EF Core and SQLite.
- **Api** is a thin layer that wires everything together and exposes HTTP endpoints.

Dependencies flow inward: Api and Infrastructure depend on Application and Domain, but Domain depends on nothing.
