using CarRentalGridForm.Constants;

namespace CarRentalGridForm.Helpers
{
    /// <summary>
    /// Класс для валидации данных автомобиля согласно бизнес-правилам.
    /// </summary>
    public static class CarValidator
    {
        /// <summary>
        /// Проверяет корректность всех параметров автомобиля.
        /// </summary>
        public static bool ValidateCarData(
            string brand,
            string licensePlate,
            int mileage,
            double consumption,
            double fuel,
            decimal price,
            out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                errorMessage = "Марка автомобиля не может быть пустой";
                return false;
            }

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                errorMessage = "Гос. номер не может быть пустым";
                return false;
            }

            if (!CarLimits.IsValidMileage(mileage))
            {
                errorMessage = CarLimits.GetMileageError();
                return false;
            }

            if (!CarLimits.IsValidConsumption(consumption))
            {
                errorMessage = CarLimits.GetConsumptionError();
                return false;
            }

            if (!CarLimits.IsValidFuel(fuel))
            {
                errorMessage = CarLimits.GetFuelError();
                return false;
            }

            if (!CarLimits.IsValidRentCost(price))
            {
                errorMessage = CarLimits.GetRentCostError();
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}