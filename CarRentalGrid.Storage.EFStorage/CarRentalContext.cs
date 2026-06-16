using CarRentalGridForm.Models;
using CarRentalGridForm.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

/// <summary>
/// Контекст БД для работы с товарами через MS SQL Server.
/// </summary>
public class CarRentalContext : DbContext, IReader, IWriter
{
    /// <summary>
    /// Набор данных автомобилей (<see cref="Car"/>).
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// Инициализирует контекст и автоматически создаёт базу данных при первом запуске,
    /// если она не существует (<see cref="Database.EnsureCreated"/>).
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста базы данных.</param>
    public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Возвращает IQueryable для чтения сущностей указанного типа без отслеживания изменений.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности для чтения.</typeparam>
    /// <returns>Запрос IQueryable для указанного типа сущности.</returns>
    public IQueryable<TEntity> Read<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();
    }

    /// <summary>
    /// Добавляет новую сущность в контекст для последующего сохранения.
    /// </summary>
    /// <typeparam name="TEntity">Тип добавляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности для добавления.</param>
    void IWriter.Add<TEntity>(TEntity entity)
    {
        base.Add(entity);
    }

    /// <summary>
    /// Помечает существующую сущность как измененную для последующего сохранения.
    /// </summary>
    /// <typeparam name="TEntity">Тип обновляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности с обновленными данными.</param>
    void IWriter.Update<TEntity>(TEntity entity)
    {
        base.Update(entity);
    }

    /// <summary>
    /// Помечает сущность на удаление из базы данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемой сущности.</typeparam>
    /// <param name="entity">Экземпляр сущности для удаления.</param>
    void IWriter.Delete<TEntity>(TEntity entity)
    {
        base.Remove(entity);
    }
}