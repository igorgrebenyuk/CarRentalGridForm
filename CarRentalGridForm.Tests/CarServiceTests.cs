using CarRentalGridForm.BL;
using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.Models;
using CarRentalGridForm.Constants;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarRentalGridForm.Tests.BL
{
    /// <summary>
    /// Класс тестов для сервиса управления автомобилями
    /// </summary>
    public class CarServiceTests
    {
        private readonly Mock<ICarRepository> mockRepository;
        private readonly CarService service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CarServiceTests"/> с мокированным репозиторием
        /// </summary>
        public CarServiceTests()
        {
            mockRepository = new Mock<ICarRepository>();
            service = new CarService(mockRepository.Object);
        }

        /// <summary>
        /// Тест проверяет что метод GetAllCars возвращает список автомобилей из репозитория
        /// </summary>
        [Fact]
        public void GetAllCars_ReturnsListOfCarsFromRepository()
        {
            // Arrange
            var expectedCars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", LicensePlate = "А111АА77", Mileage = 1000, AverageConsumption = 8.0, CurrentFuel = 50.0, RentCostPerMinute = 5.0m },
                new Car { Id = 2, Brand = "Honda", LicensePlate = "В222ВВ77", Mileage = 2000, AverageConsumption = 7.5, CurrentFuel = 40.0, RentCostPerMinute = 4.5m }
            };
            mockRepository.Setup(r => r.GetAll()).Returns(expectedCars);

            // Act
            var result = service.GetAllCars();

            // Assert
            result.Should().BeEquivalentTo(expectedCars);
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод GetCarById возвращает автомобиль при наличии
        /// </summary>
        [Fact]
        public void GetCarById_ExistingId_ReturnsCar()
        {
            // Arrange
            var expectedCar = new Car { Id = 42, Brand = "BMW", LicensePlate = "Е333ЕЕ77", Mileage = 5000, AverageConsumption = 9.0, CurrentFuel = 60.0, RentCostPerMinute = 6.0m };
            mockRepository.Setup(r => r.GetById(42)).Returns(expectedCar);

            // Act
            var result = service.GetCarById(42);

            // Assert
            result.Should().BeEquivalentTo(expectedCar);
            mockRepository.Verify(r => r.GetById(42), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод GetCarById возвращает null при отсутствии автомобиля
        /// </summary>
        [Fact]
        public void GetCarById_NonExistingId_ReturnsNull()
        {
            // Arrange
            mockRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns((Car)null);

            // Act
            var result = service.GetCarById(999);

            // Assert
            result.Should().BeNull();
            mockRepository.Verify(r => r.GetById(999), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при пустой марке автомобиля
        /// </summary>
        [Fact]
        public void AddCar_EmptyBrand_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = null,
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.0,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Марка автомобиля не может быть пустой");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при пустом гос. номере
        /// </summary>
        [Fact]
        public void AddCar_EmptyLicensePlate_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = null,
                Mileage = 1000,
                AverageConsumption = 8.0,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Гос. номер не может быть пустым");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при отрицательном пробеге
        /// </summary>
        [Fact]
        public void AddCar_NegativeMileage_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = -100,
                AverageConsumption = 8.0,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Пробег не может быть отрицательным");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при расходе топлива равном минимальному лимиту
        /// </summary>
        [Fact]
        public void AddCar_ConsumptionAtMinLimit_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = CarLimits.MinConsumption,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage($"Расход топлива должен быть больше {CarLimits.MinConsumption} литра");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при расходе топлива равном максимальному лимиту
        /// </summary>
        [Fact]
        public void AddCar_ConsumptionAtMaxLimit_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = CarLimits.MaxConsumption,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage($"Расход топлива должен быть меньше {CarLimits.MaxConsumption} литров");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при уровне топлива вне допустимого диапазона
        /// </summary>
        [Fact]
        public void AddCar_FuelOutOfRange_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.0,
                CurrentFuel = 101.0,
                RentCostPerMinute = 5.0m
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Топливо должно быть в диапазоне 0-100 литров");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar выбрасывает исключение при стоимости аренды равной минимальному лимиту
        /// </summary>
        [Fact]
        public void AddCar_RentCostAtMinLimit_ThrowsArgumentException()
        {
            // Arrange
            var invalidCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.0,
                CurrentFuel = 50.0,
                RentCostPerMinute = CarLimits.MinRentCost
            };

            // Act
            Action act = () => service.AddCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Стоимость аренды должна быть больше нуля");
            mockRepository.Verify(r => r.Add(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод AddCar добавляет валидный автомобиль в репозиторий
        /// </summary>
        [Fact]
        public void AddCar_ValidCar_AddsToRepository()
        {
            // Arrange
            var validCar = new Car
            {
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.5,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.5m
            };

            // Act
            service.AddCar(validCar);

            // Assert
            mockRepository.Verify(r => r.Add(validCar), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод UpdateCar обновляет валидный автомобиль через репозиторий
        /// </summary>
        [Fact]
        public void UpdateCar_ValidCar_UpdatesThroughRepository()
        {
            // Arrange
            var validCar = new Car
            {
                Id = 1,
                Brand = "Toyota",
                LicensePlate = "А123БВ78",
                Mileage = 1000,
                AverageConsumption = 8.5,
                CurrentFuel = 50.0,
                RentCostPerMinute = 5.5m
            };

            // Act
            service.UpdateCar(validCar);

            // Assert
            mockRepository.Verify(r => r.Update(validCar), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод UpdateCar не вызывает репозиторий при невалидных данных
        /// </summary>
        [Fact]
        public void UpdateCar_InvalidData_DoesNotCallRepository()
        {
            // Arrange
            var invalidCar = new Car { Brand = "  ", LicensePlate = "А123БВ78" };

            // Act
            Action act = () => service.UpdateCar(invalidCar);

            // Assert
            act.Should().Throw<ArgumentException>();
            mockRepository.Verify(r => r.Update(It.IsAny<Car>()), Times.Never);
        }

        /// <summary>
        /// Тест проверяет что метод DeleteCar вызывает удаление в репозитории с правильным ID
        /// </summary>
        [Fact]
        public void DeleteCar_ValidId_CallsRepositoryDelete()
        {
            // Arrange
            var carId = 42;

            // Act
            service.DeleteCar(carId);

            // Assert
            mockRepository.Verify(r => r.Delete(carId), Times.Once);
        }

        /// <summary>
        /// Тест проверяет что метод GetStatistics корректно рассчитывает статистику по парку автомобилей
        /// </summary>
        [Fact]
        public void GetStatistics_WithCars_ReturnsCorrectStats()
        {
            var cars = new List<Car>
            {
                new Car { CurrentFuel = 50.0, AverageConsumption = 10.0, RentCostPerMinute = 10m },
                new Car { CurrentFuel = 7.0, AverageConsumption = 10.0, RentCostPerMinute = 10m },
                new Car { CurrentFuel = 6.9, AverageConsumption = 10.0, RentCostPerMinute = 10m },
                new Car { CurrentFuel = 0.0, AverageConsumption = 10.0, RentCostPerMinute = 10m }
            };
            mockRepository.Setup(r => r.GetAll()).Returns(cars);

            // Act
            var stats = service.GetStatistics();

            // Assert
            stats.TotalCars.Should().Be(4);
            stats.LowFuelCars.Should().Be(2);
            stats.TotalValue.Should().Be(6390m);
        }

        /// <summary>
        /// Тест проверяет что метод GetStatistics возвращает нулевые значения при пустом списке автомобилей
        /// </summary>
        [Fact]
        public void GetStatistics_NoCars_ReturnsZeroStats()
        {
            // Arrange
            mockRepository.Setup(r => r.GetAll()).Returns(new List<Car>());

            // Act
            var stats = service.GetStatistics();

            // Assert
            stats.TotalCars.Should().Be(0);
            stats.LowFuelCars.Should().Be(0);
            stats.TotalValue.Should().Be(0);
        }
    }
}