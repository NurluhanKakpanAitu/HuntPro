namespace AuthServer.Exceptions;

public sealed class ValidationException : Exception 
{
    public string Code { get; }
    
    public ValidationException(string code, string message) : base(message)
    {
        Code = code;
    }
    
    public ValidationException(Error error) : base(error.Message)
    {
        Code = error.Code;
    }
}