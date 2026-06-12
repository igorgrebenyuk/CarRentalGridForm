using System.Diagnostics;
using CarRentalGridForm.BL.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ICarService carService;
    public HomeController(ICarService carService)
    {
        this.carService = carService;
    }
    public async Task<IActionResult> Index()
    {
        var cars = await carService.GetAllCarsAsync();
        return View(cars);
    }   

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}