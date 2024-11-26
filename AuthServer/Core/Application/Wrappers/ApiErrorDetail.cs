using AuthServer.Exceptions;

namespace AuthServer.Core.Application.Wrappers;

public class ApiErrorDetail
{
    /// <summary>
    /// Свойство
    /// </summary>
    public string Property { get; set; }

    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    public ApiErrorDetail()
    {
    }

    public ApiErrorDetail(string message, string property = null, string code = null)
    {
        Property = property;
        Message = message;
        Code = code;
    }

    public ApiErrorDetail(Error error, string property = null)
    {
        Message = error.Message;
        Code = error.Code;
        Property = property;
    }
}