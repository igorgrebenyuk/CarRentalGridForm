using CarRentalGridForm.BL;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.UI;
using CarRentalGridForm.DAL;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;


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
        private static void Main()
        {
            using var log = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Debug()
            .WriteTo.File("logs\\app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            ApplicationConfiguration.Initialize();

            ICarRepository repository = new CarRepository();
            ICarService service = new CarService(repository);
            ICarService loggingWrapper = new CarServiceLogWrapper(service, logger);

            Application.Run(new MainForm(loggingWrapper));

            Log.CloseAndFlush();
        }
    }
}