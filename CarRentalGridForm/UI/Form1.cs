using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
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
        /// <summary>
        /// Рисует динамическую цветную полоску для суммы аренды.
        /// Длина и цвет зависят от отношения значения к максимуму в столбце.
        /// </summary>
        private void dgvCars_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Проверяем, что это не заголовок и именно колонка с суммой аренды
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCars.Columns["colTotalSum"].Index)
            {
                var cars = carRepository.GetCars();
                if (cars.Count == 0) return;

                // 1. Находим границы значений для масштабирования
                var maxVal = (double)cars.Max(c => c.TotalRentSum);
                var minVal = (double)cars.Min(c => c.TotalRentSum);

                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                if (e.Value != null && double.TryParse(e.Value.ToString(), out var currentVal))
                {
                    // 2. Вычисляем коэффициент (от 0.0 до 1.0)
                    var range = maxVal - minVal;
                    var ratio = range > 0 ? (currentVal - minVal) / range : 1.0;

                    // Чтобы полоска не была нулевой длины для самого маленького значения, добавим минимум
                    var displayRatio = 0.2 + (ratio * 0.8);

                    // 3. Рассчитываем цвет (от зеленого к красному)
                    // При ratio = 0 (min): R=0, G=255 (Зеленый)
                    // При ratio = 1 (max): R=255, G=0 (Красный)
                    var red = ratio < 0.5 ? (int)(255 * (ratio * 2)) : 255;
                    var green = ratio > 0.5 ? (int)(255 * ((1 - ratio) * 2)) : 255;
                    var barColor = Color.FromArgb(180, red, green, 0); // 180 - прозрачность, чтобы текст читался

                    // 4. Рисуем полоску
                    var fillWidth = (int)((e.CellBounds.Width - 4) * displayRatio);
                    var fillRect = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, fillWidth, e.CellBounds.Height - 4);

                    using (var brush = new SolidBrush(barColor))
                    {
                        e.Graphics.FillRectangle(brush, fillRect);
                    }
                }

                // 5. Рисуем текст поверх полоски
                e.PaintContent(e.CellBounds);
            }
        }
        /// <summary>
        /// Открывает окно редактирования для выбранного автомобиля.
        /// </summary>
        /// <summary>
        /// Открывает окно редактирования и обновляет таблицу.
        /// </summary>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dgvCars.CurrentRow == null) return;

            var selectedCar = (Car)dgvCars.CurrentRow.DataBoundItem;
            var editForm = new CarEditForm(selectedCar);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                dgvCars.DataSource = null;

                dgvCars.DataSource = carRepository.GetCars();

                dgvCars.Refresh();

                UpdateStats();
            }
        }
    }
}