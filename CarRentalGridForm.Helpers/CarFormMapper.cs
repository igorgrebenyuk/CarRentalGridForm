using CarRentalGridForm.Models;

namespace CarRentalGridForm.Helpers
{
    /// <summary>
    /// Вспомогательный класс для преобразования данных между моделью автомобиля и элементами формы.
    /// </summary>
    public static class CarFormMapper
    {
        /// <summary>
        /// Загружает данные автомобиля в элементы управления формы.
        /// </summary>
        public static void LoadCarToForm(
            Car car,
            TextBox txtBrand,
            TextBox txtLicensePlate,
            NumericUpDown numMileage,
            NumericUpDown numConsumption,
            NumericUpDown numFuel,
            NumericUpDown numPrice)
        {
            txtBrand.Text = car.Brand;
            txtLicensePlate.Text = car.LicensePlate;
            numMileage.Value = car.Mileage;
            numConsumption.Value = (decimal)car.AverageConsumption;
            numFuel.Value = (decimal)car.CurrentFuel;
            numPrice.Value = car.RentCostPerMinute;
        }

        /// <summary>
        /// Сохраняет данные из элементов управления формы в объект автомобиля.
        /// </summary>
        public static void SaveFormToCar(
            Car car,
            string brand,
            string licensePlate,
            int mileage,
            double consumption,
            double fuel,
            decimal price)
        {
            car.Brand = brand;
            car.LicensePlate = licensePlate;
            car.Mileage = mileage;
            car.AverageConsumption = consumption;
            car.CurrentFuel = fuel;
            car.RentCostPerMinute = price;
        }
    }
}