using System.Data;
using Microsoft.Data.SqlClient;
using FoodDist.Api.Hubs;
using FoodDist.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: builder.Configuration.GetConnectionString("FoodDistDb"),
        healthQuery: "SELECT 1;",
        name: "sqlserver",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "sqlserver" });

// SQL connection
builder.Services.AddTransient<IDbConnection>(sp => 
{
    var connection = new SqlConnection(builder.Configuration.GetConnectionString("FoodDistDb"));
    connection.Open();
    return connection;
});

// Repositories
builder.Services.AddScoped<ParcelRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<FoodDist.Api.Handlers.CreateParcelHandler>());

// SignalR
builder.Services.AddSignalR();

// API services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Endpoints
app.MapHealthChecks("/health");
app.MapControllers();
app.MapHub<ParcelHub>("/hubs/parcels");

// Test endpoint
app.MapGet("/testdb", async (IConfiguration config) => 
{
    try 
    {
        await using var connection = new SqlConnection(config.GetConnectionString("FoodDistDb"));
        await connection.OpenAsync();
        return Results.Ok("Database connection successful");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Database connection failed: {ex.Message}");
    }
});

app.Run();