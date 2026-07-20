using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;
using SampleApi.DTOs.Movies;
using SampleApi.Services.MovieServices;
using SampleApi.Interfaces.MovieInterfaces;
using Microsoft.AspNetCore.Authorization;

namespace SampleApi.Controllers;



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

    

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDTORead>> GetById(string id)
    {
        var MovieDTORead = await _movieService.GetById(id);

        if (MovieDTORead == null)
            return NotFound();

        return MovieDTORead;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(MovieDTOAdd movieDTOAdd)
    {
        var serviceResult =await _movieService.Add(movieDTOAdd);
        if (!serviceResult.Success)
            return BadRequest(serviceResult.Errors);
        
        //CreatedAtAction is NOT from EF Core.  
        //It comes from ASP.NET Core MVC, specifically from the ControllerBase class.
        //⭐ What CreatedAtAction actually does
        //It builds an HTTP 201 Created response and includes:
        //the Location header (URL of the newly created resource)
        //the response body (your DTO)
        return CreatedAtAction(nameof(GetById), new { id = serviceResult.Data!.Id }, serviceResult.Data);
        // null‑forgiving operator: serviceResult.Data!.Id
        // serviceResult.Data!.Id: serviceResult.Data can be null, so Data.Id would fail.
        // It tells the compiler:I know this value is not null here — trust me.
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, MovieDTOUpdate movieDTOUpdate)
    {
        var serviceResult =await _movieService.Update(id, movieDTOUpdate);
        if (!serviceResult.Success)
            return BadRequest(serviceResult.Errors);
        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var serviceResult = await _movieService.Delete(id);

        if (!serviceResult.Success)
            return BadRequest(serviceResult.Errors);

        return Ok(true); 
    }
}
