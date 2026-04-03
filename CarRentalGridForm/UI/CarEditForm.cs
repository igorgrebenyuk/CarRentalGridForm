using System;
using System.Windows.Forms;
using CarRentalGridForm.Models;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Окно добавления и редактирования информации об автомобиле.
    /// </summary>
    public partial class CarEditForm : Form
    {
        private Car currentCar;

        /// <summary>
        /// Инициализирует форму для редактирования переданного автомобиля.
        /// </summary>
        public CarEditForm(Car car)
        {
            InitializeComponent();
            currentCar = car;
        }

        /// <summary>
        /// Вызывается при загрузке формы. Заполняет поля данными автомобиля.
        /// </summary>
        private void CarEditForm_Load(object sender, EventArgs e)
        {
            txtBrand.Text = currentCar.Brand;
            txtLicensePlate.Text = currentCar.LicensePlate;

            // Защита от выхода за границы NumericUpDown
            numMileage.Value = Math.Min(numMileage.Maximum, currentCar.Mileage);
            numConsumption.Value = Math.Min(numConsumption.Maximum, (decimal)currentCar.AverageConsumption);
            numFuel.Value = Math.Min(numFuel.Maximum, (decimal)currentCar.CurrentFuel);
            numPrice.Value = Math.Min(numPrice.Maximum, currentCar.RentCostPerMinute);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку ОК. Сохраняет введенные данные.
        /// </summary>
        /// <summary>
        /// Сохраняет изменения и закрывает форму.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            currentCar.Brand = txtBrand.Text;
            currentCar.LicensePlate = txtLicensePlate.Text;
            currentCar.Mileage = (int)numMileage.Value;
            currentCar.AverageConsumption = (double)numConsumption.Value;
            currentCar.CurrentFuel = (double)numFuel.Value;
            currentCar.RentCostPerMinute = numPrice.Value;

            // ВАЖНО: Устанавливаем результат, чтобы главная форма поняла, что нужно обновиться
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}