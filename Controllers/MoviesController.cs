using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;

namespace SampleApi.Controllers;

//using the repositiory pattern

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _repo;

    public MoviesController(IMovieRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IEnumerable<Movie>> Get()
    {
        return await _repo.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        await _repo.Add(movie);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        var movie = await _repo.GetById(id);

        if (movie == null)
            return NotFound();

        return movie;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Movie updated)
    {
        var existing = await _repo.GetById(id);

        if (existing == null)
            return NotFound();

        existing.Title = updated.Title;
        existing.Year = updated.Year;

        await _repo.Update(existing);

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _repo.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
