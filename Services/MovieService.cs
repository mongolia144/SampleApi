
using SampleApi.Interfaces.MovieInterfaces;
using SampleApi.Models;
using SampleApi.DTOs.Movies;
using Microsoft.EntityFrameworkCore;
using SampleApi.Mappings.MovieMapping;

namespace SampleApi.Services.MovieServices;
class MovieService : IMovieService
{
    //ASYNC RULE
    //If your method uses await, it must be async.
    //If your method returns a Task directly, it must NOT be async
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDTORead>> GetAll()
    {
        var movieEntities = await _movieRepository.GetAll();
        var moviesDTO = new List<MovieDTORead>();
        foreach(Movie movieEntity in movieEntities)
        {
            moviesDTO.Add(MovieMapping.MapFromMovieEntityToMovieDTORead(movieEntity));
        }
        return moviesDTO;
    }
    public async Task<MovieDTORead?> GetById(string id)
    {
        var movieEntity = await _movieRepository.GetById(id);

        if (movieEntity == null)
            return null;

        return MovieMapping.MapFromMovieEntityToMovieDTORead(movieEntity);
    }
    public async Task<MovieDTORead> Add(MovieDTOAdd movieDTOAdd)
    {
        // validate

        // map
        var movieEntity = MovieMapping.MapFromMovieDTOAddToMovieEntity(movieDTOAdd);
        //EF Core tracks the entity that we have just added
        await _movieRepository.Add(movieEntity);
        return MovieMapping.MapFromMovieEntityToMovieDTORead(movieEntity);
    }  
    public async Task<MovieDTORead?> Update(string id, MovieDTOUpdate movieDTOUpdate)
    {
        //MovieDTORead?? = Because the service might not find the movie, and you need a way to express that possibility in the return type.
        //The service should always return MovieDTORead, never MovieDTOUpdate
        //DTOUpdate = input
        //DTORead = output
        //It represents what the API returns after the update.
        //Returning DTOUpdate makes no sense because:
        //DTOUpdate does not contain the Id
        //DTOUpdate does not represent the final state of the entity
        //DTOUpdate is not what clients expect after an update
        //DTOUpdate is not used anywhere else in your API
        var movieEntity = await _movieRepository.GetById(id);

        if (movieEntity == null)
            return null;

        movieEntity.Title = movieDTOUpdate.Title;
        movieEntity.Year = movieDTOUpdate.Year;

        await _movieRepository.Update(movieEntity);
        return MovieMapping.MapFromMovieEntityToMovieDTORead(movieEntity);
    }
    public async Task<bool> Delete(string id)
    {
        //if the entity cannot be found the EF will return false.
        var deleted = await _movieRepository.Delete(id);
        return deleted;
    }

}