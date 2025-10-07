using RentalService.Domain.Entities;
using RentalService.Domain.Services.Interfaces;

namespace RentalService.Domain.Services.Implementation;

public sealed class RentalPricingService : IRentalPricingService
{
    public decimal CalculatePrice(CarCategory category, int days, long km, decimal baseDayRental, decimal baseKmPrice)
    {
        return category switch
        {
            CarCategory.SmallCar => baseDayRental * days,
            CarCategory.Combi => baseDayRental * days * 1.3m + baseKmPrice * km,
            CarCategory.Truck => baseDayRental * days * 1.5m + baseKmPrice * km * 1.5m,
            _ => throw new ArgumentOutOfRangeException(nameof(category))
        };
    }
}