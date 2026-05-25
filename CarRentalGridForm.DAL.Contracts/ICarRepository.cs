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
        Car Add(Car car);

        /// <summary>
        /// Обновить автомобиль
        /// </summary>
        void Update(Car car);

        /// <summary>
        /// Удалить автомобиль по ID
        /// </summary>
        void Delete(int id);
    }
}