namespace WebApplication1.Models;

/// <summary>
/// Модель представления для отображения информации об ошибке.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Уникальный идентификатор запроса, вызвавшего ошибку.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Возвращает значение, указывающее, следует ли отображать RequestId (true, если он не пуст).
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}