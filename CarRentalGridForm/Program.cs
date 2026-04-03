using System;
using System.Windows.Forms;
using CarRentalGridForm.UI; 

namespace CarRentalGridForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new CarRentalGridForm.UI.CarRentalGridForm());
        }
    }
}