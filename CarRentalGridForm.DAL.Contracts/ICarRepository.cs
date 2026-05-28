using CarRentalGridForm.Models;

namespace CarRentalGridForm.DAL.Contracts
{
    /// <summary>
    /// Интерфейс для работы с хранилищем автомобилей
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Получить все автомобили
        /// </summary>
        List<Car> GetAll();

        /// <summary>
        /// Получить автомобиль по ID
        /// </summary>
        Car? GetById(int id);

        /// <summary>
        /// Добавить автомобиль
        /// </summary>
        Task<Car> AddAsync(Car car);

        /// <summary>
        /// Обновить автомобиль
        /// </summary>
        Task UpdateAsync(Car car);

        /// <summary>
        /// Удалить автомобиль по ID
        /// </summary>
        Task DeleteAsync(int id);
    }
}