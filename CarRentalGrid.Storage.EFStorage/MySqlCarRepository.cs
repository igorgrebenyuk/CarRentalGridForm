using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

public class MySqlCarRepository : ICarRepository
{
    public async Task<List<Car>> GetAllCarsAsync()
    {
        using var db =  new CarRentalContext();
        var items = await db.Cars
        .AsNoTracking()
        .OrderBy(x => x.Brand)
        .ToListAsync();
        return items;
    }

    async Task<Car?> ICarRepository.GetByIdAsync(int id)
    {
        using var db = new CarRentalContext();
        var item = await db.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return item;
    }

    async Task<Car> ICarRepository.AddAsync(Car car)
    {
        using var db = new CarRentalContext();
        db.Cars.Add(car);
        await db.SaveChangesAsync();
        return car;
    }

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