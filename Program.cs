using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using SampleApi.Services.MovieServices;
using SampleApi.Interfaces.MovieInterfaces;
using SampleAPI.Validators;
using SampleApi.Interfaces.AuthInterfaces;
using SampleApi.Services.AuthServices;
using SampleApi.Interfaces.UserInterfaces;
using SampleApi.Repositories;
//using Microsoft.OpenApi.Models;




var builder = WebApplication.CreateBuilder(args);

// Load JWT settings
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings.GetValue<string>("Key")
    ?? throw new Exception("JWT Key is missing in configuration");

var issuer = jwtSettings.GetValue<string>("Issuer")
    ?? throw new Exception("JWT Issuer is missing in configuration");

var audience = jwtSettings.GetValue<string>("Audience")
    ?? throw new Exception("JWT Audience is missing in configuration");

// Register services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieValidator, MovieValidator>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//User Repositiory
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Register AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SampleDb"));

// Controllers
builder.Services.AddControllers();

// ⭐ Register Authentication + JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(key)
        )
    };
});

// Authorization
builder.Services.AddAuthorization();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
}


// ⭐ Authentication + Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Test if user exist : uncomment this line to verify if you cannot login
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//
//    db.Database.EnsureDeleted();   // optional, but useful while debugging
//    db.Database.EnsureCreated();   // this is what triggers HasData
//
//    var users = db.Users.ToList();
//    Console.WriteLine($"Seeded users: {users.Count}");
//}


app.Run();
