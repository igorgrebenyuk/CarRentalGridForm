using System.Diagnostics;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

/// <summary>
/// Контроллер для управления реестром автомобилей 
/// </summary>
public class HomeController : Controller
{
    private readonly ICarService carService;

    /// <summary>
    /// Инициализирует контроллер с сервисом автомобилей (<see cref="ICarService"/>).
    /// </summary>
    public HomeController(ICarService carService)
    {
        this.carService = carService;
    }

    /// <summary>
    /// Отображает список всех автомобилей в системе проката.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var cars = await carService.GetAllCarsAsync();
        var statistics = await carService.GetStatisticsAsync();

        ViewBag.LowFuelCarsCount = statistics.LowFuelCars;

        return View(cars);
    }


    /// <summary>
    /// Создаёт новый автомобиль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(Car car)
    {
        if (!ModelState.IsValid)
        {
            var cars = await carService.GetAllCarsAsync();
            return View(nameof(Index), cars);
        }

        await carService.AddCarAsync(car);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Редактирует существующий автомобиль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Car car)
    {
        if (id != car.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var cars = await carService.GetAllCarsAsync();
            return View(nameof(Index), cars);
        }

        try
        {
            await carService.UpdateCarAsync(car);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            var cars = await carService.GetAllCarsAsync();
            return View(nameof(Index), cars);
        }
    }


    /// <summary>
    /// Удаляет автомобиль по Id
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var cars = await carService.GetAllCarsAsync();
        var car = cars.FirstOrDefault(c => c.Id == id);

        if (car != null)
        {
            await carService.DeleteCarAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Обработчик ошибок приложения
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}