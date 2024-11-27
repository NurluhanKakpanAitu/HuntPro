namespace AuthServer.Core.Application.Wrappers;

/// <summary>
/// Класс контейнер для пагинации.
/// </summary>
/// <typeparam name="T">Тип данных.</typeparam>
public sealed class PagedContent<T>
    where T : class
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PagedContent{T}"/>.
    /// </summary>
    /// <param name="content">Список элементов.</param>
    /// <param name="pageNum">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    public PagedContent(
        IReadOnlyCollection<T> content,
        int pageNum = 1,
        int pageSize = 20) : this() 
    {
        Content = content;
        PageNum = pageNum;
        PageSize = pageSize;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PagedContent{T}"/>.
    /// </summary>
    public PagedContent()
    {
        Content = [];
        TotalCount = 0;
        PageNum = 1;
        PageSize = 20;
    }

    /// <summary>
    /// Список элементов.
    /// </summary>
    public IReadOnlyCollection<T> Content { get; set; }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNum { get; set; }

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int PageSize { get; set; }
}