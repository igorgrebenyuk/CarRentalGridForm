namespace CarRentalGridForm.Constants
{
    /// <summary>
    /// Класс, содержащий бизнес-ограничения и константы для валидации данных автомобилей.
    /// </summary>
    public static class CarLimits
    {
        /// <summary>
        /// Минимально допустимое значение пробега в километрах.
        /// </summary>
        public const int MinMileage = 0;

        /// <summary>
        /// Максимально допустимое значение пробега в километрах.
        /// </summary>
        public const int MaxMileage = 1000000;

        /// <summary>
        /// Минимально допустимый расход топлива в литрах на 100 км.
        /// </summary>
        public const double MinConsumption = 1.0;

        /// <summary>
        /// Максимально допустимый расход топлива в литрах на 100 км.
        /// </summary>
        public const double MaxConsumption = 50.0;

        /// <summary>
        /// Минимально допустимый объём топлива в литрах.
        /// </summary>
        public const double MinFuel = 0.0;

        /// <summary>
        /// Максимально допустимый объём топлива в литрах.
        /// </summary>
        public const double MaxFuel = 100.0;

        /// <summary>
        /// Минимально допустимая стоимость аренды за минуту в рублях.
        /// </summary>
        public const decimal MinRentCost = 1.0m;

        /// <summary>
        /// Максимально допустимая стоимость аренды за минуту в рублях.
        /// </summary>
        public const decimal MaxRentCost = 1000.0m;

        /// <summary>
        /// Критический уровень топлива, ниже которого требуется заправка.
        /// </summary>
        public const double CriticalFuelLevel = 7.0;

        /// <summary>
        /// Проверяет, находится ли пробег в допустимом диапазоне.
        /// </summary>
        public static bool IsValidMileage(int mileage) =>
            mileage >= MinMileage && mileage <= MaxMileage;

        /// <summary>
        /// Проверяет, находится ли расход топлива в допустимом диапазоне.
        /// </summary>
        public static bool IsValidConsumption(double consumption) =>
            consumption >= MinConsumption && consumption <= MaxConsumption;

        /// <summary>
        /// Проверяет, находится ли объём топлива в допустимом диапазоне.
        /// </summary>
        public static bool IsValidFuel(double fuel) =>
            fuel >= MinFuel && fuel <= MaxFuel;

        /// <summary>
        /// Проверяет, находится ли стоимость аренды в допустимом диапазоне.
        /// </summary>
        public static bool IsValidRentCost(decimal cost) =>
            cost >= MinRentCost && cost <= MaxRentCost;

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимого пробега.
        /// </summary>
        public static string GetMileageError() =>
            $"Пробег должен быть от {MinMileage} до {MaxMileage} км";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимого расхода топлива.
        /// </summary>
        public static string GetConsumptionError() =>
            $"Расход должен быть от {MinConsumption} до {MaxConsumption} л/100км";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимого объёма топлива.
        /// </summary>
        public static string GetFuelError() =>
            $"Топливо должно быть от {MinFuel} до {MaxFuel} литров";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимой стоимости аренды.
        /// </summary>
        public static string GetRentCostError() =>
            $"Стоимость должна быть от {MinRentCost} до {MaxRentCost} руб";

        /// <summary>
        /// Стандартный множитель для расчёта расхода топлива на 100 км.
        /// </summary>
        public const double FuelConsumptionMultiplier = 100.0;

        /// <summary>
        /// Количество десятичных знаков для округления расчётных значений.
        /// </summary>
        public const int DecimalPrecision = 2;

        /// <summary>
        /// Минимальное значение для проверки корректности расхода топлива.
        /// </summary>
        public const double MinConsumptionThreshold = 0.0;
    }
}