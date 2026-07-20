
using SampleApi.Models;
using SampleAPI.Validators;

namespace SampleApi.Interfaces.MovieInterfaces;
public interface IMovieValidator
{
    ValidationResult Validate(Movie movie);
}
