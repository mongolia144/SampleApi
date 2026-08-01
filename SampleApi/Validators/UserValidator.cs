
using SampleApi.Models;
using SampleApi.Validators;
using SampleApi.Interfaces.MovieInterfaces;

namespace SampleApi.Validators;
public class UserValidator: IValidator<User>
{
    public ValidationResult Validate(User user)
    {
        var validationResult = new ValidationResult()
        {
            IsValid = true,
            Errors = new List<string>()
        };
        if (user == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("The user cannot be null");
            return validationResult; 
            // if the movie is null it does not make sense to check the rest , 
            // because then for instance movie.Title will throw an exception (the title of something that is null)
        }
        if(string.IsNullOrWhiteSpace(user.Email))
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("The E-mail of the User cannot be empty");
        }
        if (user.Salt == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("The Salt of the User cannot be empty");
        }
        if (user.PasswordHash == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("The PasswordHash of the User cannot be empty");
        }

        return validationResult;
    }
}