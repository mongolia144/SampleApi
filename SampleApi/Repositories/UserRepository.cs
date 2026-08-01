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
        // 🔥 Add this line here to see if the hash generated s the same as on creation
        // different hashes, different instances, and records might not be found
        //Console.WriteLine("Repository DbContext hash: " + _db.GetHashCode());
    }
    public async Task<User?> GetByEmail(string email)
    {
        //we don not use .FindAsync(u => u.Email == email) because email is not a primary key
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task Add(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}
