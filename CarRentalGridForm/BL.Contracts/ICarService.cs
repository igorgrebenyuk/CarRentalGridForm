using CarRentalGridForm.Models;

namespace CarRentalGridForm.BL.Contracts
{
    /// <summary>
    /// Интерфейс сервиса для управления автомобилями.
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Возвращает список всех автомобилей.
        /// </summary>
        /// <returns>Список всех автомобилей.</returns>
        List<Car> GetAllCars();

        /// <summary>
        /// Возвращает автомобиль по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        /// <returns>Автомобиль с указанным ID.</returns>
        Car GetCarById(int id);

        /// <summary>
        /// Добавляет новый автомобиль после проверки валидности.
        /// </summary>
        /// <param name="car">Автомобиль для добавления.</param>
        void AddCar(Car car);

        /// <summary>
        /// Обновляет данные существующего автомобиля.
        /// </summary>
        /// <param name="car">Автомобиль с обновлёнными данными.</param>
        void UpdateCar(Car car);

        /// <summary>
        /// Удаляет автомобиль по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля для удаления.</param>
        void DeleteCar(int id);

        /// <summary>
        /// Формирует сводную статистику по парку автомобилей.
        /// </summary>
        /// <returns>Объект статистики с основными показателями.</returns>
        Statistics GetStatistics();
    }
}