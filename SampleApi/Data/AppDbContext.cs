using Microsoft.EntityFrameworkCore;
using SampleApi.Models;

namespace SampleApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "seed-user-1",
                Email = "test@example.com",
                Salt = "somesalt",
                HashedPassword = "6e5605d42fe720882511feecd48a6a44f2110d9d4713e1b5c4c70ed7519f9519"
            }
        );
    }


}
