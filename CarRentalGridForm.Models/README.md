# Car Rental Grid Form Models

Библиотека моделей и контрактов для системы управления автопарком и прокатом автомобилей.

##  Установка

```bash
dotnet add package CarRentalGridForm.Models
```
## Состав пакета
### Константы (Constants/)
* CarLimits.cs — ограничения и константы для валидации
### Модели (Models/)
* Car.cs — модель автомобиля с расчётными свойствами
* Statistics.cs — статистика по автопарку
### Корневые файлы
* icon.png — иконка пакета (128×128 пикселей)
* README.md — этот файл с описанием
## Использование
```bash
using CarRentalGridForm.Models;

// Создание автомобиля
var car = new Car
{
    Brand = "Hyundai",
    LicensePlate = "A123BB77",
    Mileage = 15000,
    AverageConsumption = 8.5,
    CurrentFuel = 45.0,
    RentCostPerMinute = 5.5m
};

// Расчётные свойства
Console.WriteLine($"Запас хода: {car.Range} ч");
Console.WriteLine($"Сумма аренды: {car.TotalRentSum} ₽");
```
## Свойства модели Car


| Свойство | Тип | Описание |
| :--- | :--- | :--- |
| `Id` | `int` | Уникальный идентификатор |
| `Brand` | `string` | Марка автомобиля |
| `LicensePlate` | `string` | Гос. номер |
| `Mileage` | `int` | Пробег (км) |
| `AverageConsumption` | `double` | Средний расход (л/100км) |
| `CurrentFuel` | `double` | Текущий уровень топлива (л) |
| `RentCostPerMinute` | `decimal` | Стоимость аренды (₽/мин) |
| `Range` | `double` | Запас хода (часы) — расчётное |
| `TotalRentSum` | `decimal` | Сумма аренды — расчётное |

## Автор
### Igor Grebenyuk