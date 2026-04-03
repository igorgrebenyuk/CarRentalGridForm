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

            carService = new CarService(carRepository);

            dgvCars.AutoGenerateColumns = false;
            dgvCars.DataSource = carRepository.GetCars();

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

            lblStatusInfo.Text = $"Всего машин: {total} | Критическое топливо (<7л): {lowFuel}";
        }

        /// <summary>
        /// Рисует динамическую цветную полоску для суммы аренды.
        /// Длина и цвет зависят от отношения значения к максимуму в столбце.
        /// </summary>
        private void dgvCars_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCars.Columns["colTotalSum"].Index)
            {
                var cars = carRepository.GetCars();
                if (cars.Count == 0) return;

                var maxVal = (double)cars.Max(c => c.TotalRentSum);
                var minVal = (double)cars.Min(c => c.TotalRentSum);

                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                if (e.Value != null && double.TryParse(e.Value.ToString(), out var currentVal))
                {
                    var range = maxVal - minVal;
                    var ratio = range > 0 ? (currentVal - minVal) / range : 1.0;
                    var displayRatio = 0.2 + (ratio * 0.8);

                    var red = ratio < 0.5 ? (int)(255 * (ratio * 2)) : 255;
                    var green = ratio > 0.5 ? (int)(255 * ((1 - ratio) * 2)) : 255;
                    var barColor = Color.FromArgb(180, red, green, 0);

                    var fillWidth = (int)((e.CellBounds.Width - 4) * displayRatio);
                    var fillRect = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, fillWidth, e.CellBounds.Height - 4);

                    using (var brush = new SolidBrush(barColor))
                    {
                        e.Graphics.FillRectangle(brush, fillRect);
                    }
                }

                e.PaintContent(e.CellBounds);
            }
        }

        /// <summary>
        /// Открывает окно редактирования и обновляет таблицу.
        /// </summary>
        private void btnEditCar_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Открывает форму для добавления нового автомобиля.
        /// </summary>
        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var newCar = new Car();
            var editForm = new CarEditForm(newCar);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                carRepository.GetCars().Add(newCar);

                dgvCars.DataSource = null;
                dgvCars.DataSource = carRepository.GetCars();

                UpdateStats();
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку удаления. Проверяет наличие выбора и запрашивает подтверждение.
        /// </summary>
        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (dgvCars.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите автомобиль в списке!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedCar = (Car)dgvCars.CurrentRow.DataBoundItem;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить {selectedCar.Brand} ({selectedCar.LicensePlate})?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                carRepository.GetCars().Remove(selectedCar);
                RefreshGrid();
                UpdateStats();
            }
        }

        /// <summary>
        /// Полностью обновляет привязку данных таблицы для отображения изменений.
        /// </summary>
        public void RefreshGrid()
        {
            dgvCars.DataSource = null;
            dgvCars.DataSource = carRepository.GetCars();
        }
    }
}