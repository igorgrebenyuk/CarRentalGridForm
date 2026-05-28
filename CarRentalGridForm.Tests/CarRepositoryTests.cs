using CarRentalGridForm.DAL;
using CarRentalGridForm.Models;
using FluentAssertions;
using Xunit;

namespace CarRentalGridForm.Tests.DAL
{
    /// <summary>
    /// Класс тестов для CarRepository
    /// </summary>
    public class CarRepositoryTests
    {
        private readonly CarRepository repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CarRepositoryTests"/> с репозиторием
        /// </summary>
        public CarRepositoryTests()
        {
            repository = new CarRepository();
        }

        /// <summary>
        /// Тест проверяет, что конструктор создаёт три автомобиля с начальными данными
        /// </summary>
        [Fact]
        public void Constructor_CreatesThreeCarsWithInitialData()
        {
            // Arrange и Act уже выполнены в конструкторе

            // Assert
            var cars = repository.GetAll();
            cars.Should().HaveCount(3);
            cars.Should().Contain(c => c.Brand == "Hyundai" && c.LicensePlate == "А123БВ78");
            cars.Should().Contain(c => c.Brand == "Lada" && c.LicensePlate == "В456ГД78");
            cars.Should().Contain(c => c.Brand == "Mitsubishi" && c.LicensePlate == "Е789ЖЗ78");
        }

        /// <summary>
        /// Тест проверяет, что метод GetAll возвращает копию коллекции (не ту же ссылку)
        /// </summary>
        [Fact]
        public void GetAll_ReturnsCopyNotReference()
        {
            // Arrange уже выполнен в конструкторе
            var firstCall = repository.GetAll();

            // Act
            var secondCall = repository.GetAll();

            // Assert
            secondCall.Should().NotBeSameAs(firstCall);
            secondCall.Should().BeEquivalentTo(firstCall);
        }

        /// <summary>
        /// Тест проверяет, что метод GetAll возвращает все добавленные автомобили
        /// </summary>
        [Fact]
        public void GetAll_ReturnsAllCars()
        {
            // Arrange уже выполнен в конструкторе

            // Act
            var cars = repository.GetAll();

            // Assert
            cars.Should().HaveCount(3);
            cars.Should().OnlyContain(c => c.Id > 0);
        }

        /// <summary>
        /// Тест проверяет, что метод GetById возвращает автомобиль при наличии
        /// </summary>
        [Fact]
        public async Task GetById_ExistingId_ReturnsCar()
        {
            // Arrange уже выполнен в конструкторе
            var expectedId = repository.GetAll().First().Id;

            // Act
            var result = await repository.GetByIdAsync(expectedId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expectedId);
        }

        /// <summary>
        /// Тест проверяет, что метод GetById возвращает null при отсутствии автомобиля
        /// </summary>
        [Fact]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            // Arrange уже выполнен в конструкторе

            // Act
            var result = await repository.GetByIdAsync(9999);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Тест проверяет, что метод Add добавляет автомобиль с уникальным ID
        /// </summary>
        [Fact]
        public async Task Add_ValidCar_AssignsUniqueIdAndAddsToCollection()
        {
            // Arrange уже выполнен в конструкторе
            var initialCount = repository.GetAll().Count;
            var newCar = new Car
            {
                Brand = "Kia",
                LicensePlate = "К777КК77",
                Mileage = 5000,
                AverageConsumption = 7.5,
                CurrentFuel = 45.0,
                RentCostPerMinute = 4.5m
            };

            // Act
            await repository.AddAsync(newCar);

            // Assert
            repository.GetAll().Should().HaveCount(initialCount + 1);
            newCar.Id.Should().BeGreaterThan(0);
            repository.GetAll().Should().Contain(c => c.Id == newCar.Id);
        }

        /// <summary>
        /// Тест проверяет, что метод Add присваивает возрастающие ID при добавлении нескольких автомобилей
        /// </summary>
        [Fact]
        public async Task Add_MultipleCars_IncrementsId()
        {
            // Arrange уже выполнен в конструкторе
            var car1 = new Car { Brand = "A", LicensePlate = "А1", Mileage = 0, AverageConsumption = 8.0, CurrentFuel = 50, RentCostPerMinute = 5m };
            var car2 = new Car { Brand = "Б", LicensePlate = "Б2", Mileage = 0, AverageConsumption = 8.0, CurrentFuel = 50, RentCostPerMinute = 5m };

            // Act
            await repository.AddAsync(car1);
            await repository.AddAsync(car2);

            // Assert
            car2.Id.Should().Be(car1.Id + 1);
        }

        /// <summary>
        /// Тест проверяет, что метод Update заменяет существующий автомобиль новыми данными
        /// </summary>
        [Fact]
        public async Task Update_ExistingCar_ReplacesData()
        {
            // Arrange уже выполнен в конструкторе
            var originalId = repository.GetAll().First().Id;
            var updatedBrand = "Updated_" + Guid.NewGuid().ToString("N").Substring(0, 6);

            var updatedCar = new Car
            {
                Id = originalId,
                Brand = updatedBrand,
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.0,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            await repository.UpdateAsync(updatedCar);

            // Assert
            var carInStorage = repository.GetAll().First(c => c.Id == originalId);
            carInStorage.Brand.Should().Be(updatedBrand);
        }

        /// <summary>
        /// Тест проверяет, что метод Update не изменяет коллекцию при обновлении несуществующего автомобиля
        /// </summary>
        [Fact]
        public async Task Update_NonExistingCar_DoesNotChangeCollection()
        {
            // Arrange уже выполнен в конструкторе
            var originalCount = repository.GetAll().Count;
            var fakeTestCar = new Car
            {
                Id = 9999,
                Brand = "fakeTestCar",
                LicensePlate = "Х000ХХ77",
                Mileage = 0,
                AverageConsumption = 8.0,
                CurrentFuel = 50,
                RentCostPerMinute = 5m
            };

            // Act
            await repository.UpdateAsync(fakeTestCar);

            // Assert
            repository.GetAll().Should().HaveCount(originalCount);
            repository.GetAll().Should().NotContain(c => c.Id == 9999);
        }

        /// <summary>
        /// Тест проверяет, что метод Delete удаляет автомобиль из коллекции
        /// </summary>
        [Fact]
        public async Task Delete_ExistingCar_RemovesFromCollection()
        {
            // Arrange уже выполнен в конструкторе
            var carToDelete = repository.GetAll().First();
            var carId = carToDelete.Id;

            // Act
            await repository.DeleteAsync(carId);

            // Assert
            repository.GetAll().Should().HaveCount(2);
            repository.GetAll().Should().NotContain(c => c.Id == carId);
        }

        /// <summary>
        /// Тест проверяет, что метод Delete не изменяет коллекцию при удалении несуществующего автомобиля
        /// </summary>
        [Fact]
        public async Task Delete_NonExistingCar_DoesNotChangeCollection()
        {
            // Arrange уже выполнен в конструкторе
            var originalCount = repository.GetAll().Count;

            // Act
            await repository.DeleteAsync(9999);

            // Assert
            repository.GetAll().Should().HaveCount(originalCount);
        }
    }
}