namespace CarRentalGridForm.Models
{
    public class Car
    {
        /// <summary>
        /// Уникальный идентификатор автомобиля.
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
                if (AverageConsumption <= 0)
                    return 0;

                return Math.Round((CurrentFuel / AverageConsumption) * 100, 2);
            }
        }

        /// <summary>
        /// Общая расчётная сумма аренды.
        /// </summary>
        public decimal TotalRentSum => (decimal)Range * RentCostPerMinute;
    }
}