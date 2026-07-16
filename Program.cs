using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using Microsoft.AspNetCore.OpenApi; // ✔ correct Swagger namespace for .NET 10

var builder = WebApplication.CreateBuilder(args);

// EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SampleDb"));

// Controllers
builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
    NORMAL / INDUSTRY STANDARD BEHAVIOR
    Swagger normally enabled only in Development.
    Example of the standard approach:

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
*/

/*
    DEMO / PORTFOLIO BEHAVIOR (INTENTIONAL)
    Swagger enabled for ALL environments.
*/

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API v1");
});

app.MapControllers();

app.Run();
