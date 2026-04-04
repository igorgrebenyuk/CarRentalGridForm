namespace CarRentalGridForm.Models
{
    public class Statistics
    {
        /// <summary>
        /// Общее количество автомобилей в реестре.
        /// </summary>
        public int TotalCars { get; set; }

        /// <summary>
        /// Количество автомобилей с критическим уровнем топлива.
        /// </summary>
        public int LowFuelCars { get; set; }

        /// <summary>
        /// Суммарная стоимость аренды всех автомобилей.
        /// </summary>
        public decimal TotalValue { get; set; }
    }
}