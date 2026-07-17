using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

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
