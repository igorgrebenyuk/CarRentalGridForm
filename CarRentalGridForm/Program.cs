using System;
using System.Windows.Forms;
using CarRentalGridForm.UI;
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

            // Инициализация слоёв
            ICarRepository repository = new CarRepository();
            ICarService service = new CarService(repository);

            Application.Run(new global::CarRentalGridForm.UI.CarRentalGridForm(service));
        }
    }
}