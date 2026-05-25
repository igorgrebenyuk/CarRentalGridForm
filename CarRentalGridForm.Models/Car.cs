using CarRentalGridForm.Constants;

namespace CarRentalGridForm.Models
{
    
    /// <summary>
    /// Модель автомобиля для системы проката.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Индетификатор машины
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Марка автомобиля.
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Государственный регистрационный номер.
        /// </summary>
        public string LicensePlate { get; set; } = string.Empty;

        /// <summary>
        /// Пробег автомобиля в километрах.
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Средний расход топлива в литрах на 100 км.
        /// </summary>
        public double AverageConsumption { get; set; }

        /// <summary>
        /// Текущий объём топлива в баке в литрах.
        /// </summary>
        public double CurrentFuel { get; set; }

        /// <summary>
        /// Стоимость аренды за одну минуту в рублях.
        /// </summary>
        public decimal RentCostPerMinute { get; set; }

        /// <summary>
        /// Расчётный запас хода в часах.
        /// </summary>
        public double Range
        {
            get
            {
                if (AverageConsumption <= CarLimits.MinConsumptionThreshold)
                    return 0;

                return Math.Round(
                    (CurrentFuel / AverageConsumption) * CarLimits.FuelConsumptionMultiplier,
                    CarLimits.DecimalPrecision);
            }
        }

        /// <summary>
        /// Общая расчётная сумма аренды.
        /// </summary>
        public decimal TotalRentSum => (decimal)Range * RentCostPerMinute;
    }
}