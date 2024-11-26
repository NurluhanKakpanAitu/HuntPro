using AuthServer.Core.Application.Wrappers;

namespace AuthServer.Common.Wrappers;

internal class ApiResponse
{
    public bool IsSuccess { get; }

    public int StatusCode { get; }

    public List<ApiErrorDetail> Errors { get; } = new();

    protected ApiResponse()
    {
        IsSuccess = true;
        StatusCode = StatusCodes.Status200OK;
    }

    protected ApiResponse(IEnumerable<ApiErrorDetail> errors, int statusCode)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        Errors = errors.ToList();
    }

    public static ApiResponse Ok() => new();

    public static ApiResponse<T> Ok<T>(T data) => new(data);

    public static ApiResponse Error(int statusCode, IEnumerable<ApiErrorDetail> errors) => new(errors, statusCode);

    public static ApiResponse Error(int statusCode, ApiErrorDetail error) => new(new []{error}, statusCode);
}

internal sealed class ApiResponse<T> : ApiResponse
{
    public T Data { get; }

    public ApiResponse(T data)
    {
        Data = data;
    }
}