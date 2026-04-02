namespace CarRentalGridForm.Models
{
    /// <summary>
    /// Представляет автомобиль в системе проката.
    /// </summary>
    public class Car
    {
        public string Brand { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public double AverageConsumption { get; set; }
        public double CurrentFuel { get; set; }
        public decimal RentCostPerMinute { get; set; }

        /// <summary>
        /// Автоматически рассчитывает запас хода.
        /// </summary>
        public double Range => AverageConsumption > 0 ? CurrentFuel / AverageConsumption : 0;

        /// <summary>
        /// Автоматически рассчитывает итоговую сумму.
        /// </summary>
        public decimal TotalRentSum => (decimal)Range * RentCostPerMinute;
    }
}