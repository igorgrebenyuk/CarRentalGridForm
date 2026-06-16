using CarRentalGridForm.Constants;
using System.ComponentModel.DataAnnotations;


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
        [Required(ErrorMessage = "Марка автомобиля обязательна для заполнения")]
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Государственный регистрационный номер.
        /// </summary>
        [Required(ErrorMessage = "Гос. номер автомобиля обязателен для заполнения")]
        public string LicensePlate { get; set; } = string.Empty;

        /// <summary>
        /// Пробег автомобиля в километрах.
        /// </summary>
        [Range(0, 1000000, ErrorMessage = "Пробег автомобиля должен быть от 0 до 1000000 км")]
        public int Mileage { get; set; }

        /// <summary>
        /// Средний расход топлива в литрах на 100 км.
        /// </summary>
        [Range(1.0, 50.0, ErrorMessage = "Средний расход топлива в литрах на 100 км должен быть от 1 до 50")]
        public double AverageConsumption { get; set; }

        /// <summary>
        /// Текущий объём топлива в баке в литрах.
        /// </summary>
        [Range(0.0, 100.0, ErrorMessage = "Текущий объём топлива в баке должен быть от 1 до 100 литров")]
        public double CurrentFuel { get; set; }

        /// <summary>
        /// Стоимость аренды за одну минуту в рублях.
        /// </summary>
        [Range(1.0 , 1000.0, ErrorMessage = "Стоимость аренды за одну минуту должна быть от 1 до 1000 рублей")]
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