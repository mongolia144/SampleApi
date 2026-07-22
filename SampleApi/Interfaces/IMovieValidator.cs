
using SampleApi.Models;
using SampleApi.Validators;

namespace SampleApi.Interfaces.MovieInterfaces;
public interface IMovieValidator
{
    ValidationResult Validate(Movie movie);
}
