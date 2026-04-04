using CarRentalGridForm.BL;
using CarRentalGridForm.DAL;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.DAL.Contracts;

namespace CarRentalGridForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ICarRepository repository = new CarRepository();
            ICarService service = new CarService(repository);

            Application.Run(new global::CarRentalGridForm.UI.CarRentalGridForm(service));
        }
    }
}