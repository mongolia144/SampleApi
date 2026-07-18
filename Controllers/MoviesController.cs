using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;
using SampleApi.DTOs.Movies;

namespace SampleApi.Controllers;
using SampleApi.Services.MovieServices;
using SampleApi.Interfaces.MovieInterfaces;

//using the repositiory pattern
//ASYNC RULE
//If your method uses await, it must be async.
//If your method returns a Task directly, it must NOT be async

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IEnumerable<MovieDTORead>> Get()
    {
        return await _movieService.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MovieDTOAdd movieDTOAdd)
    {
        var movieEntityCreated =await _movieService.Add(movieDTOAdd);
        //CreatedAtAction is NOT from EF Core.  
        //It comes from ASP.NET Core MVC, specifically from the ControllerBase class.
        //⭐ What CreatedAtAction actually does
        //It builds an HTTP 201 Created response and includes:
        //the Location header (URL of the newly created resource)
        //the response body (your DTO)
        return CreatedAtAction(nameof(GetById), new { id = movieEntityCreated.Id }, movieEntityCreated);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDTORead>> GetById(string id)
    {
        var MovieDTORead = await _movieService.GetById(id);

        if (MovieDTORead == null)
            return NotFound();

        return MovieDTORead;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, MovieDTOUpdate movieDTOUpdate)
    {
        var updated = await _movieService.Update(id, movieDTOUpdate);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _movieService.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
