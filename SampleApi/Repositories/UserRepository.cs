using SampleApi.Models;
using SampleApi.DTOs.Auth;
using SampleApi.Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using SampleApi.Data;

namespace SampleApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
        // 🔥 Add this line here
        Console.WriteLine("Repository DbContext hash: " + _db.GetHashCode());
    }
    public async Task<User?> GetByEmail(string email)
    {
        //we don not use .FindAsync(u => u.Email == email) because email is not a primary key
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
