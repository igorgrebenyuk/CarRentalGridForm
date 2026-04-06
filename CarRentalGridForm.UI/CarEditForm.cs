using System.ComponentModel;
using CarRentalGridForm.Models;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.Constants;

namespace CarRentalGridForm.UI
{
    public partial class CarEditForm : Form
    {
        private readonly ICarService carService;
        private Car currentCar;
        private readonly BindingSource bindingSource;
        private readonly ErrorProvider errorProvider;
        private bool isNewCar;

        private void CarEditForm_Load(object sender, EventArgs e)
        {
            Text = isNewCar ? "Добавление автомобиля" : "Редактирование автомобиля";

            numMileage.Minimum = CarLimits.MinMileage;
            numMileage.Maximum = CarLimits.MaxMileage;

            numConsumption.Minimum = (decimal)CarLimits.MinConsumption;
            numConsumption.Maximum = (decimal)CarLimits.MaxConsumption;

            numFuel.Minimum = 0;
            numFuel.Maximum = (decimal)CarLimits.MaxFuel;

            numPrice.Minimum = CarLimits.MinRentCost;
            numPrice.Maximum = CarLimits.MaxRentCost;
        }

        private void Control_Validating(object? sender, CancelEventArgs e)
        {
            bool hasError = false;

            if (sender == txtBrand)
            {
                if (string.IsNullOrWhiteSpace(txtBrand.Text))
                {
                    errorProvider.SetError(txtBrand, "Марка не может быть пустой");
                    hasError = true;
                }
                else
                {
                    errorProvider.SetError(txtBrand, null);
                }
            }
            else if (sender == txtLicensePlate)
            {
                txtLicensePlate.Text = txtLicensePlate.Text.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
                {
                    errorProvider.SetError(txtLicensePlate, "Введите гос. номер");
                    hasError = true;
                }
                else
                {
                    errorProvider.SetError(txtLicensePlate, null);
                }
            }
            else if (sender == numConsumption)
            {
                if (numConsumption.Value <= (decimal)CarLimits.MinConsumption)
                {
                    errorProvider.SetError(numConsumption, $"Расход > {CarLimits.MinConsumption}");
                    hasError = true;
                }
                else
                {
                    errorProvider.SetError(numConsumption, null);
                }
            }
            else if (sender == numFuel)
            {
                if (numFuel.Value < 0)
                {
                    errorProvider.SetError(numFuel, "Топливо >= 0");
                    hasError = true;
                }
                else
                {
                    errorProvider.SetError(numFuel, null);
                }
            }
            else if (sender == numPrice)
            {
                if (numPrice.Value <= 0)
                {
                    errorProvider.SetError(numPrice, "Цена > 0");
                    hasError = true;
                }
                else
                {
                    errorProvider.SetError(numPrice, null);
                }
            }

            if (hasError)
            {
                e.Cancel = true;
            }
        }
        public CarEditForm(ICarService service, Car car, bool isNew)
        {
            InitializeComponent();
            carService = service;
            currentCar = car;
            isNewCar = isNew;

            errorProvider = new ErrorProvider();

            bindingSource = new BindingSource();

            SetupBindings();
        }

        private void SetupBindings()
        {
            var tempCar = new Car
            {
                Id = currentCar.Id,
                Brand = currentCar.Brand,
                LicensePlate = currentCar.LicensePlate,
                Mileage = currentCar.Mileage,
                AverageConsumption = currentCar.AverageConsumption,
                CurrentFuel = currentCar.CurrentFuel,
                RentCostPerMinute = currentCar.RentCostPerMinute
            };

            bindingSource.DataSource = tempCar;

            txtBrand.DataBindings.Add("Text", bindingSource, "Brand", true, DataSourceUpdateMode.OnValidation);
            txtLicensePlate.DataBindings.Add("Text", bindingSource, "LicensePlate", true, DataSourceUpdateMode.OnValidation);
            numMileage.DataBindings.Add("Value", bindingSource, "Mileage", true, DataSourceUpdateMode.OnValidation);
            numConsumption.DataBindings.Add("Value", bindingSource, "AverageConsumption", true, DataSourceUpdateMode.OnValidation);
            numFuel.DataBindings.Add("Value", bindingSource, "CurrentFuel", true, DataSourceUpdateMode.OnValidation);
            numPrice.DataBindings.Add("Value", bindingSource, "RentCostPerMinute", true, DataSourceUpdateMode.OnValidation);

            txtBrand.Validating += Control_Validating;
            txtLicensePlate.Validating += Control_Validating;
            numMileage.Validating += Control_Validating;
            numConsumption.Validating += Control_Validating;
            numFuel.Validating += Control_Validating;
            numPrice.Validating += Control_Validating;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (this.ValidateChildren())
            {
                bindingSource.EndEdit();

                var tempCar = (Car)bindingSource.DataSource;

                currentCar.Brand = tempCar.Brand?.Trim() ?? string.Empty;
                currentCar.LicensePlate = tempCar.LicensePlate?.Trim().ToUpper() ?? string.Empty;
                currentCar.Mileage = tempCar.Mileage;
                currentCar.AverageConsumption = tempCar.AverageConsumption;
                currentCar.CurrentFuel = tempCar.CurrentFuel;
                currentCar.RentCostPerMinute = tempCar.RentCostPerMinute;

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
                    MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}