using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

/// <summary>
/// Контекст БД для работы с товарами через MS SQL Server.
/// </summary>
public class CarRentalContext : DbContext
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

    /// <summary>
    /// Настраивает подключение к локальной базе данных MS SQL Server Express LocalDB.
    /// Строка подключения: <c>Server=(localdb)\mssqllocaldb;Database=CarRentaldb;Trusted_Connection=True;</c>
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRentaldb;Trusted_Connection=True;");
    }
}