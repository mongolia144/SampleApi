using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SampleApi.Results;

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new();
    public T? Data { get; set; }

    public static ServiceResult<T> Ok(T data) =>
        new() { Success = true, Data = data };

    public static ServiceResult<T> Fail(List<string> errors) =>
        new() { Success = false, Errors = errors };

    public static string ErrorsToString(List<string> errors)
    {
        var errorString = "";
        foreach(var error in errors)
        {
            errorString = errorString + error;
        }
        return errorString;
    }
    
}
