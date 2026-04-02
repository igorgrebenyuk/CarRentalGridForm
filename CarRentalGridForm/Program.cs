using CarRentalGridForm.UI;

namespace CarRentalGridForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Запускаем твою форму по новому адресу
            Application.Run(new CarRentalGridForm.UI.CarRentalGridForm());
        }
    }
}