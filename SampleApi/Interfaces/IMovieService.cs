using SampleApi.DTOs.Movies;
using SampleApi.Results;

namespace SampleApi.Interfaces.MovieInterfaces;
public interface IMovieService
{
    Task<IEnumerable<MovieDTORead>> GetAll();
    Task<MovieDTORead?> GetById(string id);
    Task<ServiceResult<MovieDTORead>> Add(MovieDTOAdd movieDTOAdd);
    Task<ServiceResult<MovieDTORead>> Update(string id, MovieDTOUpdate movieDTOUpdate);
    Task<ServiceResult<bool>> Delete(string id);
}
