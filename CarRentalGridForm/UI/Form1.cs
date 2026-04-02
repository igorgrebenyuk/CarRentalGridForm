using System;
using System.Drawing;
using System.Windows.Forms;
using CarRentalGridForm.DAL;
using CarRentalGridForm.Models;
using CarRentalGridForm.BL;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Главное окно программы проката авто.
    /// </summary>
    public partial class CarRentalGridForm : Form
    {
        private CarRepository carRepository = new CarRepository();
        private CarService carService;

        public CarRentalGridForm()
        {
            InitializeComponent();

            // Инициализация логики
            carService = new CarService(carRepository);

            // Настройка таблицы (проверь, что Name в дизайнере именно dgvCars)
            dgvCars.AutoGenerateColumns = false;
            dgvCars.DataSource = carRepository.GetCars();

            // Добавляем машины через var
            var cars = carRepository.GetCars();

            cars.Add(new Car { Brand = "Hyundai Creta", LicensePlate = "А123БВ 77", Mileage = 45000, AverageConsumption = 8.5, CurrentFuel = 40.0, RentCostPerMinute = 12.5m });
            cars.Add(new Car { Brand = "Lada Vesta", LicensePlate = "Е555КХ 199", Mileage = 12000, AverageConsumption = 7.2, CurrentFuel = 15.0, RentCostPerMinute = 9.0m });
            cars.Add(new Car { Brand = "Mitsubishi Outlander", LicensePlate = "М001ОР 777", Mileage = 89000, AverageConsumption = 11.5, CurrentFuel = 5.0, RentCostPerMinute = 18.0m });

            UpdateStats();
        }

        /// <summary>
        /// Обновляет статистику в нижней панели.
        /// </summary>
        private void UpdateStats()
        {
            var total = carService.GetTotalCount();
            var lowFuel = carService.GetLowFuelCount();

            // Проверь, что Name лейбла в дизайнере именно lblStatusInfo
            lblStatusInfo.Text = $"Всего машин: {total} | Критическое топливо (<7л): {lowFuel}";
        }
    }
}