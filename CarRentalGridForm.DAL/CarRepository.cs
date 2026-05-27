using CarRentalGridForm.Models;
using CarRentalGridForm.DAL.Contracts;

namespace CarRentalGridForm.DAL
{
    /// <summary>
    /// Репозиторий для работы с данными автомобилей в памяти.
    /// </summary>
    public class CarRepository : ICarRepository
    {
        private readonly List<Car> cars = new();
        private int nextId = 1;

        /// <summary>
        /// Инициализирует репозиторий и загружает начальные тестовые данные.
        /// </summary>
        public CarRepository()
        {
            SeedInitialData();
        }

        /// <summary>
        /// Возвращает список всех автомобилей из хранилища.
        /// </summary>
        /// <returns>Список всех автомобилей.</returns>
        public List<Car> GetAll() => new(cars);

        /// <summary>
        /// Возвращает автомобиль по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        /// <returns>Автомобиль с указанным ID или null, если не найден.</returns>
        public Car GetById(int id) => cars.FirstOrDefault(c => c.Id == id);

        /// <summary>
        /// Добавляет новый автомобиль в хранилище с присвоением уникального ID.
        /// </summary>
        /// <param name="car">Автомобиль для добавления.</param>
        public Task<Car> AddAsync(Car car)
        {
            car.Id = nextId++;
            cars.Add(car);
            return Task.FromResult(car);
        }

        /// <summary>
        /// Обновляет данные существующего автомобиля в хранилище.
        /// </summary>
        /// <param name="car">Автомобиль с обновлёнными данными.</param>
        public void Update(Car car)
        {
            var index = cars.FindIndex(c => c.Id == car.Id);
            if (index >= 0)
            {
                cars[index] = car;
            }
        }

        /// <summary>
        /// Удаляет автомобиль из хранилища по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля для удаления.</param>
        public void Delete(int id) => cars.RemoveAll(c => c.Id == id);

        private void SeedInitialData()
        {
            AddAsync(new Car
            {
                Brand = "Hyundai",
                LicensePlate = "А123БВ78",
                Mileage = 15000,
                AverageConsumption = 8.5,
                CurrentFuel = 45.0,
                RentCostPerMinute = 5.5m
            });

            AddAsync(new Car
            {
                Brand = "Lada",
                LicensePlate = "В456ГД78",
                Mileage = 32000,
                AverageConsumption = 7.2,
                CurrentFuel = 5.5,
                RentCostPerMinute = 3.0m
            });

            AddAsync(new Car
            {
                Brand = "Mitsubishi",
                LicensePlate = "Е789ЖЗ78",
                Mileage = 8000,
                AverageConsumption = 10.0,
                CurrentFuel = 60.0,
                RentCostPerMinute = 7.0m
            });
        }
    }
}