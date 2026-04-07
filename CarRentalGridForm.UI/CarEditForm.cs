using CarRentalGridForm.Models;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.Constants;

namespace CarRentalGridForm.UI
{
    /// <summary>
    /// Форма для добавления или редактирования данных автомобиля.
    /// </summary>
    public partial class CarEditForm : Form
    {
        private readonly ICarService carService;
        private Car currentCar;
        private readonly BindingSource bindingSource;
        private readonly ErrorProvider errorProvider;
        private readonly bool isNewCar;

        /// <summary>
        /// Инициализирует форму редактирования автомобиля.
        /// </summary>
        public CarEditForm(ICarService service, Car car, bool isNew)
        {
            InitializeComponent();
            carService = service;
            currentCar = car;
            isNewCar = isNew;

            errorProvider = new ErrorProvider
            {
                ContainerControl = this,
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

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

            SetupBrandBinding();
            SetupLicensePlateBinding();
            SetupMileageBinding();
            SetupConsumptionBinding();
            SetupFuelBinding();
            SetupPriceBinding();
        }

        private void SetupBrandBinding()
        {
            var brandBinding = new Binding("Text", bindingSource, "Brand", true, DataSourceUpdateMode.OnValidation);
            brandBinding.Parse += (s, e) =>
            {
                var value = e.Value as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    errorProvider.SetError(txtBrand, "Марка не может быть пустой");
                    e.Value = value ?? string.Empty;
                }
                else
                {
                    errorProvider.SetError(txtBrand, null);
                    e.Value = value.Trim();
                }
            };
            txtBrand.DataBindings.Add(brandBinding);
        }

        private void SetupLicensePlateBinding()
        {
            var licenseBinding = new Binding("Text", bindingSource, "LicensePlate", true, DataSourceUpdateMode.OnValidation);
            licenseBinding.Parse += (s, e) =>
            {
                var value = e.Value as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    errorProvider.SetError(txtLicensePlate, "Введите гос. номер");
                    e.Value = value ?? string.Empty;
                }
                else
                {
                    errorProvider.SetError(txtLicensePlate, null);
                    e.Value = value.Trim().ToUpper();
                }
            };
            txtLicensePlate.DataBindings.Add(licenseBinding);
        }

        private void SetupMileageBinding()
        {
            var mileageBinding = new Binding("Value", bindingSource, "Mileage", true, DataSourceUpdateMode.OnValidation);
            mileageBinding.Parse += (s, e) =>
            {
                if (e.Value is int mileage)
                {
                    if (mileage < CarLimits.MinMileage || mileage > CarLimits.MaxMileage)
                    {
                        errorProvider.SetError(numMileage, $"Пробег от {CarLimits.MinMileage} до {CarLimits.MaxMileage}");
                        e.Value = Math.Max(CarLimits.MinMileage, Math.Min(mileage, CarLimits.MaxMileage));
                    }
                    else
                    {
                        errorProvider.SetError(numMileage, null);
                        e.Value = mileage;
                    }
                }
            };
            numMileage.DataBindings.Add(mileageBinding);
        }

        private void SetupConsumptionBinding()
        {
            var consumptionBinding = new Binding("Value", bindingSource, "AverageConsumption", true, DataSourceUpdateMode.OnValidation);
            consumptionBinding.Parse += (s, e) =>
            {
                if (e.Value is decimal consumption)
                {
                    if (consumption <= (decimal)CarLimits.MinConsumption)
                    {
                        errorProvider.SetError(numConsumption, $"Расход > {CarLimits.MinConsumption}");
                        e.Value = (decimal)CarLimits.MinConsumption + 0.1m;
                    }
                    else if (consumption > (decimal)CarLimits.MaxConsumption)
                    {
                        errorProvider.SetError(numConsumption, $"Расход <= {CarLimits.MaxConsumption}");
                        e.Value = (decimal)CarLimits.MaxConsumption;
                    }
                    else
                    {
                        errorProvider.SetError(numConsumption, null);
                        e.Value = consumption;
                    }
                }
            };
            numConsumption.DataBindings.Add(consumptionBinding);
        }

        private void SetupFuelBinding()
        {
            var fuelBinding = new Binding("Value", bindingSource, "CurrentFuel", true, DataSourceUpdateMode.OnValidation);
            fuelBinding.Parse += (s, e) =>
            {
                if (e.Value is decimal fuel)
                {
                    if (fuel < 0)
                    {
                        errorProvider.SetError(numFuel, "Топливо >= 0");
                        e.Value = 0m;
                    }
                    else if (fuel > (decimal)CarLimits.MaxFuel)
                    {
                        errorProvider.SetError(numFuel, $"Топливо <= {CarLimits.MaxFuel}");
                        e.Value = (decimal)CarLimits.MaxFuel;
                    }
                    else
                    {
                        errorProvider.SetError(numFuel, null);
                        e.Value = fuel;
                    }
                }
            };
            numFuel.DataBindings.Add(fuelBinding);
        }

        private void SetupPriceBinding()
        {
            var priceBinding = new Binding("Value", bindingSource, "RentCostPerMinute", true, DataSourceUpdateMode.OnValidation);
            priceBinding.Parse += (s, e) =>
            {
                if (e.Value is decimal price)
                {
                    if (price <= 0)
                    {
                        errorProvider.SetError(numPrice, "Цена > 0");
                        e.Value = CarLimits.MinRentCost;
                    }
                    else if (price > CarLimits.MaxRentCost)
                    {
                        errorProvider.SetError(numPrice, $"Цена <= {CarLimits.MaxRentCost}");
                        e.Value = CarLimits.MaxRentCost;
                    }
                    else
                    {
                        errorProvider.SetError(numPrice, null);
                        e.Value = price;
                    }
                }
            };
            numPrice.DataBindings.Add(priceBinding);
        }

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

        private void btnOk_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            foreach (Binding binding in txtBrand.DataBindings)
            {
                binding.WriteValue();
            }
            foreach (Binding binding in txtLicensePlate.DataBindings)
            {
                binding.WriteValue(); 
            }
            foreach (Binding binding in numMileage.DataBindings)
            {
                binding.WriteValue();
            }
            foreach (Binding binding in numConsumption.DataBindings)
            { 
                binding.WriteValue(); 
            }
            foreach (Binding binding in numFuel.DataBindings)
            { 
                binding.WriteValue(); 
            }
            foreach (Binding binding in numPrice.DataBindings)
            {
                binding.WriteValue();
            }

            bool hasErrors = !string.IsNullOrEmpty(errorProvider.GetError(txtBrand)) ||
                           !string.IsNullOrEmpty(errorProvider.GetError(txtLicensePlate)) ||
                           !string.IsNullOrEmpty(errorProvider.GetError(numMileage)) ||
                           !string.IsNullOrEmpty(errorProvider.GetError(numConsumption)) ||
                           !string.IsNullOrEmpty(errorProvider.GetError(numFuel)) ||
                           !string.IsNullOrEmpty(errorProvider.GetError(numPrice));

            if (hasErrors)
                return;

            bindingSource.EndEdit();
            var tempCar = (Car)bindingSource.DataSource;

            currentCar.Brand = tempCar.Brand;
            currentCar.LicensePlate = tempCar.LicensePlate;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}