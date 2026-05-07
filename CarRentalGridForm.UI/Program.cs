using CarRentalGridForm.BL;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.DAL.Contracts;
using CarRentalGridForm.UI;
using CarRentalGridForm.DAL;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace CarRentalGridForm
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var seqApiKey = "mqoY4hJHcRmN6lLAXLgL";

            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.Seq(
                    serverUrl: "http://localhost:5341",
                    apiKey: seqApiKey,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug
                )
                .WriteTo.File("logs/car-perf-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var loggerFactory = new SerilogLoggerFactory(serilogLogger);
            var logger = loggerFactory.CreateLogger<CarServiceLogWrapper>();

            ApplicationConfiguration.Initialize();

            ICarRepository repository = new CarRepository();
            ICarService service = new CarService(repository);

            ICarService loggingWrapper = new CarServiceLogWrapper(service, logger);

            Application.Run(new MainForm(loggingWrapper));

            Log.CloseAndFlush();
        }
    }
}