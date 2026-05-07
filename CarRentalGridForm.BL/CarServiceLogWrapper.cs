using CarRentalGridForm.Models;
using CarRentalGridForm.BL.Contracts;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CarRentalGridForm.BL
{
    /// <summary>
    /// Враппер для логирования производительности методов CarService
    /// </summary>
    public class CarServiceLogWrapper : ICarService
    {
        private readonly ICarService carService;
        private readonly ILogger<CarServiceLogWrapper> logger;

        /// <summary>
        /// Конструктор, принимающий сервис и логгер
        /// </summary>

        public CarServiceLogWrapper(ICarService carService, ILogger<CarServiceLogWrapper> logger)
        {
            this.carService = carService;
            this.logger = logger;
        }
        /// <summary>
        /// Возвращает все машины    с логированием производительности
        /// </summary>
        public List<Car> GetAllCars()
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = carService.GetAllCars();

            watch.Stop();
            var count = result != null ? result.Count : 0;
            logger.LogInformation("GetAllCars executed in {ElapsedMilliseconds} ms", watch.ElapsedMilliseconds, count);
            return result;
        }

        /// <summary>
        /// Возвращает машину по ID с логированием производительности
        /// </summary>
        public Car GetCarById(int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = carService.GetCarById(id);

            watch.Stop();
            var found = result != null;
            logger.LogInformation("GetCarById executed in {ElapsedMilliseconds} ms for ID {CarId}"
                , watch.ElapsedMilliseconds
                , found
                , id);
            return result;
        }

        /// <summary>
        /// Добавляет машину с логированием производительности
        /// </summary>
        public void AddCar(Car car)
        {
            var watch = new Stopwatch();
            watch.Start();

            carService.AddCar(car);

            watch.Stop();
            var brand = car != null ? car.Brand : null;
            var licensePlate = car != null ? car.LicensePlate : null;
            logger.LogDebug(
                "CarService.AddCar: {ElapsedMilliseconds} ms. Brand: {Brand}, LicensePlate: {LicensePlate}"
                , watch.ElapsedMilliseconds
                , brand
                , licensePlate);
        }

        /// <summary>
        /// Обновляет автомобиль с логированием производительности
        /// </summary>
        public void UpdateCar(Car car)
        {
            var watch = new Stopwatch();
            watch.Start();

            carService.UpdateCar(car);

            watch.Stop();
            var brand = car != null ? car.Brand : null;
            var id = car != null ? car.Id : 0;
            logger.LogDebug(
                "CarService.UpdateCar: {ElapsedMilliseconds} ms. Id: {Id}, Brand: {Brand}"
                , watch.ElapsedMilliseconds
                , id
                , brand);
        }

        /// <summary>
        /// Удаляет автомобиль по ID с логированием производительности
        /// </summary>
        public void DeleteCar(int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            carService.DeleteCar(id);

            watch.Stop();
            logger.LogDebug(
                "CarService.DeleteCar: {ElapsedMilliseconds} ms. Id: {Id}"
                , watch.ElapsedMilliseconds
                , id);
        }


        /// <summary>
        /// Возвращает статистику с логированием производительности
        /// </summary>
        public Statistics GetStatistics()
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = carService.GetStatistics();

            watch.Stop();
            var totalCars = result != null ? result.TotalCars : 0;
            var totalValue = result != null ? result.TotalValue : 0;
            var lowFuelCars = result != null ? result.LowFuelCars : 0;

            logger.LogDebug(
                "CarService.GetStatistics: {ElapsedMilliseconds} ms. TotalCars: {TotalCars}, TotalValue: {TotalValue}, LowFuelCars: {LowFuelCars}"
                , watch.ElapsedMilliseconds
                , totalCars
                , totalValue
                , lowFuelCars);

            return result;
        }
    }
}
