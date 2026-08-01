using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DTOs.Auth;
using SampleApi.DTOs.Movies;
using SampleApi.Interfaces.MovieInterfaces;
using SampleApi.Models;
using SampleApi.Results;
using SampleApi.Services.MovieServices;

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
    private readonly ILogger<AuthController> _logger;

    public MoviesController(IMovieService movieService, ILogger<AuthController> logger)
    {
        _movieService = movieService;
        _logger = logger;
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
        {
            _logger.LogInformation("MovieController.GetById Movie not found: {id}", id);
            return NotFound();
        }
            

        return MovieDTORead;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(MovieDTOAdd movieDTOAdd)
    {
        var serviceResult =await _movieService.Add(movieDTOAdd);
        if (!serviceResult.Success)
        {
            _logger.LogWarning("MovieController.Create Failed: Reason={Reason}", ServiceResult<MovieDTORead>.ErrorsToString(serviceResult.Errors));
            return BadRequest(serviceResult.Errors);
        }


        //CreatedAtAction is NOT from EF Core.  
        //It comes from ASP.NET Core MVC, specifically from the ControllerBase class.
        //⭐ What CreatedAtAction actually does
        //It builds an HTTP 201 Created response and includes:
        //the Location header (URL of the newly created resource)
        //the response body (your DTO)
        _logger.LogInformation("MovieController.Create Movie Created: {Id}", serviceResult.Data?.Id);
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
        {
            _logger.LogWarning("MovieController.Update Failed: Reason={Reason}", ServiceResult<MovieDTORead>.ErrorsToString(serviceResult.Errors));
            return BadRequest(serviceResult.Errors);
        }
        _logger.LogInformation("MovieController.Update Movie Updated: {Id}", serviceResult.Data?.Id);
        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var serviceResult = await _movieService.Delete(id);

        if (!serviceResult.Success)
        {
            _logger.LogWarning("MovieController.Delete Failed: Reason={Reason}", ServiceResult<bool>.ErrorsToString(serviceResult.Errors));
            return BadRequest(serviceResult.Errors);
        }

        _logger.LogInformation("MovieController.Delete Movie Deleted: {Id}", id);
        return Ok(true); 
    }
}
