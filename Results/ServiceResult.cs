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
}
