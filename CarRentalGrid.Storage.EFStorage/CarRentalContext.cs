using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage;

public class CarRentalContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    public CarRentalContext() => Database.EnsureCreated();

}