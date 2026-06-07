using CarRentalGridForm.Models;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.Constants;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Главная форма приложения для управления парком автомобилей.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ICarService carService;
        private BindingSource bindingSource;
        private decimal maxRentSum;

        /// <summary>
        /// Инициализирует главную форму с сервисом автомобилей.
        /// </summary>
        public MainForm(ICarService service)
        {
            InitializeComponent();
            carService = service;

            InitializeDataGridView();
            LoadData();
            UpdateStatusInfo();
        }

        private void InitializeDataGridView()
        {
            bindingSource = new BindingSource();
            dgvCars.DataSource = bindingSource;
            dgvCars.AutoGenerateColumns = false;
            dgvCars.CellPainting += dgvCars_CellPainting;
        }

        private async void LoadData()
        {
            var cars = await carService.GetAllCarsAsync();
            bindingSource.DataSource = cars;

            maxRentSum = cars.Any() ? cars.Max(c => c.TotalRentSum) : 1;
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var newCar = new Car();
            var editForm = new CarEditForm(carService, newCar, true);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                UpdateStatusInfo();
            }
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is Car selectedCar)
            {
                var editForm = new CarEditForm(carService, selectedCar, false);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    UpdateStatusInfo();
                }
            }
            else
            {
                MessageBox.Show("Выберите автомобиль для редактирования",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is Car selectedCar)
            {
                var result = MessageBox.Show(
                    $"Удалить автомобиль {selectedCar.Brand} ({selectedCar.LicensePlate})?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await carService.DeleteCarAsync(selectedCar.Id);
                    LoadData();
                    UpdateStatusInfo();
                }
            }
            else
            {
                MessageBox.Show("Выберите автомобиль для удаления",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvCars_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dgvCars.Columns[e.ColumnIndex].Name == UiConstants.TotalSumColumnName)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out var currentSum))
                {
                    e.PaintBackground(e.CellBounds, true);

                    var percentage = maxRentSum > 0 ? (double)(currentSum / maxRentSum) : 0;

                    var red = (int)(UiConstants.MaxColorValue * percentage);
                    var green = (int)(UiConstants.BaseGreenValue * (1 - percentage * UiConstants.GreenChannelFactor));
                    var blue = (int)(UiConstants.BaseBlueValue * (1 - percentage));

                    var fillColor = Color.FromArgb(UiConstants.AlphaTransparency, red, green, blue);
                    var barWidth = (int)((e.CellBounds.Width - (UiConstants.CellPaddingX * UiConstants.PaddingSidesCount)) * percentage);

                    using (var brush = new SolidBrush(fillColor))
                    {
                        var rect = new Rectangle(
                            e.CellBounds.X + UiConstants.CellPaddingX,
                            e.CellBounds.Y + UiConstants.CellPaddingY,
                            Math.Max(barWidth, UiConstants.MinBarWidth),
                            e.CellBounds.Height - UiConstants.CellHeightReduction);

                        e.Graphics.FillRectangle(brush, rect);
                    }

                    e.PaintContent(e.CellBounds);
                    e.Handled = true;
                }
            }
        }

        private async void UpdateStatusInfo()
        {
            var stats = await carService.GetStatisticsAsync();
            lblStatusInfo.Text =
                $"Всего машин: {stats.TotalCars} | Критическое топливо: {stats.LowFuelCars}";
        }
    }
}