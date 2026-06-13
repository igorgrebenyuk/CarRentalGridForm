using System.Diagnostics;
using CarRentalGridForm.BL.Contracts;
using CarRentalGridForm.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

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
            return View("Index", cars);
        }

        await carService.AddCarAsync(car);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Редактирует существующий автомобиль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(Car car)
    {
        if (!ModelState.IsValid)
        {
            var cars = await carService.GetAllCarsAsync();
            return View("Index", cars);
        }

        await carService.UpdateCarAsync(car);
        return RedirectToAction(nameof(Index));
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