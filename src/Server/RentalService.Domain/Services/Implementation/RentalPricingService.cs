using RentalService.Domain.Entities;
using RentalService.Domain.Services.Interfaces;

namespace RentalService.Domain.Services.Implementation;

public sealed class RentalPricingService : IRentalPricingService
{
    private const decimal BaseDayRental = 100;
    private const decimal BaseKmPrice = 2;

    public decimal Calculate(CarCategory category, int days, long km) =>
        category switch
        {
            CarCategory.SmallCar => BaseDayRental * days,
            CarCategory.Combi => BaseDayRental * days * 1.3m + BaseKmPrice * km,
            CarCategory.Truck => BaseDayRental * days * 1.5m + BaseKmPrice * km * 1.5m,
            _ => throw new ArgumentOutOfRangeException(nameof(category))
        };
}