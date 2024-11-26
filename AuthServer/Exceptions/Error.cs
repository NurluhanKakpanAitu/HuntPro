namespace AuthServer.Exceptions;

public sealed class Error
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Error"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="message">Сообщение ошибки.</param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Код ошибки.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Сообщение ошибки.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Получить пустую ошибку.
    /// </summary>
    /// <returns>Пустая ошибка.</returns>
    public static Error Empty() => new(string.Empty, string.Empty);
}