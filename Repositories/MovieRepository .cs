using SampleApi.Models;
using SampleApi.Data;
using Microsoft.EntityFrameworkCore;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _db;

    public MovieRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _db.Movies.ToListAsync();
    }

    public async Task<Movie?> GetById(string id)
    {
        return await _db.Movies.FindAsync(id);
    }

    public async Task Add(Movie movie)
    {
        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Movie movie)
    {
        _db.Movies.Update(movie);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var movie = await _db.Movies.FindAsync(id);

        if (movie == null)
            return false;

        _db.Movies.Remove(movie);
        await _db.SaveChangesAsync();
        return true;
    }
}
