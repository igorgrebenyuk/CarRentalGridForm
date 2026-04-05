using CarRentalGridForm.BL;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.UI;
using CarRentalGridForm.DAL; 


namespace CarRentalGridForm
{
    /// <summary>
    /// Главный класс приложения, содержащий точку входа.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа в приложение.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ICarRepository repository = new CarRepository();
            ICarService service = new CarService(repository);

            Application.Run(new MainForm(service));
        }
    }
}