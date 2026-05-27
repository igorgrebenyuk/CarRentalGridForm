using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

public class MySqlCarRepository : ICarRepository
{
    List<Car> ICarRepository.GetAll()
    {
        using var db = new CarRentalContext();
        var items = db.Cars
        .AsNoTracking()
        .OrderBy(x => x.Brand)
        .ToList();
        return items;
    }

    Car? ICarRepository.GetById(int id)
    {
        using var db = new CarRentalContext();
        var item = db.Cars
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        return item;
    }

    async Task<Car> ICarRepository.AddAsync(Car car)
    {
        using var db = new CarRentalContext();
        db.Cars.Add(car);
        await db.SaveChangesAsync();
        return car;
    }

    void ICarRepository.Update(Car car)
    {
        using var db = new CarRentalContext();
        var item = db.Cars
            .FirstOrDefault(x => x.Id == car.Id);
        if (item != null)
        {
            
        item.Brand = car.Brand;
        item.LicensePlate = car.LicensePlate;
        item.Mileage = car.Mileage;
        item.AverageConsumption = car.AverageConsumption;
        item.CurrentFuel = car.CurrentFuel;
        item.RentCostPerMinute = car.RentCostPerMinute;


        db.Update(item);
        db.SaveChanges();
        }
    }

    void ICarRepository.Delete(int id)
    {
        using var db = new CarRentalContext();
        var item = db.Cars
            .FirstOrDefault(x => x.Id == id);
       if (item != null)
    {
        db.Cars.Remove(item);
        db.SaveChanges();
    }
    }
}