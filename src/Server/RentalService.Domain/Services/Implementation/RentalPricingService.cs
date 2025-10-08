using Microsoft.Extensions.Options;
using RentalService.Domain.Configuration;
using RentalService.Domain.Entities;
using RentalService.Domain.Services.Interfaces;

namespace RentalService.Domain.Services.Implementation;

public sealed class RentalPricingService(IOptions<RentalPricingOptions> options) : IRentalPricingService
{
    private readonly RentalPricingOptions _options = options.Value;

    public decimal Calculate(CarCategory category, int days, long km) =>
        category switch
        {
            CarCategory.SmallCar => _options.BaseDayRental * days,
            CarCategory.Combi => _options.BaseDayRental * days * 1.3m + _options.BaseKmPrice * km,
            CarCategory.Truck => _options.BaseDayRental * days * 1.5m + _options.BaseKmPrice * km * 1.5m,
            _ => throw new ArgumentOutOfRangeException(nameof(category))
        };
}