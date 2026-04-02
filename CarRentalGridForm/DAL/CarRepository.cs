using System.ComponentModel;
using CarRentalGridForm.Models;

namespace CarRentalGridForm.DAL
{
    /// <summary>
    /// Управляет хранением данных об автомобилях.
    /// </summary>
    public class CarRepository
    {
        private BindingList<Car> carsList = new BindingList<Car>();

        /// <summary>
        /// Возвращает список машин для привязки к интерфейсу.
        /// </summary>
        public BindingList<Car> GetCars()
        {
            var result = carsList;
            return result;
        }
    }
}