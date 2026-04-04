using System;
using System.Windows.Forms;
using CarRentalGridForm.Models;
using CarRentalGridForm.Helpers;
using CarRentalGridForm.BL.Contracts;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Форма для добавления или редактирования данных автомобиля.
    /// </summary>
    public partial class CarEditForm : Form
    {
        private readonly ICarService carService;
        private Car currentCar;
        // УДАЛИТЕ эту строку, если она есть:
        // private ErrorProvider errorProvider;  ← УДАЛИТЬ!
        private bool isNewCar;

        /// <summary>
        /// Создаёт экземпляр формы редактирования автомобиля.
        /// </summary>
        public CarEditForm(ICarService service, Car car, bool isNew)
        {
            InitializeComponent();
            carService = service;
            currentCar = car;
            isNewCar = isNew;
            // Инициализация делается в Designer.cs
        }

        private void CarEditForm_Load(object sender, EventArgs e)
        {
            Text = isNewCar ? "Добавление автомобиля" : "Редактирование автомобиля";

            CarFormMapper.LoadCarToForm(
                currentCar,
                txtBrand,
                txtLicensePlate,
                numMileage,
                numConsumption,
                numFuel,
                numPrice);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            CarFormMapper.SaveFormToCar(
                currentCar,
                txtBrand.Text.Trim(),
                txtLicensePlate.Text.Trim().ToUpper(),
                (int)numMileage.Value,
                (double)numConsumption.Value,
                (double)numFuel.Value,
                numPrice.Value);

            try
            {
                if (isNewCar)
                    carService.AddCar(currentCar);
                else
                    carService.UpdateCar(currentCar);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateFields()
        {
            var isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                errorProvider.SetError(txtBrand, "Марка не может быть пустой");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                errorProvider.SetError(txtLicensePlate, "Введите гос. номер");
                isValid = false;
            }

            if (!CarValidator.ValidateCarData(
                txtBrand.Text,
                txtLicensePlate.Text,
                (int)numMileage.Value,
                (double)numConsumption.Value,
                (double)numFuel.Value,
                numPrice.Value,
                out var errorMessage))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}