namespace SampleApi.Interfaces.MovieInterfaces;

using SampleApi.DTOs.Movies;

public interface IMovieService
{
    Task<IEnumerable<MovieDTORead>> GetAll();
    Task<MovieDTORead?> GetById(string id);
    Task<MovieDTORead> Add(MovieDTOAdd movie);
    Task<MovieDTORead?> Update(string id, MovieDTOUpdate movie);
    Task<bool> Delete(string id);
}
