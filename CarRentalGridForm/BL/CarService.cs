using System.Linq;
using CarRentalGridForm.DAL;

namespace CarRentalGridForm.BL
{
    public class CarService
    {
        private CarRepository carRepository;

        public CarService(CarRepository repository)
        {
            carRepository = repository;
        }

        /// <summary>
        /// Считает общее количество машин.
        /// </summary>
        public int GetTotalCount()
        {
            var count = carRepository.GetCars().Count;
            return count;
        }

        /// <summary>
        /// Считает машины с низким уровнем топлива (меньше 7 литров).
        /// </summary>
        public int GetLowFuelCount()
        {
            var cars = carRepository.GetCars();
            // Используем LINQ для подсчета
            var count = cars.Count(c => c.CurrentFuel < 7.0);
            return count;
        }
    }
}