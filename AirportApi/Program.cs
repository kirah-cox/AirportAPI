using System.Text.Json.Serialization;
using AirportApi.Data;
using AirportApi.Services;
using Microsoft.EntityFrameworkCore;

// Load DB_* variables from the .env file at the repo root (searches parent folders).
// NoClobber: variables that are already set (e.g. by docker-compose) win over the file.
DotNetEnv.Env.NoClobber().TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// appsettings.json holds a template like "Host=DB_HOST;...". Swap each placeholder
// for the matching environment variable so no credentials live in appsettings.
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("ConnectionStrings:Default is missing from appsettings.json.");
foreach (var name in new[] { "DB_HOST", "DB_PORT", "DB_NAME", "DB_USER", "DB_PASSWORD" })
{
    var value = Environment.GetEnvironmentVariable(name)
        ?? throw new InvalidOperationException($"Missing environment variable {name}. Add it to the .env file (see .env.example).");
    connectionString = connectionString.Replace(name, value);
}
// Store the filled-in string back so anything reading ConnectionStrings:Default
// (including "dotnet ef ... Name=ConnectionStrings:Default") gets the real value.
builder.Configuration["ConnectionStrings:Default"] = connectionString;

builder.Services.AddDbContext<AirportContext>(options =>
    options.UseNpgsql(connectionString));

// App services: one instance per HTTP request (same lifetime as AirportContext).
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddControllers()
    // Scaffolded models link to each other (Employee -> Assignment -> Employee ...),
    // so skip repeated references instead of throwing when serializing to JSON.
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Serve the built React app from wwwroot (copied in by the Dockerfile).
// Locally, run the React dev server in client/ instead (npm run dev).
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// Any non-API URL that isn't a real file loads the React app.
app.MapFallbackToFile("index.html");

app.Run();
