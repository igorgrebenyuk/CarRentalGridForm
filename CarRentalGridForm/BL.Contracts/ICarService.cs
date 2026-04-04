using CarRentalGridForm.Models;

namespace CarRentalGridForm.BL.Contracts
{
    public interface ICarService
    {
        /// <summary>
        /// Возвращает список всех автомобилей.
        /// </summary>
        List<Car> GetAllCars();

        /// <summary>
        /// Возвращает автомобиль по уникальному идентификатору.
        /// </summary>
        Car GetCarById(int id);

        /// <summary>
        /// Добавляет новый автомобиль с проверкой данных.
        /// </summary>
        void AddCar(Car car);

        /// <summary>
        /// Обновляет данные существующего автомобиля.
        /// </summary>
        void UpdateCar(Car car);

        /// <summary>
        /// Удаляет автомобиль из системы по идентификатору.
        /// </summary>
        void DeleteCar(int id);

        /// <summary>
        /// Формирует сводную статистику по парку автомобилей.
        /// </summary>
        Statistics GetStatistics();
    }
}