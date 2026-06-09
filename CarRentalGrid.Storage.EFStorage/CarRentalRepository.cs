using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalGrid.Storage.EFStorage
{
    public class CarRentalRepository : ICarRepository
    {

        private readonly IReader reader;

        public CarRentalRepository(IReader reader)
        {
            this.reader = reader;
        }
        public Task<Car> AddAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> GetAllCarsAsync()
        {
            return reader.Read<Car>()
                .OrderBy(x => x.Brand)
                .ToListAsync();
        }

        public Task<Car?> GetByIdAsync(int id)
        {
            return reader.Read<Car>()
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
