using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Car"/> через Entity Framework Core и MS SQL Server.
/// Реализует интерфейс <see cref="ICarRepository"/>.
/// </summary>
public class MySqlCarRepository : ICarRepository
{
    /// <summary>
    /// Асинхронно получает все автомобили из базы данных, отсортированные по марке.
    /// Использует <see cref="AsNoTracking"/> для оптимизации чтения.
    /// </summary>
    public async Task<List<Car>> GetAllCarsAsync()
    {
        using var db =  new CarRentalContext();
        var items = await db.Cars
        .AsNoTracking()
        .OrderBy(x => x.Brand)
        .ToListAsync();
        return items;
    }

    /// <summary>
    /// Асинхронно находит автомобиль по уникальному идентификатору.
    /// Возвращает <c>null</c>, если запись не найдена.
    /// </summary>
    async Task<Car?> ICarRepository.GetByIdAsync(int id)
    {
        using var db = new CarRentalContext();
        var item = await db.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return item;
    }

    /// <summary>
    /// Асинхронно добавляет новый автомобиль в базу данных и сохраняет изменения.
    /// </summary>
    async Task<Car> ICarRepository.AddAsync(Car car)
    {
        using var db = new CarRentalContext();
        db.Cars.Add(car);
        await db.SaveChangesAsync();
        return car;
    }

    /// <summary>
    /// Асинхронно обновляет существующий автомобиль по идентификатору.
    /// Если запись найдена, обновляет все поля и сохраняет изменения.
    /// </summary>
    async Task ICarRepository.UpdateAsync(Car car)
    {
        using var db = new CarRentalContext();
        var item = await db.Cars
            .FirstOrDefaultAsync(x => x.Id == car.Id);
        if (item != null)
        {

            item.Brand = car.Brand;
            item.LicensePlate = car.LicensePlate;
            item.Mileage = car.Mileage;
            item.AverageConsumption = car.AverageConsumption;
            item.CurrentFuel = car.CurrentFuel;
            item.RentCostPerMinute = car.RentCostPerMinute;


            db.Update(item);
            await db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Асинхронно удаляет автомобиль по идентификатору.
    /// Если запись найдена, удаляет её и сохраняет изменения.
    /// </summary>
    async Task ICarRepository.DeleteAsync(int id)
    {
        using var db = new CarRentalContext();
        var item = await db.Cars
            .FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            db.Cars.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}