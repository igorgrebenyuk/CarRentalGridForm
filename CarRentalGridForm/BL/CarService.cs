using System.Linq;
using CarRentalGridForm.DAL;

namespace CarRentalGridForm.BL
{
    /// <summary>
    /// Сервис для управления бизнес-логикой проката.
    /// </summary>
    public class CarService
    {
        private CarRepository repository;

        public CarService(CarRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Возвращает общее количество машин.
        /// </summary>
        public int GetTotalCount()
        {
            var count = repository.GetCars().Count;
            return count;
        }

        /// <summary>
        /// Возвращает количество машин с топливом менее 7 литров.
        /// </summary>
        public int GetCriticalFuelCount()
        {
            var count = repository.GetCars().Count(c => c.CurrentFuel < 7);
            return count;
        }

        internal object GetLowFuelCount()
        {
            throw new NotImplementedException();
        }
    }
}