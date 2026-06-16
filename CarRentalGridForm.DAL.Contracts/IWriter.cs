using System.Diagnostics.CodeAnalysis;

namespace CarRentalGridForm.DAL.Contracts;

/// <summary>
/// Интерфейс для выполнения операций записи (добавление, обновление, удаление) и сохранения изменений.
/// </summary>
public interface IWriter
{
    /// <summary>
    /// Добавляет новую сущность в хранилище.
    /// </summary>
    /// <typeparam name="TEntity">Тип добавляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности для добавления.</param>
    void Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    /// Обновляет данные существующей сущности в хранилище.
    /// </summary>
    /// <typeparam name="TEntity">Тип обновляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности с обновленными данными.</param>
    void Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    /// Удаляет сущность из хранилища.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности для удаления.</param>
    void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    /// Асинхронно сохраняет все накопленные изменения в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Количество записей, затронутых в базе данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}