using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Car"/> через Entity Framework Core .
/// Реализует интерфейс <see cref="ICarRepository"/>.
/// </summary>
public class EfCarRepository : ICarRepository
{
    private readonly IReader reader;
    private readonly IWriter writer;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория с указанными зависимостями для чтения и записи.
    /// </summary>
    /// <param name="reader">Сервис для выполнения операций чтения из базы данных.</param>
    /// <param name="writer">Сервис для выполнения операций записи в базу данных.</param>
    public EfCarRepository(IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }
    /// <summary>
    /// Асинхронно добавляет новый автомобиль в базу данных и сохраняет изменения.
    /// </summary>
    public async Task<Car> AddAsync(Car car)
    {
        writer.Add(car);
        await writer.SaveChangesAsync();
        return car;
    }

    /// <summary>
    /// Асинхронно удаляет автомобиль из базы данных по его идентификатору.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var item = await reader.Read<Car>()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            writer.Delete(item);
            await writer.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Асинхронно возвращает отсортированный по марке список всех автомобилей из базы данных.
    /// </summary>
    public Task<List<Car>> GetAllCarsAsync()
    {
        return reader.Read  <Car>()
            .OrderBy(x => x.Brand)
            .ToListAsync();
    }
    
    /// <summary>
    /// Асинхронно возвращает автомобиль по его идентификатору.
    /// </summary>
    public Task<Car?> GetByIdAsync(int id)
    {
        return reader.Read<Car>()
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Асинхронно обновляет данные существующего автомобиля в базе данных.
    /// </summary>
    public async Task UpdateAsync(Car car)
    {
        var item = await reader.Read<Car>()
            .FirstOrDefaultAsync(x => x.Id == car.Id);
        if (item != null)
        {

            item.Brand = car.Brand;
            item.LicensePlate = car.LicensePlate;
            item.Mileage = car.Mileage;
            item.AverageConsumption = car.AverageConsumption;
            item.CurrentFuel = car.CurrentFuel;
            item.RentCostPerMinute = car.RentCostPerMinute;

            writer.Update(item);
            await writer.SaveChangesAsync();
        }
    }
}