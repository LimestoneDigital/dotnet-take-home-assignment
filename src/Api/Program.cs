using Microsoft.EntityFrameworkCore;
using RepositoryInsights.Application.Interfaces;
using RepositoryInsights.Application.Services;
using RepositoryInsights.Infrastructure.Persistence;
using RepositoryInsights.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITrackedRepositoryRepository, TrackedRepositoryRepository>();
builder.Services.AddScoped<TrackedRepositoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Initialize the database and seed sample data on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await DatabaseSeeder.SeedAsync(db);
}

app.Run();

public partial class Program { }
