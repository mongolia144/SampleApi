
using SampleApi.Models;
using SampleApi.Validators;

namespace SampleApi.Interfaces.MovieInterfaces;
public interface IValidator<T>
{
    ValidationResult Validate(T entity);
}
