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

    public EfCarRepository(IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }
    public async Task<Car> AddAsync(Car car)
    {
        writer.Add(car);
        await writer.SaveChangesAsync();
        return car;
    }

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

    public Task<List<Car>> GetAllCarsAsync()
    {
        return reader.Read  <Car>()
            .OrderBy(x => x.Brand)
            .ToListAsync();
    }

    public Task<Car?> GetByIdAsync(int id)
    {
        return reader.Read<Car>()
        .FirstOrDefaultAsync(x => x.Id == id);
    }

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