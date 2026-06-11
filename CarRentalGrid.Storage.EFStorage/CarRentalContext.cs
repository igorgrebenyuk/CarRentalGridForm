using CarRentalGridForm.Models;
using CarRentalGridForm.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

/// <summary>
/// Контекст БД для работы с товарами через MS SQL Server.
/// </summary>
public class CarRentalContext : DbContext, IReader , IWriter
{
    /// <summary>
    /// Набор данных автомобилей (<see cref="Car"/>).
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// Инициализирует контекст и автоматически создаёт базу данных при первом запуске,
    /// если она не существует (<see cref="Database.EnsureCreated"/>).
    /// </summary>
    public CarRentalContext() => Database.EnsureCreated();

    public IQueryable<TEntity> Read<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();
    }

    void IWriter.Add<TEntity>(TEntity entity)
    {
        base.Add(entity);
    }

    void IWriter.Update<TEntity>(TEntity entity)
    {
        base.Update(entity);
    }

    void IWriter.Delete<TEntity>(TEntity entity)
    {
        base.Remove(entity);
    }

}