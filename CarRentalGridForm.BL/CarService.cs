using CarRentalGridForm.Models;
using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Constants;
using CarRentalGridForm.BL.Contracts;

namespace CarRentalGridForm.BL
{
    /// <summary>
    /// Сервис бизнес-логики для управления автомобилями.
    /// </summary>
    public class CarService : ICarService
    {
        private readonly ICarRepository repository;

        /// <summary>
        /// Создаёт экземпляр сервиса с указанным репозиторием.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными.</param>
        public CarService(ICarRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Возвращает список всех автомобилей из репозитория.
        /// </summary>
        /// <returns>Список всех автомобилей.</returns>
        public List<Car> GetAllCars() => repository.GetAll();

        /// <summary>
        /// Возвращает автомобиль по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        /// <returns>Автомобиль с указанным ID.</returns>
        public Car GetCarById(int id) => repository.GetById(id);

        /// <summary>
        /// Добавляет новый автомобиль после проверки валидности данных.
        /// </summary>
        /// <param name="car">Автомобиль для добавления.</param>
        public async Task AddCarAsync(Car car)
        {
            ValidateCar(car);
            await repository.AddAsync(car);
        }

        /// <summary>
        /// Обновляет данные существующего автомобиля после проверки валидности.
        /// </summary>
        /// <param name="car">Автомобиль с обновлёнными данными.</param>
        public void UpdateCar(Car car)
        {
            ValidateCar(car);
            repository.Update(car);
        }

        /// <summary>
        /// Удаляет автомобиль из системы по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля для удаления.</param>
        public void DeleteCar(int id) => repository.Delete(id);

        /// <summary>
        /// Формирует и возвращает статистику по парку автомобилей.
        /// </summary>
        /// <returns>Объект статистики с основными показателями.</returns>
        public Statistics GetStatistics()
        {
            var cars = repository.GetAll();

            return new Statistics
            {
                TotalCars = cars.Count,
                LowFuelCars = cars.Count(c => c.CurrentFuel < CarLimits.CriticalFuelLevel),
                TotalValue = cars.Sum(c => c.TotalRentSum)
            };
        }

        private void ValidateCar(Car car)
        {
            if (string.IsNullOrWhiteSpace(car.Brand))
            {
                throw new ArgumentException("Марка автомобиля не может быть пустой");
            }

            if (string.IsNullOrWhiteSpace(car.LicensePlate))
            { 
                throw new ArgumentException("Гос. номер не может быть пустым");
            }

            if (car.Mileage < 0)
            { 
                throw new ArgumentException("Пробег не может быть отрицательным");
            }

            if (car.AverageConsumption <= CarLimits.MinConsumption)
            {
                throw new ArgumentException($"Расход топлива должен быть больше {CarLimits.MinConsumption} литра");
            }

            if (car.AverageConsumption >= CarLimits.MaxConsumption)
            {
                throw new ArgumentException($"Расход топлива должен быть меньше {CarLimits.MaxConsumption} литров");
            }

            if (car.CurrentFuel < CarLimits.MinFuel || car.CurrentFuel > CarLimits.MaxFuel)
            { 
                throw new ArgumentException("Топливо должно быть в диапазоне 0-100 литров"); 
            }

            if (car.RentCostPerMinute <= CarLimits.MinRentCost)
            { 
                throw new ArgumentException("Стоимость аренды должна быть больше нуля"); 
            }
        }
    }
}