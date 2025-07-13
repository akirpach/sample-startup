using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add this logging to see what endpoints are mapped
app.MapControllers();


// Log all registered endpoints
app.Lifetime.ApplicationStarted.Register(() =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("=== Registered Endpoints ===");

    // This will help us see what routes are actually registered
    foreach (var endpoint in app.Services.GetRequiredService<EndpointDataSource>().Endpoints)
    {
        logger.LogInformation($"Endpoint: {endpoint.DisplayName}");
    }
});


app.Run();



// Seeding data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Apply migrations automatically

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Name = "Albs" },
            new User { Name = "Bobs" }
        );
        db.SaveChanges();
    }
}
