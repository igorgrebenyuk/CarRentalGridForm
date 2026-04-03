using System;
using System.ComponentModel;
using System.Windows.Forms;
using CarRentalGridForm.Models;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Форма для редактирования данных автомобиля с поддержкой валидации.
    /// </summary>
    public partial class CarEditForm : Form
    {
        private Car currentCar;

        public CarEditForm(Car car)
        {
            InitializeComponent();
            currentCar = car;
        }

        private void CarEditForm_Load(object sender, EventArgs e)
        {
            // Заполняем поля данными (как делали раньше)
            txtBrand.Text = currentCar.Brand;
            txtLicensePlate.Text = currentCar.LicensePlate;
            numMileage.Value = (decimal)currentCar.Mileage;
            numFuel.Value = (decimal)currentCar.CurrentFuel;
            numPrice.Value = currentCar.RentCostPerMinute;
        }

       

        
        /// <summary>
        /// Обработчик нажатия кнопки "Сохранить". Срабатывает валидация.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Сначала проверяем, всё ли правильно введено
            if (!ValidateFields())
            {
                return;
            }

            // ТУТ КРОЕТСЯ ОШИБКА: проверь, что эти строки есть
            currentCar.Brand = txtBrand.Text;
            currentCar.LicensePlate = txtLicensePlate.Text;
            currentCar.AverageConsumption = (double)numConsumption.Value; // Проверь имя контрола!
            currentCar.CurrentFuel = (double)numFuel.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Проверяет корректность всех заполненных полей.
        /// </summary>
        private bool ValidateFields()
        {
            var isValid = true;
            errorProvider.Clear(); // Очищаем старые ошибки перед новой проверкой

            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                errorProvider.SetError(txtBrand, "Марка не может быть пустой!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                errorProvider.SetError(txtLicensePlate, "Введите гос. номер!");
                isValid = false;
            }

            if (numPrice.Value <= 0)
            {
                errorProvider.SetError(numPrice, "Цена аренды должна быть больше нуля!");
                isValid = false;
            }

            return isValid;
        }
    }
}