using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarRentalGridForm.Models;
using CarRentalGridForm.BL.Contracts;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Главная форма приложения для управления парком автомобилей.
    /// </summary>
    public partial class CarRentalGridForm : Form
    {
        private readonly ICarService carService;
        private BindingSource bindingSource = null!;
        private decimal maxRentSum;

        /// <summary>
        /// Инициализирует главную форму с сервисом автомобилей.
        /// </summary>
        public CarRentalGridForm(ICarService service)
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
        }

        private void LoadData()
        {
            var cars = carService.GetAllCars();
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

        private void btnDeleteCar_Click(object sender, EventArgs e)
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
                    carService.DeleteCar(selectedCar.Id);
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
            if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                var grid = (DataGridView)sender;

                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out var currentSum))
                {
                    e.PaintBackground(e.CellBounds, true);

                    var percentage = maxRentSum > 0 ? (double)(currentSum / maxRentSum) : 0;

                    var red = (int)(255 * percentage);
                    var green = (int)(200 * (1 - percentage * 0.5));
                    var blue = (int)(150 * (1 - percentage));

                    var fillColor = Color.FromArgb(150, red, green, blue);

                    var barWidth = (int)((e.CellBounds.Width - 8) * percentage);

                    using (var brush = new SolidBrush(fillColor))
                    {
                        var rect = new Rectangle(
                            e.CellBounds.X + 4,
                            e.CellBounds.Y + 2,
                            Math.Max(barWidth, 2),
                            e.CellBounds.Height - 5);

                        e.Graphics.FillRectangle(brush, rect);
                    }

                    e.PaintContent(e.CellBounds);
                    e.Handled = true;
                }
            }
        }

        private void UpdateStatusInfo()
        {
            var stats = carService.GetStatistics();
            lblStatusInfo.Text =
                $"Всего машин: {stats.TotalCars} | Критическое топливо: {stats.LowFuelCars}";
        }
    }
}