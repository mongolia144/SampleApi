using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using Microsoft.AspNetCore.OpenApi;
using SampleApi.Services.MovieServices;
using SampleApi.Interfaces.MovieInterfaces;

var builder = WebApplication.CreateBuilder(args);

//Services:
//Scoped = correct for services that use EF Core, Because:
//EF Core DbContext is scoped, Repositories are scoped
//Scoped (✔ correct)
//One instance per HTTP request.
//Matches EF Core lifetime.

//Transient (✘ not ideal)
//New instance every time.
//Can cause multiple DbContext instances → problems.

//Singleton (✘ wrong)
//One instance for the whole app.
//Cannot use DbContext (not thread‑safe).
builder.Services.AddScoped<IMovieService, MovieService>();
//Repositiory Pattern:
builder.Services.AddScoped<IMovieRepository, MovieRepository>();


// EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SampleDb"));

// Controllers
builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ⭐ Swagger only in Development:
// Set "ASPNETCORE_ENVIRONMENT": "Development"
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API v1");
    });
}

app.MapControllers();

app.Run();
